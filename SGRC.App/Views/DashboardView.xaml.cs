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
                // Si la fecha del sistema es hoy, mostramos el reloj normal
                if (SesionGlobal.FechaSistema.Date == DateTime.Now.Date)
                {
                    txtReloj.Text = DateTime.Now.ToString("hh:mm tt  M/dd/yyyy");
                    txtReloj.Foreground = System.Windows.Media.Brushes.White;
                }
                else
                {
                    // Si viajamos en el tiempo, cambiamos el texto y lo ponemos en amarillo para que el profe lo note
                    txtReloj.Text = "SIMULANDO: " + SesionGlobal.FechaSistema.ToString("dd/MM/yyyy");
                    txtReloj.Foreground = System.Windows.Media.Brushes.Yellow;
                }
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
                DateTime caducidad = dpCaducidad.SelectedDate ?? DateTime.Now; 

                if (caducidad.Date < DateTime.Now.Date)
                {
                    MessageBox.Show("No se pueden registrar insumos con fecha de caducidad vencida.", "Rechazo Automático", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string nombre = txtNombre.Text; 
                decimal cantidad = decimal.Parse(txtCantidad.Text);
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

        private void BtnSimular_Click(object sender, RoutedEventArgs e)
        {
            SesionGlobal.FechaSistema = dpSimulador.SelectedDate ?? DateTime.Now;
            InventarioNotifier.Instancia.Notify(); // ¡El Observer actualiza todo al instante! [cite: 165]
            MessageBox.Show($"Sistema sincronizado al: {SesionGlobal.FechaSistema:dd/MM/yyyy}");
        }
    }
}