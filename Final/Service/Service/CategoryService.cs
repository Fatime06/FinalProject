using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
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
        private readonly IFileService _fileService;

        public CategoryService(ICategoryRepository catRepo, IMapper mapper, IFileService fileService)
        {
            _catRepo = catRepo;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<bool> CreateAsync(CategoryCreateVM vm, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;
            if (await _catRepo.IsExistAsync(vm.Name.Trim()))
            {
                modelState.AddModelError("Name", "This category already exist");
                return false;
            }
            var category = _mapper.Map<Category>(vm);
            category.Name = category.Name.Trim();
            category.Image = await _fileService.UploadAsync(vm.Image, "admin/uploads/categories");
            category.CreatedDate = DateTime.Now;
            await _catRepo.AddAsync(category);
            await _catRepo.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _catRepo.Find(id).Include(c=>c.Products).FirstOrDefaultAsync();
            if (category is null)
                throw new CustomException(404, "Category not found");
            _catRepo.Delete(category);
            await _catRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryVM>> GetAllAsync()
        {
            var categories = await _catRepo.GetAll().Include(c => c.Products).ThenInclude(p => p.Ratings).ToListAsync();
            var categoryDtos = _mapper.Map<IEnumerable<CategoryVM>>(categories);
            return categoryDtos;
        }

        public async Task<CategoryVM> GetAsync(int id)
        {
            var category = await _catRepo.Find(id).Include(c => c.Products).ThenInclude(p => p.Ratings).FirstOrDefaultAsync();
            if (category is null)
                throw new CustomException(404, "Category not found");
            var categoryVm = _mapper.Map<CategoryVM>(category);
            return categoryVm;
        }

        public async Task<CategoryUpdateVM> GetUpdatedVmAsync(int id)
        {
            var category = await _catRepo.Find(id).Include(c=>c.Products).ThenInclude(p=>p.Ratings).FirstOrDefaultAsync();
            if (category is null)
                throw new CustomException(404, "Category not found");

            var vm = _mapper.Map<CategoryUpdateVM>(category);

            return vm;
        }

        public async Task<bool> UpdateAsync(CategoryUpdateVM vm, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;
            var existCategory = await _catRepo.Find(vm.Id).Include(c => c.Products).ThenInclude(p => p.Ratings).FirstOrDefaultAsync();
            if (existCategory is null) throw new CustomException(404, "Category not found");
            if (await _catRepo.IsExistAsync(vm.Name.Trim()) && _catRepo.GetCategoryByNameAsync(vm.Name).Result.Id != vm.Id)
            {
                modelState.AddModelError("Name", "This category already exists");
                return false;
            }
            var image = existCategory.Image;
            existCategory = _mapper.Map(vm, existCategory);
            if (vm.Image != null)
            {
                _fileService.Delete(existCategory.Image, "admin/uploads/categories");

                existCategory.Image = await _fileService.UploadAsync(vm.Image, "admin/uploads/categories");
            }
            else
            {
                existCategory.Image = image;
            }
            _catRepo.Update(existCategory);
            await _catRepo.SaveChangesAsync();
            return true;
        }
        public async Task<CategoryVM> GetCategoryByName(string categoryName)
        {
            var category = await _catRepo.GetCategoryByNameAsync(categoryName);
            if (category == null) throw new CustomException(404, "Category not found");
            return _mapper.Map<CategoryVM>(category);
        }
        public IQueryable<CategoryVM> GetCategoriesQuery()
        {
            return _catRepo.GetAll().Select(c => _mapper.Map<CategoryVM>(c));
        }
    }
}
