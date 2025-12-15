using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.Exceptions;
using Service.Service.Interfaces;
using Service.ViewModels.Brand;

namespace Service.Service
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IFileService fileService, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(BrandCreateVM vm, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;
            if(vm.Image.FileName.Length > 100)
            {
                modelState.AddModelError("Image", "Image file name must be less than 100 characters");
                return false;
            }
            var brand = _mapper.Map<Brand>(vm);
            brand.CreatedDate = DateTime.Now;
            string fileName = await _fileService.UploadAsync(vm.Image, "admin/uploads/brands");
            brand.Image = fileName;
            await _brandRepository.AddAsync(brand);
            await _brandRepository.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var brand = await _brandRepository.Find(id).FirstOrDefaultAsync();
            if (brand is null)
                throw new CustomException(404, "Brand not found");
            _fileService.Delete(brand.Image, "admin/uploads/brands");
            _brandRepository.Delete(brand);
            await _brandRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<BrandVM>> GetAllAsync()
        {
            var brands = await _brandRepository.GetAll().Include(b=>b.Products).ToListAsync();
            var brandVMs = _mapper.Map<IEnumerable<BrandVM>>(brands);
            return brandVMs;
        }

        public async Task<BrandVM> GetAsync(int id)
        {
            var brand = await _brandRepository.Find(id).Include(b => b.Products).FirstOrDefaultAsync();
            if (brand is null)
                throw new CustomException(404, "Brand not found");
            var brandVm = _mapper.Map<BrandVM>(brand);
            return brandVm;
        }

        public async Task<BrandUpdateVM> GetUpdatedVmAsync(int id)
        {
            var brand = await _brandRepository.Find(id).Include(b => b.Products).FirstOrDefaultAsync();
            if (brand is null)
                throw new CustomException(404, "Brand not found");

            var vm = _mapper.Map<BrandUpdateVM>(brand);

            return vm;
        }

        public async Task<bool> UpdateAsync(BrandUpdateVM vm, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;
            var brand = await _brandRepository.Find(vm.Id).FirstOrDefaultAsync();
            if (brand == null) throw new CustomException(404, "Brand not found");
            var image = brand.Image;

            _mapper.Map(vm, brand);

            if (vm.Image != null)
            {
                if (vm.Image.FileName.Length > 100)
                {
                    modelState.AddModelError("Image", "Image file name must be less than 100 characters");
                    return false;
                }
                _fileService.Delete(brand.Image, "admin/uploads/brands");

                brand.Image = await _fileService.UploadAsync(vm.Image, "admin/uploads/brands");
            }
            else
            {
                brand.Image = image;
            }

            await _brandRepository.SaveChangesAsync();
            return true;
        }
        public IQueryable<BrandVM> GetBrandsQuery()
        {
            var query = _brandRepository.GetAll()
                .Include(b => b.Products)
                .Select(b => new BrandVM
                {
                    Id = b.Id,
                    Name = b.Name,
                    Image = b.Image,
                    CreatedDate = b.CreatedDate,
                    Products = b.Products.Select(p => new ProductInBrandVM
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price
                    })
                });
            return query;
        }
    }
}
