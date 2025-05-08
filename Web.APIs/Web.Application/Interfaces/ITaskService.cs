using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.DTOs.TaskDTO;
using Web.Application.Response;

namespace Web.Application.Interfaces
{
    public interface ITaskService
    {
        Task<BaseResponse<GetTaskDto>> CreateTaskAsync(string UserId,AddTaskDto addTaskDto);
        Task<BaseResponse<GetTaskDto>> GetTaskByIdAsync(int TaskId);
        Task<BaseResponse<bool>> DeleteTaskAsync(int TaskId);
        Task<BaseResponse<List<GetTaskDto>>> GetAllTasks();
        Task<BaseResponse<GetTaskDto>> UpdateTaskAsync(int id, AddTaskDto addTaskDto);

    }
}
