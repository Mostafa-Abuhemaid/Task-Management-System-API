﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.DTOs.CategoryDTO;
using Web.Application.Interfaces;
using Web.Application.Response;
using Web.Domain.Entites;
using Web.Infrastructure.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Web.Infrastructure.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _DbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public CategoryService(AppDbContext dbContext, IMapper mapper, UserManager<AppUser> userManager)
        {
            _DbContext = dbContext;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<BaseResponse<GetGategoryDto>> CreateCategoryAsync(string userId, AddGategoryDto addCategoryDTO)
        {
            var User = await _userManager.FindByIdAsync(userId);
            if (User == null)
                return new BaseResponse<GetGategoryDto>(false, $"No user with this id : {userId}");
            if (addCategoryDTO == null)
            {
                return new BaseResponse<GetGategoryDto>(false, "You Can't Add Empty Category");
            }
         var category = new Category()
         {
             Name = addCategoryDTO.Name,
             UserId= userId
         };
         await   _DbContext.AddAsync(category);
           await _DbContext.SaveChangesAsync();
            var CategoryDto= _mapper.Map<GetGategoryDto>(category);
            return new BaseResponse<GetGategoryDto>(true, "The new Categoty had Added successfuly", CategoryDto);
        }

        public async Task<BaseResponse<bool>> DeleteCategoryAsync(string userId,int categoryId)
        {
            var User = await _userManager.FindByIdAsync(userId);
            if (User == null)
                return new BaseResponse<bool>(false, $"No user with this id : {userId}");
            var Cat = _DbContext.Categories.FirstOrDefault(c=> c.Id == categoryId);
            if(Cat == null)
            {
                return new BaseResponse<bool>(false, $"No Category has id {categoryId}");
            }
            _DbContext.Remove(Cat);
            await _DbContext.SaveChangesAsync();
            return new BaseResponse<bool>(true, "The Categoty Deletes successfuly");
           
        }

        public async Task<BaseResponse<List<GetGategoryDto>>> GetAllCategory(string userId)
        {
            var User = await _userManager.FindByIdAsync(userId);
            if (User == null)
                return new BaseResponse<List<GetGategoryDto>>(false, $"No user with this id : {userId}");
            var Cate = await _DbContext.Categories.Where(c=>c.UserId==userId).ToListAsync();
            if(Cate == null)
            {
                return new BaseResponse<List<GetGategoryDto>>(true, "No Category Found");
            }
            var CategoryDto = _mapper.Map<List<GetGategoryDto>>(Cate);

            return new BaseResponse<List< GetGategoryDto>>(true, "Categories", CategoryDto);
        }

        public async Task<BaseResponse<GetGategoryDto>> GetCategoryByIdAsync(string userId,int categoryId)
        {
            var Cat = _DbContext.Categories.Where(c => c.UserId == userId).FirstOrDefault(c => c.Id == categoryId);
            if (Cat == null)
            {
                return new BaseResponse<GetGategoryDto>(false, $"No Category has id {categoryId}");
            }
            var CategoryDto = _mapper.Map<GetGategoryDto>(Cat);

            return new BaseResponse<GetGategoryDto>(true, "Category", CategoryDto);
        }

        public async Task<BaseResponse<GetGategoryDto>> UpdateCategoryAsync(string userId,int id, AddGategoryDto addCategoryDTO)
        {
            var User = await _userManager.FindByIdAsync(userId);
            if (User == null)
                return new BaseResponse<GetGategoryDto>(false, $"No user with this id : {userId}");
            var Cat = _DbContext.Categories.Where(c => c.UserId == userId).FirstOrDefault(c => c.Id == id);
            if (Cat == null)
            {
                return new BaseResponse<GetGategoryDto>(false, $"No Category has id {id}");
            }
            Cat.Name=addCategoryDTO.Name;
            await _DbContext.SaveChangesAsync();
            var CategoryDto = _mapper.Map<GetGategoryDto>(Cat);
            return new BaseResponse<GetGategoryDto>(true, "The Catecory Updated successfuly", CategoryDto);
        }
    }
}
