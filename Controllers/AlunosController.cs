using Microsoft.AspNetCore.Mvc;
using APIMongoDB.Services;
using APIMongoDB.Model;

namespace APIMongoDB.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunosController : Controller
    {
        private readonly ServicoAlunos _servico;

        public AlunosController(ServicoAlunos servico)
        {
            _servico = servico;
        }

        [HttpGet]
        public async Task<ActionResult<List<Aluno>>> ObterTodos() =>
            await _servico.ObterTodosAsync();

        [HttpGet("{id:length(24)}", Name = "ObterAluno")]
        public async Task<ActionResult<Aluno>> ObterPorId(string id)
        {
            var aluno = await _servico.ObterPorIdAsync(id);
            return aluno is null ? NotFound() : aluno;
        }

        [HttpPost]
        public async Task<ActionResult<Aluno>> Criar(Aluno aluno)
        {
            await _servico.CriarAsync(aluno);
            return CreatedAtRoute("ObterAluno", new { id = aluno.Id }, aluno);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Atualizar(string id, Aluno alunoAtualizado)
        {
            var alunoExistente = await _servico.ObterPorIdAsync(id);
            if (alunoExistente is null) return NotFound();

            alunoAtualizado.Id = alunoExistente.Id;
            var atualizado = await _servico.AtualizarAsync(id, alunoAtualizado);

            return atualizado ? NoContent() : NotFound();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Remover(string id)
        {
            var alunoExistente = await _servico.ObterPorIdAsync(id);
            if (alunoExistente is null) return NotFound();

            var removido = await _servico.RemoverAsync(id);
            return removido ? NoContent() : NotFound();
        }
    }
}
