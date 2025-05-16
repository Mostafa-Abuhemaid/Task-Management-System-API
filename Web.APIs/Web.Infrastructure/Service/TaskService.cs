using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.DTOs.TaskDTO;
using Web.Application.Interfaces;
using Web.Application.Response;
using Web.Domain.Entites;
using Web.Domain.Hubs;
using Web.Infrastructure.Data;

namespace Web.Infrastructure.Service
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _DbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly INotificationService _notificationService;
        public TaskService(AppDbContext dbContext, UserManager<AppUser> userManager, INotificationService notificationService)
        {
            _DbContext = dbContext;
            _userManager = userManager;
            _notificationService = notificationService;
        }

        public async Task<BaseResponse<GetTaskDto>> CreateTaskAsync(string UserId, AddTaskDto addTaskDto)
        {
            var user= await _userManager.FindByIdAsync(UserId);
            if (user == null)
                return new BaseResponse<GetTaskDto>(false, "User Not Found");

            var task = new TaskItem
            {
                Title = addTaskDto.Title,
                Description = addTaskDto.Description,
                DueDate = addTaskDto.DueDate,
                CategoryId = addTaskDto.CategoryId,
                UserId = UserId,
                Status = Domain.Enums.Status.Pending
            };

            _DbContext.Tasks.Add(task);
            await _DbContext.SaveChangesAsync();

            BackgroundJob.Schedule(() => _notificationService.SendTaskReminder(task.Id), task.DueDate);
            return new BaseResponse<GetTaskDto>(true, "Task Created Successfully");
        }

        public async Task<BaseResponse<bool>> DeleteTaskAsync(int TaskId)
        {
            var task = await _DbContext.Tasks.FindAsync(TaskId);
            if (task == null)
                return new BaseResponse<bool>(false, "Task Not Found");

            _DbContext.Tasks.Remove(task);
            await _DbContext.SaveChangesAsync();

            return new BaseResponse<bool>(true, "Task Deleted Successfully", true);
        }

        public async Task<BaseResponse<List<GetTaskDto>>> GetAllTasks()
        {
            var tasks = await _DbContext.Tasks.Include(t => t.Category)  
        .Include(t => t.User).ToListAsync();
            if (tasks == null)
                return new BaseResponse<List<GetTaskDto>>(false, "No Taskes Found");
            var result = tasks.Select(task => new GetTaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Status = task.Status.ToString(),
                CategoryId = task.CategoryId,
                Category = task.Category.Name,
                UserName = task.User.UserName,
                UserId = task.UserId
            }).ToList();

            return new BaseResponse<List<GetTaskDto>>(true, "Tasks Retrieved Successfully", result);
        }

        public async Task<BaseResponse<GetTaskDto>> GetTaskByIdAsync(int TaskId)
        {
            var task = await _DbContext.Tasks.Include(t => t.Category)  // Load Category
        .Include(t => t.User).FirstOrDefaultAsync(t => t.Id == TaskId);
            if (task == null)
                return new BaseResponse<GetTaskDto>(false, "Task Not Found");

            var result = new GetTaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Status = task.Status.ToString(),
                CategoryId = task.CategoryId,
                Category=task.Category.Name,
                UserName=task.User.UserName,
                UserId = task.UserId
            };

            return new BaseResponse<GetTaskDto>(true, "Task Retrieved Successfully", result);
        }

        public async Task<BaseResponse<GetTaskDto>> UpdateTaskAsync(int id, AddTaskDto addTaskDto)
        {
            var task = await _DbContext.Tasks.FindAsync(id);
            if (task == null)
                return new BaseResponse<GetTaskDto>(false, "Task Not Found");

            task.Title = addTaskDto.Title;
            task.Description = addTaskDto.Description;
            task.DueDate = addTaskDto.DueDate;
            task.CategoryId = addTaskDto.CategoryId;

            await _DbContext.SaveChangesAsync();

            var result = new GetTaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Status = task.Status.ToString(),
                CategoryId = task.CategoryId,
                UserId = task.UserId
            };

            return new BaseResponse<GetTaskDto>(true, "Task Updated Successfully", result);
        }
    }
}
