// PONTO DE ENTRADA: Configura e inicia a aplicação.
// É onde você registra os serviços (Injeção de Dependência) 
// e define o "pipeline" de execução (Middleware) da API.


using RestWithASPNET10Erudio.Configurations;
using RestWithASPNET10Erudio.Repositories; 
using RestWithASPNET10Erudio.Repositories.Impl;         // PersonRepository, BookRepository, GenericRepository
using RestWithASPNET10Erudio.Services;
using RestWithASPNET10Erudio.Services.Impl;  // PersonServicesImpl, BookServicesImpl
using RestWithASPNET10Erudio.Data.DTO.V2;
using RestWithASPNET10Erudio.Hypermedia.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilogLogging();

builder.Services.AddControllers(options =>
	{
		options.Filters.Add<HypermediaFilter>();
	})
	.AddContentNegotiacion();

builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddEvolveConfiguration(builder.Configuration, builder.Environment);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiConfig();
builder.Services.AddSwaggerConfig();
builder.Services.AddRouteConfig();


builder.Services.AddCorsConfiguration(builder.Configuration);
builder.Services.AddHATEOASConfiguration();



builder.Services.AddScoped<IPersonServices, PersonServicesImplV1>();
builder.Services.AddScoped<IPersonServicesV2, PersonServicesV2>();
builder.Services.AddScoped<IBookServices, BookServicesImpl>();

builder.Services.AddScoped<IPersonRepository, PersonRepository>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseRouting();
app.UseCorsConfiguration(builder.Configuration);
app.UseAuthorization();

app.MapControllers();

app.UseHATEOASRoutes();

app.UseSwaggerSpecification();
app.UseScalarConfiguration();

app.Run();
