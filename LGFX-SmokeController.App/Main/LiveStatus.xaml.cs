using System.Windows;
using System.Windows.Controls;
using LGFX_SmokeController.App.ArtNet;

namespace LGFX_SmokeController.App.Main;

public partial class LiveStatus : UserControl
{
    public ArtNetService ArtNetService => ((App)Application.Current).ArtNetService;
    
    public LiveStatus()
    {
        InitializeComponent();
    }
}