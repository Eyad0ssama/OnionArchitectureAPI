using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onion.APIs.DTOs;
using Onion.APIs.Errors;
using Onion.APIs.Helper;
using Onion.Core.Entities;
using Onion.Core.Repositories;
using Onion.Core.Specification;
using Onion.Repository;

namespace Onion.APIs.Controllers
{

    public class ProductController : APIBaseController
    {
        private readonly IGenaricRepository<Product> _productRepo;
        private readonly IMapper _mapper;
        private readonly IGenaricRepository<ProductType> typeRepo;
        private readonly IGenaricRepository<ProductBrand> _brandRepo;

        public ProductController(IGenaricRepository<Product> productRepo, IMapper mapper,
            IGenaricRepository<ProductType>TypeRepo,IGenaricRepository<ProductBrand>brandRepo)
        {
            _productRepo = productRepo;
            _mapper = mapper;
            typeRepo = TypeRepo;
            _brandRepo = brandRepo;
        }

        //GetAll

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDTO>>> Getproducts([FromQuery]ProductSpecParams Params)
        {
            var spec = new ProductWithProductBrandAndType(Params);
            var products = await _productRepo.GetAllWithspec(spec);
            var MappedProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products);
            var CountSpec = new ProductWithFiltrationForCountAsync(Params);
            var Count = await _productRepo.GetCountWithSpecAsync(CountSpec);
            return Ok(new Pagination<ProductToReturnDTO>(Params.PageIndex, Params.PageSize, MappedProducts,Count));
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProductToReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var spec = new ProductWithProductBrandAndType(id);
            var product = await _productRepo.GetByIdWithSpecAsync(spec);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var types = await typeRepo.GetAll();
                        return Ok(types);
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _brandRepo.GetAll();
            return Ok(brands);
        }
    }       
}
