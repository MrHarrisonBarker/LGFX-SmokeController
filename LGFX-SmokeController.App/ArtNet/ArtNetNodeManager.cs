using System.Net;
using ART.NET;

namespace LGFX_SmokeController.App.ArtNet;

public delegate void NodePollEvent( ArtNetNode node );

public sealed class ArtNetNodeManager
{
    public event NodePollEvent? PollReceived;

    private const int PollRate = 1000 * 30;
    public ArtNetSocket Socket { get; set; }

    private readonly Thread PollThread;
    private readonly Thread ListenThread;

    private readonly CancellationTokenSource CancellationTokenSource = new ();

    public ArtNetNodeManager( ArtNetSocket socket, string shortName, string longName )
    {
        Socket = socket;
        PollReplyBuffer = new ArtNetPollReplyBuffer( socket.NetworkInterface.Address, shortName, longName );

        Socket.ListenFor( ArtNetOpCodes.Poll, ArtNetOpCodes.PollReply );

        PollThread = new Thread( Poll )
        {
            Name = "PollThread",
            Priority = ThreadPriority.BelowNormal
        };

        ListenThread = new Thread( Listen )
        {
            Name = "ListenThread",
            Priority = ThreadPriority.BelowNormal
        };
    }

    private readonly ArtNetPollBuffer PollBuffer = new ();
    private readonly ArtNetPollReplyBuffer PollReplyBuffer;


    public void StartPollReply()
    {
        Console.WriteLine( "Starting poll reply" );
        if ( !PollThread.IsAlive ) PollThread.Start();
        if ( !ListenThread.IsAlive ) ListenThread.Start();
    }

    private void Poll()
    {
        while ( !CancellationTokenSource.IsCancellationRequested )
        {
            Console.WriteLine( "Sending ArtNetPoll" );
            Socket.Send( PollBuffer );
            Socket.Send( PollReplyBuffer );

            // LastChecked = DateTime.Now;
            Thread.Sleep( PollRate );
        }
    }

    private void Listen()
    {
        while ( !CancellationTokenSource.Token.IsCancellationRequested )
        {
            Console.WriteLine( "Listening for polls" );
            try
            {
                var nextBuffer = Socket.RxQueue.Take( CancellationTokenSource.Token );

                if ( nextBuffer is ArtNetPollBuffer )
                {
                    Console.WriteLine( "Received poll, replying" );
                    Socket.Send( PollReplyBuffer );
                }
                else if ( nextBuffer is ArtNetPollReplyBuffer reply )
                {
                    Console.WriteLine( "Received poll reply" );
                    var ip = new IPAddress( reply.IpAddress );

                    PollReceived?.Invoke( new ArtNetNode( reply.ShortName, reply.LongName, ip ) );
                }
            }
            catch ( Exception e )
            {
                Console.WriteLine( e );
            }
        }
    }

    public void Refresh()
    {
        Task.Run( () =>
        {
            Thread.Sleep( 250 );
            for ( int i = 0; i < 3; i++ )
            {
                Socket.Send( PollBuffer );
                Socket.Send( PollReplyBuffer );

                Thread.Sleep( 250 );
            }
        } );
    }

    public void Stop()
    {
        CancellationTokenSource.Cancel();
    }
}