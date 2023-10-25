namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {   private readonly IProdutoApplication _application;
        public ProdutoController(IProdutoApplication application) => _application = application;
        
        [HttpGet("todos")]
        public async Task<ActionResult> Get()
        {
            try
            {
                var produtos = await _application.GetAllProdutosAsync();

                if (produtos?.Count() <= 0)
                    return NoContent();
                else
                    return Ok(produtos);
            }
            catch(Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"Não foi possível retornar os produtos. Erro: {ex.Message}");
            }
        }
        [HttpGet("{IdProduto}")]
        public async Task<ActionResult> Get(int IdProduto)
        {
            try
            {
                var produto = await _application.GetByIdProduto(IdProduto);

                if (produto != null)
                    return Ok(produto);
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possível retornar o produto. Erro: {ex.Message}");
            }
        }
        [HttpPost("cadastrar")]
        public async Task<ActionResult> Post(ProdutoDTO model)
        {
            try
            {  
                    var produto = await _application.GetByNomeProduto(model.Nome);
                    if (produto != null) return BadRequest("Já existe um produto com esse mesmo nome");

                    var newproduto = await _application.Add(model);

                    if (newproduto != null)
                        return Ok(newproduto);
                    else
                        return BadRequest("Erro ao cadastrar o produto");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possível cadastrar o produto. Erro: {ex.Message}");
            }
        }
        [HttpPut("alterar/{IdProduto}")]
        public async Task<ActionResult> Put(ProdutoDTO model, int IdProduto)
        {
            try
            {
                
                var produto = await _application.GetByIdProduto(IdProduto);
                if (produto == null) return BadRequest("Produto Inválido");

                var result = await _application.Update(model, IdProduto);
                if (result == null) return BadRequest("Erro ao alterar o produto");

                return Ok(produto);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possível alterar o produto. Erro {ex.Message}");
            }
        }
        [HttpDelete("deletar/{IdProduto}")]
        public async Task<ActionResult> Delete(int IdProduto)
        {
            try
            {
                var produto = await _application.GetByIdProduto(IdProduto);
                if (produto == null) return BadRequest("Produto Inválido");

                return await _application.Delete(IdProduto) ? Ok("Deletado") :
                BadRequest("Ocorreu um problema não especifico ao tentar deletar o produto");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possível deletar o produto. Erro {ex.Message}");
            }
        }
    }
}
