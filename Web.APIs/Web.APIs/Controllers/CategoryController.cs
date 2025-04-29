using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Application.DTOs.CategoryDTO;
using Web.Application.Interfaces;

namespace Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCategory(AddGategoryDto addGategoryDto)
        {
            var Cate = _categoryService.CreateCategoryAsync(addGategoryDto);
            return Ok(Cate);
        }
        [HttpGet]
        public async Task<IActionResult> GatAllCategoriesAsync()
        {
            var Cate =await _categoryService.GetAllCategory();
            return Ok(Cate);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryByIdAsync([FromRoute] int id)
        {
            var Cate = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(Cate);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] int id)
        {
            var Cate =await  _categoryService.DeleteCategoryAsync(id);
            return Ok(Cate);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryAsync([FromRoute]int id,AddGategoryDto addGategoryDto)
        {
            var Cate =await _categoryService.UpdateCategoryAsync(id,addGategoryDto);
            return Ok(Cate);
        }

    }
}
