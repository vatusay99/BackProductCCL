using ApiInventarioCCL.Models;
using ApiInventarioCCL.Models.Dtos;
using ApiInventarioCCL.Repositories.Irepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiInventarioCCL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetProducts()
        {
            var listProducts = _productRepository.GetProducs();

            var listProductsDto = new List<ProductDto>();

            foreach (var product in listProducts) 
            {
                listProductsDto.Add(_mapper.Map<ProductDto>(product));
            }

            return Ok(listProductsDto);

        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProduct(int id)
        {
            var itemProduct = _productRepository.GetProductById(id);

            if(itemProduct == null)
            {
                return NotFound();
            }

            var itemProductDto = _mapper.Map<ProductDto>(itemProduct);

            return Ok(itemProductDto);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (createProductDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_productRepository.ExistProductByName(createProductDto.name))
            {
                ModelState.AddModelError("", $"El nombre del producto ya existe.");
                return StatusCode(400, ModelState);
            }
            var itemProductDto = _mapper.Map<Producto>(createProductDto);

            if (!_productRepository.CreateProduct(itemProductDto))
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro:s {itemProductDto.name}.");
                return StatusCode(400, ModelState);
            }


            return CreatedAtRoute("GetProduct", new {id = itemProductDto.Id}, itemProductDto);

        }

        [HttpPatch("{id:int}", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (productDto == null || id != productDto.Id)
            {
                return BadRequest(ModelState);
            }

            var productExist = _productRepository.ExistProductById(id);
            if (!productExist) { return NotFound($"No se encontro el Id: {productDto.Id}"); }

            var itemProductDto = _mapper.Map<Producto>(productDto);

            if (!_productRepository.UpdateProduct(itemProductDto))
            {
                ModelState.AddModelError("", $"Algo salio mal Actualizando el registro: {itemProductDto.name}.");
                return StatusCode(400, ModelState);
            }


            return NoContent();

        }

        [HttpDelete("{id:int}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteProduct(int id)
        {
            if (!_productRepository.ExistProductById(id)) 
            {
                return NotFound();
            }
            var itemProduct = _productRepository.GetProductById(id);

            if (!_productRepository.DeleteProduct(itemProduct.Id))
            {
                ModelState.AddModelError("", $"Algo salio mal borrando el registro: {itemProduct.name}.");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

    }
}
