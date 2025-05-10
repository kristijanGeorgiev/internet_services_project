using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComputerStore.Domain.Entities;
using ComputerStore.Infrastructure.Data;
using AutoMapper;
using ComputerStore.Application.Interfaces;
using ComputerStore.Application.Services;
using ComputerStore.Application.DTOs;
using ComputerStore.Infrastructure.Migrations;

namespace ComputerStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
            return Ok(categoriesDto);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound(new { message = "Category not found" });

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }

        
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryService.CreateAsync(category);

            var createdDto = _mapper.Map<CategoryDto>(category);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, createdDto);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            category.Id = id;

            var success = await _categoryService.UpdateAsync(id, category);
            if (!success)
                return NotFound(new { message = "Category not found for update." });
            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _categoryService.DeleteAsync(id);
            if (deleted == null)
                return NotFound(new { message = "Product not found for deletion." });

            return NoContent();
        }
    }
}