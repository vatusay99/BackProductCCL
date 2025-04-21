using ApiInventarioCCL.Models;
using ApiInventarioCCL.Models.Dtos;
using ApiInventarioCCL.Repositories;
using ApiInventarioCCL.Repositories.Irepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiInventarioCCL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoveProductController : ControllerBase
    {
        private readonly IMoveProductRepository _moveProductRepository;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public MoveProductController(IMoveProductRepository imoveProductRepository, IMapper mapper, IProductRepository productRepository)
        {
            _moveProductRepository = imoveProductRepository; 
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetMoveProducts()
        {
            var listMove = _moveProductRepository.GetMoveProduct();

            var listMoveProductsDto = new List<MoveproductDto>();

            foreach (var move in listMove)
            {
                listMoveProductsDto.Add(_mapper.Map<MoveproductDto>(move));
            }

            return Ok(listMoveProductsDto);

        }

        [HttpGet("{id:int}", Name = "GetMoveById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMoveById(int id)
        {
            var itemMove = _moveProductRepository.GetMoveProductById(id);

            if (itemMove == null)
            {
                return NotFound();
            }

            var itemMoveDto = _mapper.Map<ProductDto>(itemMove);

            return Ok(itemMoveDto);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateMoveProduct([FromBody] CreateMoveproductDto createMoveproductDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (createMoveproductDto == null)
            {
                return BadRequest(ModelState);
            }

            var itemCantidadProduct = _productRepository.GetProductById(createMoveproductDto.productId);
            if(createMoveproductDto.Moveproduct == 0)
            {
                if (itemCantidadProduct.amount < createMoveproductDto.amountMove)
                {
                    ModelState.AddModelError("", $"La cantidad de salida no puede ser mayor a la cantida en bodega.");
                    return StatusCode(400, ModelState);
                }
                else
                {
                    var amount = itemCantidadProduct.amount - createMoveproductDto.amountMove;
                    itemCantidadProduct.amount = amount;
                    _productRepository.UpdateProduct(itemCantidadProduct);
                }
            }else if(createMoveproductDto.Moveproduct != 0)
            {
                var amount = itemCantidadProduct.amount + createMoveproductDto.amountMove;
                itemCantidadProduct.amount = amount;
                _productRepository.UpdateProduct(itemCantidadProduct);
            }


                var itemProductDto = _mapper.Map<MoveProduct>(createMoveproductDto);

            if (!_moveProductRepository.CreateMoveProduct(itemProductDto))
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro: {itemProductDto.Producto.Id}.");
                return StatusCode(400, ModelState);
            }


            return CreatedAtRoute("GetMoveById", new { id = itemProductDto.Id }, itemProductDto);
        }


    }
}
