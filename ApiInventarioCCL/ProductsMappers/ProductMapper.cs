using ApiInventarioCCL.Models;
using ApiInventarioCCL.Models.Dtos;
using AutoMapper;

namespace ApiInventarioCCL.ProductsMapper
{
    public class ProductMapper: Profile
    {
        public ProductMapper()
        {
            CreateMap<Producto, ProductDto>().ReverseMap();
            CreateMap<Producto, CreateProductDto>().ReverseMap();

            CreateMap<MoveProduct, MoveproductDto>().ReverseMap();
            CreateMap<MoveProduct, CreateMoveproductDto>().ReverseMap();
        }
    }
}
