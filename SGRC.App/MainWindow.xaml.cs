using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SGRC.App.Data; 
using SGRC.App.Views; 

namespace SGRC.App;

public partial class MainWindow : Window
{
    private readonly LogisticaView _vistaLogistica;

    public MainWindow()
    {
        InitializeComponent();
        MessageBox.Show("¡SGRC Iniciando!");
        _vistaLogistica = new LogisticaView();
        PantallaPrincipal.Content = new DashboardView();
    }

    private void BtnDashboard_Click(object sender, RoutedEventArgs e)
    {
        PantallaPrincipal.Content = new DashboardView();
    }

    private void BtnInventario_Click(object sender, RoutedEventArgs e)
    {
        PantallaPrincipal.Content = new InventarioView();
    }

    private void BtnLogistica_Click(object sender, RoutedEventArgs e)
    {
        PantallaPrincipal.Content = _vistaLogistica;
    }
}