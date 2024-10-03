using System.Windows;

namespace LGFX_SmokeController.App.Main;

public partial class MainWindow : Window
{
    public App App => ( App )Application.Current;
        
    public MainWindow()
    {
        InitializeComponent();
    }
}