using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
            var Cate =await _categoryService.CreateCategoryAsync(addGategoryDto);
            return Cate.Success ? Ok(Cate) : BadRequest(Cate);
        }
        [HttpGet]
        public async Task<IActionResult> GatAllCategoriesAsync()
        {
            var Cate =await _categoryService.GetAllCategory();
            return Cate.Success ? Ok(Cate) : BadRequest(Cate);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryByIdAsync([FromRoute] int id)
        {
            var Cate = await _categoryService.GetCategoryByIdAsync(id);
            return Cate.Success ? Ok(Cate) : BadRequest(Cate);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] int id)
        {
            var Cate =await  _categoryService.DeleteCategoryAsync(id);
            return Cate.Success ? Ok(Cate) : BadRequest(Cate);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryAsync([FromRoute]int id,AddGategoryDto addGategoryDto)
        {
            var Cate =await _categoryService.UpdateCategoryAsync(id,addGategoryDto);
            return Cate.Success ? Ok(Cate) : BadRequest(Cate);
        }

    }
}
