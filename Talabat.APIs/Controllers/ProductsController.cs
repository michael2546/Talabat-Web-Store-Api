using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{
    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProductType> _typeRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;

        public ProductsController(
                                      IGenericRepository<Product> productRepo 
                                    , IMapper mapper 
                                    , IGenericRepository<ProductType> TypeRepo 
                                    , IGenericRepository<ProductBrand> BrandRepo
                                 )
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _typeRepo = TypeRepo;
            _brandRepo = BrandRepo;
        }

        //Get All Products
        //Base URL/api/Products
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts(string? sort)
        {
            var spec = new ProductWithBrandAndTypeSpecification(sort);
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            var mappedProducts = _mapper.Map< IReadOnlyList<Product> , IReadOnlyList< ProductToReturnDto >>(products);
            //OkObjectResult result = new OkObjectResult(products);   //use ok helper method
            return Ok(mappedProducts);
        }

        //Get Product By Id
        //Base URL/api/Products/1
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto) , 200 )]
        [ProducesResponseType(typeof(ApiResponce), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var spec = new ProductWithBrandAndTypeSpecification(id);
            var product = await _productRepo.GetByIdWithSpecAsync(spec);
            if (product is null)
                return NotFound(new ApiResponce(404));

            var MappedProduct = _mapper.Map<Product , ProductToReturnDto>(product);
            return Ok(MappedProduct);
        }

        //GET ALL Types
        //{{BaseURL}}/api/Products/Types
        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var Types = await _typeRepo.GetAllAsync();
            return Ok(Types);
        }

        //Get All Brands
        //{{BaseURL}}/api/Products/Brands
        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _brandRepo.GetAllAsync();
            return Ok(brands);
        }



    }
}
