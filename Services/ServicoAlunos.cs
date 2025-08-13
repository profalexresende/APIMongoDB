using APIMongoDB.Model;
using MongoDB.Driver;

namespace APIMongoDB.Services
{
    public class ServicoAlunos
    {
        private readonly IMongoCollection<Aluno> _colecaoAlunos;

        public ServicoAlunos(IMongoCollection<Aluno> colecaoAlunos)
        {
            _colecaoAlunos = colecaoAlunos;
        }

        public async Task<List<Aluno>> ObterTodosAsync() =>
            await _colecaoAlunos.Find(_ => true).ToListAsync();

        public async Task<Aluno?> ObterPorIdAsync(string id) =>
            await _colecaoAlunos.Find(a => a.Id == id).FirstOrDefaultAsync();

        public async Task<Aluno> CriarAsync(Aluno aluno)
        {
            await _colecaoAlunos.InsertOneAsync(aluno);
            return aluno;
        }

        public async Task<bool> AtualizarAsync(string id, Aluno alunoAtualizado)
        {
            var resultado = await _colecaoAlunos.ReplaceOneAsync(a => a.Id == id, alunoAtualizado);
            return resultado.MatchedCount > 0;
        }

        public async Task<bool> RemoverAsync(string id)
        {
            var resultado = await _colecaoAlunos.DeleteOneAsync(a => a.Id == id);
            return resultado.DeletedCount > 0;
        }
    }
}
