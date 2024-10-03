using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;
using ART.NET;

namespace LGFX_SmokeController.App.ArtNet;

public sealed class ArtNetNodeManager : INotifyPropertyChanged
{
    private readonly Dispatcher Dispatcher;
    private const int PollRate = 1000 * 30;
    private readonly ArtNetSocket Socket;

    private readonly Thread PollThread;
    private readonly Thread ListenThread;

    private readonly CancellationTokenSource CancellationTokenSource = new ();

    public ArtNetNodeManager( ArtNetSocket socket, string shortName, string longName, Dispatcher dispatcher )
    {
        Socket = socket;
        Dispatcher = dispatcher;
        PollReplyBuffer = new ArtNetPollReplyBuffer( socket.NetworkInterface.Address, shortName, longName );
        Nodes = new ObservableCollection<ArtNetNode>();

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

        PollThread.Start();
        ListenThread.Start();
    }

    private readonly ArtNetPollBuffer PollBuffer = new ();
    private readonly ArtNetPollReplyBuffer PollReplyBuffer;
    private DateTime _LastChecked;

    public ObservableCollection<ArtNetNode> Nodes { get; set; }

    private void Poll()
    {
        while ( !CancellationTokenSource.IsCancellationRequested )
        {
            Console.WriteLine( "Sending ArtNetPoll" );
            Socket.Send( PollBuffer );
            Socket.Send( PollReplyBuffer );

            LastChecked = DateTime.Now;
            Thread.Sleep( PollRate );
        }
    }

    private void Listen()
    {
        while ( !CancellationTokenSource.IsCancellationRequested )
        {
            Console.WriteLine( "Listening for polls" );
            var nextBuffer = Socket.RxQueue.Take();

            if ( nextBuffer is ArtNetPollBuffer )
            {
                Console.WriteLine( "Received poll, replying" );
                Socket.Send( PollReplyBuffer );
            }
            else if ( nextBuffer is ArtNetPollReplyBuffer reply )
            {
                Console.WriteLine( "Received poll reply" );
                var ip = new IPAddress( reply.IpAddress );

                var existing = Nodes.FirstOrDefault( x => Equals( x.Address, ip ) );

                if ( existing is null )
                {
                    Console.WriteLine( Dispatcher );
                    Dispatcher.Invoke( () =>
                    {
                        Nodes.Add( new ArtNetNode( reply.ShortName, reply.LongName, ip ) );
                        Console.WriteLine( $"Nodes -> {string.Join( ",", Nodes )}" );
                    } );
                }

                
            }
        }
    }

    public DateTime LastChecked
    {
        get => _LastChecked;
        private set
        {
            if ( value.Equals( _LastChecked ) ) return;
            _LastChecked = value;
            OnPropertyChanged();
        }
    }

    public void Refresh()
    {
        for ( var i = 0; i < Nodes.Count; i++ )
        {
            Nodes.RemoveAt(0);
        }

        Task.Run( () =>
        {
            Thread.Sleep( 250 );
            for ( int i = 0; i < 3; i++ )
            {
                Socket.Send( PollBuffer );
                Socket.Send( PollReplyBuffer );
                
                LastChecked = DateTime.Now;
                Thread.Sleep( 250 );
            }
        } );
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged( [CallerMemberName] string? propertyName = null )
    {
        PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
    }

    private bool SetField<T>( ref T field, T value, [CallerMemberName] string? propertyName = null )
    {
        if ( EqualityComparer<T>.Default.Equals( field, value ) ) return false;
        field = value;
        OnPropertyChanged( propertyName );
        return true;
    }
}

public record ArtNetNode( string ShortName, string LongName, IPAddress Address, bool IsSending = false ) : INotifyPropertyChanged
{
    private bool _IsSending = IsSending;

    public bool IsSending
    {
        get => _IsSending;
        set
        {
            if ( value == _IsSending ) return;
            _IsSending = value;
            OnPropertyChanged();
        }
    }

    public override string ToString()
    {
        return $"{Address}:{LongName}";
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged( [CallerMemberName] string? propertyName = null )
    {
        PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
    }

    protected bool SetField<T>( ref T field, T value, [CallerMemberName] string? propertyName = null )
    {
        if ( EqualityComparer<T>.Default.Equals( field, value ) ) return false;
        field = value;
        OnPropertyChanged( propertyName );
        return true;
    }
}