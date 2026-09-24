using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SGRC.App.Data;

namespace SGRC.App.Views
{
    public partial class InventarioView : UserControl
    {
        public InventarioView()
        {
            InitializeComponent();
            CargarDatos();

            if (!Facade.Instancia.UsuarioTienePermisosDeEdicion())
            {
                colAcciones.Visibility = Visibility.Collapsed;
                panelEdicion.Visibility = Visibility.Collapsed;
            }

            /* if (SesionGlobal.RolActual == "Voluntario")
            {
                colAcciones.Visibility = Visibility.Collapsed;
                panelEdicion.Visibility = Visibility.Collapsed;
            } */
        }

       private async void CargarDatos()
        {
            try 
            {
                gridInventario.ItemsSource = await Facade.Instancia.ObtenerInventario();
                // gridInventario.ItemsSource = await DatabaseManager.Instancia.ObtenerInventarioCompleto();
            }
            catch (Exception ex) 
            { 
                MessageBox.Show($"Error al cargar la tabla: {ex.Message}", "Error de Oracle", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (gridInventario.SelectedItem is InventarioDTO seleccionado)
            {
                var resultado = MessageBox.Show($"¿Estás seguro de dar de baja el lote {seleccionado.ID_LOTE} de {seleccionado.NOMBRE}?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (resultado == MessageBoxResult.Yes)
                {
                    try 
                    {
                        await Facade.Instancia.EliminarLoteInventario(seleccionado.ID_LOTE);
                        // await DatabaseManager.Instancia.EliminarLote(seleccionado.ID_LOTE);
                        CargarDatos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ocurrió un error al eliminar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (gridInventario.SelectedItem is InventarioDTO seleccionado)
            {
                txtEditId.Text = seleccionado.ID_LOTE.ToString();
                txtEditCantidad.Text = seleccionado.CANTIDAD.ToString();
                dpEditCaducidad.SelectedDate = seleccionado.FECHA_CADUCIDAD;
                panelEdicion.Visibility = Visibility.Visible;
            }
        }

        private async void BtnGuardarEdicion_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtEditId.Text, out int idLote) && decimal.TryParse(txtEditCantidad.Text, out decimal cant) && dpEditCaducidad.SelectedDate.HasValue)
            {
                await Facade.Instancia.ActualizarLoteInventario(idLote, cant, dpEditCaducidad.SelectedDate.Value);
                // await DatabaseManager.Instancia.ActualizarLote(idLote, cant, dpEditCaducidad.SelectedDate.Value);
                panelEdicion.Visibility = Visibility.Collapsed;
                CargarDatos();
                MessageBox.Show("Registro actualizado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnCancelarEdicion_Click(object sender, RoutedEventArgs e)
        {
            panelEdicion.Visibility = Visibility.Collapsed;
        }

        private void BtnExportarCSV_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridInventario.ItemsSource is IEnumerable<InventarioDTO> datos)
                {
                    string rutaGuardada = Facade.Instancia.ExportarInventarioACSV(datos);
                    MessageBox.Show($"¡Reporte exportado dinámicamente!\nGuardado en:\n{rutaGuardada}", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            /* try
            {
                var datos = gridInventario.ItemsSource as System.Collections.Generic.IEnumerable<InventarioDTO>;
                
                if (datos != null)
                {
                    var reporte = ReporteFactory.CrearReporte("CSV");
                    string rutaBase = AppDomain.CurrentDomain.BaseDirectory;
                    string rutaRegistros = System.IO.Path.Combine(rutaBase, "REGISTROS");

                    System.IO.Directory.CreateDirectory(rutaRegistros);

                    string nombreArchivo = $"Reporte_Inventario_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                    string rutaFinal = System.IO.Path.Combine(rutaRegistros, nombreArchivo);

                    reporte.GenerarReporte(datos, rutaFinal);
                    MessageBox.Show($"¡Reporte exportado dinámicamente!\nGuardado en:\n{rutaFinal}", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } */
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al exportar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}