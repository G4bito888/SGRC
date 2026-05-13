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
            await neo4jBrowser.EnsureCoreWebView2Async(null);
            neo4jBrowser.CoreWebView2.Navigate("http://localhost:7474");
        }
    }
}