using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            List<ProductDto> list = new List<ProductDto>();

            //List<ProductDto> list3 = new();
            //var list2 = new List<ProductDto>();

            var response = await _productService.GetAllProductsAsync<ResponseDto>();

            if (response != null && response.IsSuccessful)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? productID)
        {
            if (productID.HasValue)
            {
                var response = await _productService.GetAllProductByIDAsync<ResponseDto>(productID.Value);
                var product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));

                return View(product);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductID > 0)
                    await _productService.UpdateProductAsync<ProductDto>(product);
                else
                    await _productService.CreateProductAsync<ProductDto>(product);

                var obj = new { data = product };
            }

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public JsonResult Delete(int? productID)
        {
            if (productID.HasValue)
                _productService.DeleteProductAsync<ProductDto>(productID.Value);

            return Json(true);
        }
    }
}
