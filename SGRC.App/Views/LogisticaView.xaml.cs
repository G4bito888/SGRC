using System;
using System.Windows.Controls;

namespace SGRC.App.Views
{
    public partial class LogisticaView : UserControl
    {
        public LogisticaView()
        {
            InitializeComponent();
            IniciarNavegador();
        }

        private async void IniciarNavegador()
        {
            // 1. Forzamos al motor interno de Chromium a despertar
            await neo4jBrowser.EnsureCoreWebView2Async(null);
            
            // 2. Ahora sí, le decimos que cargue nuestro puerto local
            neo4jBrowser.CoreWebView2.Navigate("http://localhost:7474");
        }
    }
}