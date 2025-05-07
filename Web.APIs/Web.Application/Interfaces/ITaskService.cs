using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.DTOs.CategoryDTO;
using Web.Application.DTOs.TaskDTO;
using Web.Application.Response;

namespace Web.Application.Interfaces
{
    public interface ITaskService
    {
        Task<BaseResponse<GetTaskDto>> CreateCategoryAsync(AddTaskDto addTaskDto);
        Task<BaseResponse<GetTaskDto>> GetCategoryByIdAsync(int TaskId);
        Task<BaseResponse<bool>> DeleteCategoryAsync(int TaskId);
        Task<BaseResponse<List<GetTaskDto>>> GetAllCategory();
        Task<BaseResponse<GetTaskDto>> UpdateCategoryAsync(int id, AddTaskDto addTaskDto);

    }
}
