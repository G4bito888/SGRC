using System.Windows;
using System.Windows.Controls;

namespace SGRC.App.Views
{
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (cbRol.SelectedItem is ComboBoxItem item)
            {
                SesionGlobal.RolActual = item.Content?.ToString() ?? "Admin";
                ((MainWindow)Application.Current.MainWindow).IngresarAlSistema();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un rol para ingresar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}