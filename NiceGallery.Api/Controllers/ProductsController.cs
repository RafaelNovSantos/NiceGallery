using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NiceGallery.Api.Data;
using NiceGallery.Api.DTOs;
using NiceGallery.Api.Models;


namespace NiceGallery.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ApplicationDbContext context, ILogger<ProductsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                var product = await _context.Products.ToListAsync();
                if (product == null || !product.Any())
                {
                    return NoContent();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar os produtos.");
                return StatusCode(500, new { mensagem = "Erro interno ao buscar os produtos. Por favor, tente novamente mais tarde." });
            }
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                    return NotFound(new { mensagem = "Produto não encontrado." });

                return Ok(product);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar o produto com ID {id}.");
                return StatusCode(500, new { mensagem = "Erro interno ao buscar o produto. Por favor, tente novamente mais tarde." });
            }
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Criar o produto com os dados do DTO
                var product = new Product
                {
                    Name = dto.Name,
                    Price = dto.Price
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                // Retornar o produto com o id gerado
                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, new
                {
                    mensagem = "Produto cadastrado com sucesso!",
                    product
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cadastrar o produto.");
                return StatusCode(500, new { mensagem = "Erro interno ao salvar o produto. Por favor, tente novamente mais tarde." });
            }
        }



        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest(new { mensagem = "O ID do produto não corresponde ao ID fornecido na URL." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { mensagem = "Produto atualizado com sucesso!" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                    return NotFound(new { mensagem = "Produto não encontrado para atualização." });

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar o produto com ID {id}.");
                return StatusCode(500, new { mensagem = "Erro interno ao atualizar o produto. Por favor, tente novamente mais tarde." });
            }
        }


        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound(new { mensagem = "Produto não encontrado para exclusão." });
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return Ok(new { mensagem = "Produto excluído com sucesso!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao excluir o produto com ID {id}.");
                return StatusCode(500, new { mensagem = "Erro interno ao excluir o produto. Por favor, tente novamente mais tarde." });
            }
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
