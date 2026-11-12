using HotChocolate;
using SGRC.App.Data;

namespace SGRC.GraphQL.Api.Types;

[QueryType]
public static partial class Query
{
    // Consulta todos los lotes activos del inventario.
    public static async Task<List<LoteGraphQL>> GetLotesAsync(
        Facade facade)
    {
        var lotes = await facade.ObtenerInventario(DateTime.Today);

        return lotes.Select(Map).ToList();
    }

    // Consulta los lotes que caducan dentro de los próximos n días.
    public static async Task<List<LoteGraphQL>> GetLotesPorCaducarAsync(
        int dias,
        Facade facade)
    {
        if (dias < 0 || dias > 365)
        {
            throw new ArgumentOutOfRangeException(
                nameof(dias),
                "El rango permitido es de 0 a 365 días."
            );
        }

        var hoy = DateTime.Today;
        var limite = hoy.AddDays(dias);

        var lotes = await facade.ObtenerInventario(hoy);

        return lotes
            .Where(x =>
                x.FECHA_CADUCIDAD.Date >= hoy &&
                x.FECHA_CADUCIDAD.Date <= limite)
            .Select(Map)
            .ToList();
    }

    // Consulta los indicadores del dashboard.
    public static async Task<IndicadoresGraphQL> GetIndicadoresAsync(
        Facade facade)
    {
        var fecha = DateTime.Today;

        var criticos =
            await facade.ObtenerProductosCriticos(fecha);

        var donaciones =
            await facade.ObtenerDonacionesHoy(fecha);

        return new IndicadoresGraphQL(criticos, donaciones);
    }

    private static LoteGraphQL Map(InventarioDTO x)
    {
        return new LoteGraphQL(
            x.ID_LOTE,
            x.NOMBRE,
            x.CATEGORIA,
            x.CANTIDAD,
            x.UNIDAD_MEDIDA,
            x.FECHA_INGRESO,
            x.FECHA_CADUCIDAD,
            x.ESTADO_SEMAFORO
        );
    }
}

public sealed record LoteGraphQL(
    int IdLote,
    string? Nombre,
    string? Categoria,
    decimal Cantidad,
    string? UnidadMedida,
    DateTime FechaIngreso,
    DateTime FechaCaducidad,
    string? EstadoSemaforo
);

public sealed record IndicadoresGraphQL(
    int ProductosCriticos,
    decimal DonacionesHoy
);


/* PLANTILLA DEFAULT PARA GRAPHQL, NO BORRAR
namespace SGRC.GraphQL.Api.Types;

[QueryType]
public static partial class Query
{
    public static Book GetBook()
        => new Book("C# in depth.", new Author("Jon Skeet"));
}
*/