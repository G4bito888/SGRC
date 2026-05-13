using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using SGRC.App.Data;

namespace SGRC.App.Views
{
    public partial class DashboardView : UserControl, IObserver
    {
        private int _notificaciones = 0;

        public DashboardView()
        {
            InitializeComponent();

            // Reloj en tiempo real
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) => {
                txtReloj.Text = DateTime.Now.ToString("hh:mm tt  M/dd/yyyy");
            };
            timer.Start();

            // Aqui se mete el patron observer
            InventarioNotifier.Instancia.Attach(this);
            
            // Cargamos todos los datos reales al iniciar la vista
            CargarEstadisticasReales();
        }

        public void UpdateStats()
        {
            Application.Current.Dispatcher.Invoke(() => {
                _notificaciones++;
                badgeNotificaciones.Badge = _notificaciones;
                if (_notificaciones == 1) 
                {
                    lstNotificaciones.Items.Clear();
                }
                lstNotificaciones.Items.Insert(0, $"[{DateTime.Now.ToString("HH:mm")}] La base de datos ha sido actualizada.");
                CargarEstadisticasReales();
            });
        }

        private async void CargarEstadisticasReales()
        {
            try 
            {
                // Usamos el Singleton
                int criticos = await DatabaseManager.Instancia.ObtenerProductosCriticos();
                decimal hoy = await DatabaseManager.Instancia.ObtenerDonacionesHoy();
                txtItemsCriticos.Text = $"{criticos} items críticos";
                txtDonacionesHoy.Text = $"+{hoy} kg recibidos";
            }
            catch (Exception)
            {
                txtItemsCriticos.Text = "No disponible";
                txtDonacionesHoy.Text = "No disponible";
            }
        }

        private async void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text; 
                decimal cantidad = decimal.Parse(txtCantidad.Text);
                DateTime caducidad = dpCaducidad.SelectedDate ?? DateTime.Now; 
                string donante = txtDonante.Text;

                await DatabaseManager.Instancia.RegistrarAlimentoCompleto(nombre, cantidad, caducidad, donante);
                
                txtNombre.Clear();
                txtCantidad.Clear();
                dpCaducidad.SelectedDate = null;
                txtDonante.Clear();
                MessageBox.Show("¡Guardado en Oracle y Neo4j exitosamente!", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}