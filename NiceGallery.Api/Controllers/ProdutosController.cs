using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NiceGallery.Api.Data;
using NiceGallery.Api.Models;


namespace NiceGallery.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProdutosController> _logger;

        public ProdutosController(ApplicationDbContext context, ILogger<ProdutosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProdutos()
        {
            try
            {
                var produtos = await _context.Produtos.ToListAsync();
                if (produtos == null || !produtos.Any())
                {
                    return NotFound(new { mensagem = "Nenhum produto encontrado." });
                }

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar os produtos.");
                return StatusCode(500, new { mensagem = "Erro interno ao buscar os produtos. Por favor, tente novamente mais tarde." });
            }
        }

        // GET: api/Produtos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduto(int id)
        {
            try
            {
                var produto = await _context.Produtos.FindAsync(id);

                if (produto == null)
                    return NotFound(new { mensagem = "Produto não encontrado." });

                return Ok(produto);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar o produto com ID {id}.");
                return StatusCode(500, new { mensagem = "Erro interno ao buscar o produto. Por favor, tente novamente mais tarde." });
            }
        }

        // POST: api/Produtos
        [HttpPost]
        public async Task<IActionResult> PostProduto(Product produto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Produtos.Add(produto);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, new
                {
                    mensagem = "Produto cadastrado com sucesso!",
                    produto
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cadastrar o produto.");
                return StatusCode(500, new { mensagem = "Erro interno ao salvar o produto. Por favor, tente novamente mais tarde." });
            }
        }


        // PUT: api/Produtos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, Product produto)
        {
            if (id != produto.Id)
                return BadRequest(new { mensagem = "O ID do produto não corresponde ao ID fornecido na URL." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { mensagem = "Produto atualizado com sucesso!" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
                    return NotFound(new { mensagem = "Produto não encontrado para atualização." });

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar o produto com ID {id}.");
                return StatusCode(500, new { mensagem = "Erro interno ao atualizar o produto. Por favor, tente novamente mais tarde." });
            }
        }


        // DELETE: api/Produtos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            try
            {
                var produto = await _context.Produtos.FindAsync(id);
                if (produto == null)
                {
                    return NotFound(new { mensagem = "Produto não encontrado para exclusão." });
                }

                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();

                return Ok(new { mensagem = "Produto excluído com sucesso!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao excluir o produto com ID {id}.");
                return StatusCode(500, new { mensagem = "Erro interno ao excluir o produto. Por favor, tente novamente mais tarde." });
            }
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}
