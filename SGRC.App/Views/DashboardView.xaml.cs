using System;
using System.Windows;
using System.Windows.Controls;
using SGRC.App.Data;

namespace SGRC.App.Views
{
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();
        }

        private async void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text; 
                decimal cantidad = decimal.Parse(txtCantidad.Text);
                DateTime caducidad = dpCaducidad.SelectedDate ?? DateTime.Now; 
                string donante = txtDonante.Text;
                
                var db = new DatabaseManager();
                await db.RegistrarAlimentoCompleto(nombre, cantidad, caducidad, donante);
                
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