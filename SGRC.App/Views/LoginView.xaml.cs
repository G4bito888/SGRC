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
            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Password;
            if (usuario == "admin" && password == "admin123")
            {
                SesionGlobal.RolActual = "Administrador";
                ((MainWindow)Application.Current.MainWindow).IngresarAlSistema();
            }
            else if (usuario == "voluntario" && password == "vol123")
            {
                SesionGlobal.RolActual = "Voluntario";
                ((MainWindow)Application.Current.MainWindow).IngresarAlSistema();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos. Por favor, intente de nuevo.", "Error de Autenticación", MessageBoxButton.OK, MessageBoxImage.Error);
                txtPassword.Clear();
            }
        }
    }
}