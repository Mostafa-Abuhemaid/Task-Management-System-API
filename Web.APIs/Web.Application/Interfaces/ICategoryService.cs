using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.DTOs.CategoryDTO;
using Web.Application.Response;
using Web.Domain.Entites;

namespace Web.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<BaseResponse<GetGategoryDto>> CreateCategoryAsync(string userId, AddGategoryDto  addCategoryDTO);
        Task<BaseResponse<GetGategoryDto>> GetCategoryByIdAsync(string userId, int categoryId);
        Task<BaseResponse<bool>> DeleteCategoryAsync(string userId, int categoryId);
        Task<BaseResponse<List<GetGategoryDto>>> GetAllCategory(string userId );
        Task<BaseResponse<GetGategoryDto>> UpdateCategoryAsync(string userId, int id, AddGategoryDto addCategoryDTO);
    }
}
