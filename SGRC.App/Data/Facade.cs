using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRC.App.Data
{
    public class Facade
    {
        private static Facade? _instancia;
        public static Facade Instancia => _instancia ??= new Facade();

        private Facade() { }

        // Metemos toda la base de datos abajo de la alfombra
        public async Task<IEnumerable<InventarioDTO>> ObtenerInventario()
        {
            return await DatabaseManager.Instancia.ObtenerInventarioCompleto();
        }

        public async Task EliminarLoteInventario(int idLote)
        {
            await DatabaseManager.Instancia.EliminarLote(idLote);
        }

        public async Task ActualizarLoteInventario(int idLote, decimal cantidad, DateTime caducidad)
        {
            await DatabaseManager.Instancia.ActualizarLote(idLote, cantidad, caducidad);
        }

        public string ExportarInventarioACSV(IEnumerable<InventarioDTO> datos)
        {
            var reporte = ReporteFactory.CrearReporte("CSV");
            string rutaBase = AppDomain.CurrentDomain.BaseDirectory;
            string rutaRegistros = System.IO.Path.Combine(rutaBase, "REGISTROS");
            System.IO.Directory.CreateDirectory(rutaRegistros);

            string nombreArchivo = $"Reporte_Inventario_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            string rutaFinal = System.IO.Path.Combine(rutaRegistros, nombreArchivo);

            reporte.GenerarReporte(datos, rutaFinal);
            return rutaFinal;
        }

        public bool UsuarioTienePermisosDeEdicion()
        {
            return SesionGlobal.RolActual != "Voluntario";
        }

        public async Task<int> ObtenerProductosCriticos()
        {
            return await DatabaseManager.Instancia.ObtenerProductosCriticos();
        }

        public async Task<decimal> ObtenerDonacionesHoy()
        {
            return await DatabaseManager.Instancia.ObtenerDonacionesHoy();
        }

        public async Task RegistrarAlimento(string nombre, decimal cantidad, DateTime caducidad, string donante)
        {
            await DatabaseManager.Instancia.RegistrarAlimentoCompleto(nombre, cantidad, caducidad, donante);
        }
    }
}