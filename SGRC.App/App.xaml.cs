using System.Configuration;
using System.Data;
using System.Windows;

namespace SGRC.App;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        AppDomain.CurrentDomain.UnhandledException += (s, ex) => 
            MessageBox.Show($"Error crítico: {ex.ExceptionObject}");
        try 
        {
            base.OnStartup(e);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar la interfaz: {ex.Message}\n\nDetalles: {ex.InnerException?.Message}");
        }
    }
}