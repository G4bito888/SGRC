using SGRC.App.Data;

var builder = WebApplication.CreateBuilder(args);

// Registramos la Fachada existente
builder.Services.AddSingleton<Facade>(
    _ => Facade.Instancia
);

// Configuración GraphQL
builder.AddGraphQL().AddTypes();

var app = builder.Build();

// Endpoint GraphQL
app.MapGraphQL();

app.RunWithGraphQLCommands(args);

/* PLANTILLA DEFAULT PARA GRAPHQL, NO BORRAR
var builder = WebApplication.CreateBuilder(args);

builder.AddGraphQL().AddTypes();

var app = builder.Build();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);
*/