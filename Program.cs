using APIMongoDB.Model;
using APIMongoDB.Services;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1) Lê configurações do appsettings.json
builder.Services.Configure<ConfiguracaoMongoDB>(
    builder.Configuration.GetSection("ConfiguracaoMongoDb"));

// 2) Registra o cliente MongoDB
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var config = sp.GetRequiredService<IOptions<ConfiguracaoMongoDB>>().Value;
    return new MongoClient(config.StringConexao);
});

// 3) Registra a coleção de alunos
builder.Services.AddSingleton(sp =>
{
    var config = sp.GetRequiredService<IOptions<ConfiguracaoMongoDB>>().Value;
    var cliente = sp.GetRequiredService<IMongoClient>();
    var banco = cliente.GetDatabase(config.NomeBanco);
    return banco.GetCollection<Aluno>(config.NomeColecaoAlunos);
});

// 4) Registra o serviço de alunos
builder.Services.AddSingleton<ServicoAlunos>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
