using Microsoft.AspNetCore.Mvc;
using APIMongoDB.Services;     
using APIMongoDB.Model;

namespace APIMongoDB.Controllers
{
    [ApiController] // Indica que esta classe é um controller de API, habilitando validações automáticas.
    [Route("api/[controller]")] // Define a rota base para este controller: "api/alunos".
    public class AlunosController : Controller // Declara a classe do controller, herdando de Controller.
    {
        private readonly ServicoAlunos _servico; // Declara uma variável para acessar o serviço de alunos.

        // Construtor do controller, recebe o serviço de alunos via injeção de dependência.
        public AlunosController(ServicoAlunos servico)
        {
            _servico = servico; // Inicializa a variável privada com o serviço recebido.
        }

        // Método para obter todos os alunos cadastrados.
        [HttpGet] // Indica que responde a requisições HTTP GET na rota base.
        public async Task<ActionResult<List<Aluno>>> ObterTodos() =>
            await _servico.ObterTodosAsync(); // Chama o serviço para buscar todos os alunos e retorna o resultado.

        // Método para obter um aluno pelo ID.
        [HttpGet("{id:length(24)}", Name = "ObterAluno")] // Responde a GET em "api/alunos/{id}" onde id tem 24 caracteres.
        public async Task<ActionResult<Aluno>> ObterPorId(string id)
        {
            var aluno = await _servico.ObterPorIdAsync(id); // Busca o aluno pelo ID.
            return aluno is null ? NotFound() : aluno; // Se não encontrar, retorna 404. Se encontrar, retorna o aluno.
        }

        // Método para criar um novo aluno.
        [HttpPost] // Responde a requisições HTTP POST na rota base.
        public async Task<ActionResult<Aluno>> Criar(Aluno aluno)
        {
            await _servico.CriarAsync(aluno); // Chama o serviço para criar o aluno.
            // Retorna 201 (Created) e informa a rota para obter o aluno criado.
            return CreatedAtRoute("ObterAluno", new { id = aluno.Id }, aluno);
        }

        // Método para atualizar os dados de um aluno existente.
        [HttpPut("{id:length(24)}")] // Responde a PUT em "api/alunos/{id}".
        public async Task<IActionResult> Atualizar(string id, Aluno alunoAtualizado)
        {
            var alunoExistente = await _servico.ObterPorIdAsync(id); // Busca o aluno pelo ID.
            if (alunoExistente is null) return NotFound(); // Se não encontrar, retorna 404.

            alunoAtualizado.Id = alunoExistente.Id; // Garante que o ID não será alterado.
            var atualizado = await _servico.AtualizarAsync(id, alunoAtualizado); // Atualiza os dados do aluno.

            return atualizado ? NoContent() : NotFound(); // Se atualizar, retorna 204 (sem conteúdo). Se não, retorna 404.
        }

        // Método para remover um aluno pelo ID.
        [HttpDelete("{id:length(24)}")] // Responde a DELETE em "api/alunos/{id}".
        public async Task<IActionResult> Remover(string id)
        {
            var alunoExistente = await _servico.ObterPorIdAsync(id); // Busca o aluno pelo ID.
            if (alunoExistente is null) return NotFound(); // Se não encontrar, retorna 404.

            var removido = await _servico.RemoverAsync(id); // Remove o aluno do banco de dados.
            return removido ? NoContent() : NotFound(); // Se remover, retorna 204 (sucesso, sem corpo de resposta). Se não, retorna 404.
        }
    }
}
