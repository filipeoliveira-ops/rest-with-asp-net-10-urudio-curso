//Ponto de entrada: configura e inicia a aplicação.
//É onde voce registra os serviços (Injeção de independencias)
//e define o 'pipeline" de execução (Middleware) da API


using RestWithASPNET10Erudio.Services;
using restWithASPNET10Erudio.services.Implementations;

var builder = WebApplication.CreateBuilder(args):

builder.Services.AddControllers();

builder.Services.AddSingleton<MathServices>();     //"<MathServices>" relaciona a classe "mathservices"
builder.Services.AddScoped<IpersonServices, PersonServicesimpl>();  //refere-se as classes "personServices" e "PersonServicesimpl" 

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAythorization();

app.MapControllers();

app.Run();

