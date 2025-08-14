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

// Lê as configurações de conexão do MongoDB do appsettings.json e as
// disponibiliza via IOptions<ConfiguracaoMongoDB>
// IOptions<ConfiguracaoMongoDB> é uma interface usada para acessar configurações fortemente tipadas
// carregadas do appsettings.json, permitindo obter valores de configuração de forma segura e estruturada.
builder.Services.Configure<ConfiguracaoMongoDB>(
    builder.Configuration.GetSection("ConfiguracaoMongoDb"));


// Registra o cliente MongoDB como singleton, permitindo o acesso ao banco durante toda a vida da aplicação
// Singleton é um padrão de projeto onde uma única instância de um serviço é criada e
// compartilhada durante toda a vida da aplicação.
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var config = sp.GetRequiredService<IOptions<ConfiguracaoMongoDB>>().Value;
    return new MongoClient(config.StringConexao);
});

// Registra a coleção de alunos como singleton, facilitando operações CRUD na coleção "Alunos"
builder.Services.AddSingleton(sp =>
{
    var config = sp.GetRequiredService<IOptions<ConfiguracaoMongoDB>>().Value;
    var cliente = sp.GetRequiredService<IMongoClient>();
    var banco = cliente.GetDatabase(config.NomeBanco);
    return banco.GetCollection<Aluno>(config.NomeColecaoAlunos);
});

// Registra o serviço de alunos, responsável pela lógica de negócio relacionada à entidade Aluno
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
