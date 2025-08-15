using APIMongoDB.Model; 
using MongoDB.Driver;   

namespace APIMongoDB.Services
{
    // Classe responsável por gerenciar operações com alunos no banco de dados MongoDB
    public class ServicoAlunos
    {
        // Declaração de uma variável privada que representa a coleção de alunos no MongoDB
        private readonly IMongoCollection<Aluno> _colecaoAlunos;

        // Construtor da classe, recebe a coleção de alunos e inicializa a variável privada
        public ServicoAlunos(IMongoCollection<Aluno> colecaoAlunos)
        {
            _colecaoAlunos = colecaoAlunos;
        }

        // Método assíncrono para obter todos os alunos cadastrados
        public async Task<List<Aluno>> ObterTodosAsync() =>
            await _colecaoAlunos.Find(_ => true).ToListAsync(); // Busca todos os documentos na coleção

        // Método assíncrono para obter um aluno pelo seu Id
        public async Task<Aluno?> ObterPorIdAsync(string id) =>
            await _colecaoAlunos.Find(a => a.Id == id).FirstOrDefaultAsync(); // Busca o aluno com o Id informado

        // Método assíncrono para criar (inserir) um novo aluno na coleção
        public async Task<Aluno> CriarAsync(Aluno aluno)
        {
            await _colecaoAlunos.InsertOneAsync(aluno); // Insere o aluno no banco de dados
            return aluno; // Retorna o aluno inserido
        }

        // Método assíncrono para atualizar os dados de um aluno existente
        public async Task<bool> AtualizarAsync(string id, Aluno alunoAtualizado)
        {
            // Substitui o aluno com o Id informado pelos dados atualizados
            var resultado = await _colecaoAlunos.ReplaceOneAsync(a => a.Id == id, alunoAtualizado);
            return resultado.MatchedCount > 0; // Retorna true se algum documento foi encontrado e atualizado
        }

        // Método assíncrono para remover um aluno pelo Id
        public async Task<bool> RemoverAsync(string id)
        {
            // Remove o aluno com o Id informado da coleção
            var resultado = await _colecaoAlunos.DeleteOneAsync(a => a.Id == id);
            return resultado.DeletedCount > 0; // Retorna true se algum documento foi removido
        }
    }
}