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
        }

        private async void CargarDatos()
        {
            var db = new DatabaseManager();
            var datos = await db.ObtenerInventario();
            gridInventario.ItemsSource = datos; // Llenamos la tabla
        }
    }
}