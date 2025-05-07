using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.DTOs.TaskDTO;
using Web.Application.Interfaces;
using Web.Application.Response;
using Web.Infrastructure.Data;

namespace Web.Infrastructure.Service
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _DbContext;
        public TaskService(AppDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public Task<BaseResponse<GetTaskDto>> CreateCategoryAsync(AddTaskDto addTaskDto)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> DeleteCategoryAsync(int TaskId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<List<GetTaskDto>>> GetAllCategory()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<GetTaskDto>> GetCategoryByIdAsync(int TaskId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<GetTaskDto>> UpdateCategoryAsync(int id, AddTaskDto addTaskDto)
        {
            throw new NotImplementedException();
        }
    }
}
