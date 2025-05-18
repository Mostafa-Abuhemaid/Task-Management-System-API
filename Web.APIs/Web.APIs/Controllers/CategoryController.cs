using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        [Authorize]
        public async Task<IActionResult> AddNewCategory(AddGategoryDto addGategoryDto)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var Cate =await _categoryService.CreateCategoryAsync(user, addGategoryDto);
            return Cate.Success ? Ok(Cate) : BadRequest(Cate);
        }
        [HttpGet]
        public async Task<IActionResult> GatAllCategoriesAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var Cate =await _categoryService.GetAllCategory(userId);
            return Cate.Success ? Ok(Cate) : BadRequest(Cate);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryByIdAsync([FromRoute] int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var Cate = await _categoryService.GetCategoryByIdAsync(userId, id);
            return Cate.Success ? Ok(Cate) : BadRequest(Cate);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var Cate =await  _categoryService.DeleteCategoryAsync(userId,id);
            return Cate.Success ? Ok(Cate) : BadRequest(Cate);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryAsync([FromRoute]int id, AddGategoryDto addGategoryDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var Cate =await _categoryService.UpdateCategoryAsync(userId, id, addGategoryDto);
            return Cate.Success ? Ok(Cate) : BadRequest(Cate);
        }

    }
}
