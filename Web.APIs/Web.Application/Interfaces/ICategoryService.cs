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
        Task<BaseResponse<GetGategoryDto>> CreateCategoryAsync(AddGategoryDto  addCategoryDTO);
        Task<BaseResponse<GetGategoryDto>> GetCategoryByIdAsync(int categoryId);
        Task<BaseResponse<bool>> DeleteCategoryAsync(int categoryId);
        Task<BaseResponse<List<GetGategoryDto>>> GetAllCategory();
        Task<BaseResponse<GetGategoryDto>> UpdateCategoryAsync(int id, AddGategoryDto addCategoryDTO);
    }
}
