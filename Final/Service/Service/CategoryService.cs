using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Repository.Repositories.Interfaces;
using Service.DTOs.Category;
using Service.Exceptions;
using Service.Service.Interfaces;

namespace Service.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _catRepo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository catRepo, IMapper mapper)
        {
            _catRepo = catRepo;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(CategoryCreateDto dto, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;
            if (await _catRepo.IsExistAsync(dto.Name.Trim()))
            {
                modelState.AddModelError("Name", "This category already exist");
                return false;
            }
            var category = _mapper.Map<Category>(dto);
            category.Name = category.Name.Trim();
            category.CreatedDate = DateTime.Now;
            await _catRepo.AddAsync(category);
            await _catRepo.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _catRepo.FindAsync(id);
            if (category is null)
                throw new CustomException(404, "Category not found");
            _catRepo.Delete(category);
            await _catRepo.SaveChangesAsync();
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var categories = await _catRepo.GetAll();
            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
            return categoryDtos;
        }

        public async Task<CategoryDto> GetAsync(int id)
        {
            var category = await _catRepo.FindAsync(id);
            if (category is null)
                throw new CustomException(404, "Kateqoriya tapılmadı");
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }

        public async Task<CategoryUpdateDto> GetUpdatedDtoAsync(int id)
        {
            var category = await _catRepo.FindAsync(id);
            if (category is null)
                throw new CustomException(404, "Category not found");

            var dto = _mapper.Map<CategoryUpdateDto>(category);

            return dto;
        }

        public async Task<bool> UpdateAsync(CategoryUpdateDto dto, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;
            var existCategory = await _catRepo.FindAsync(dto.Id);
            if (existCategory is null) throw new CustomException(404, "Category not found");
            if (await _catRepo.IsExistAsync(dto.Name.Trim()) && _catRepo.GetCategoryByNameAsync(dto.Name).Result.Id != dto.Id)
            {
                modelState.AddModelError("Name", "This category already exists");
                return false;
            }
            existCategory = _mapper.Map(dto, existCategory);
            _catRepo.Update(existCategory);
            await _catRepo.SaveChangesAsync();
            return true;
        }
    }
}
