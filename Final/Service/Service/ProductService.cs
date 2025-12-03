using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.Exceptions;
using Service.Service.Interfaces;
using Service.ViewModels.Product;

namespace Service.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _proRepo;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public ProductService(IProductRepository proRepo, IMapper mapper, IFileService fileService)
        {
            _proRepo = proRepo;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<bool> CreateAsync(ProductCreateVM vm, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;
            var product = _mapper.Map<Product>(vm);
            product.CreatedDate = DateTime.Now;
            if (product.Quantity > 0)
            {
                product.InStock = true;
            }
            else
            {
                product.InStock = false;
            }
            string fileName = await _fileService.UploadAsync(vm.Image, "admin/uploads/products");
            product.Image = fileName;
            await _proRepo.AddAsync(product);
            await _proRepo.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _proRepo.Find(id).FirstOrDefaultAsync();
            if (product is null)
                throw new CustomException(404, "Product not found");
            _fileService.Delete(product.Image, "admin/uploads/products");
            _proRepo.Delete(product);
            await _proRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductVM>> GetAllAsync()
        {
            var products = await _proRepo.GetAll().Include(p=>p.Ratings).Include(p=>p.Category).ToListAsync();
            var productsVMs = _mapper.Map<IEnumerable<ProductVM>>(products);
            return productsVMs;
        }

        public async Task<ProductVM> GetAsync(int id)
        {
            var product = await _proRepo.Find(id).Include(p => p.Ratings).Include(c => c.Category).FirstOrDefaultAsync();
            if (product is null)
                throw new CustomException(404, "Product not found");
            var productVm = _mapper.Map<ProductVM>(product);
            return productVm;
        }

        public async Task<ProductUpdateVM> GetUpdatedVmAsync(int id)
        {
            var product = await _proRepo.Find(id).Include(p => p.Ratings).Include(c => c.Category).FirstOrDefaultAsync();
            if (product is null)
                throw new CustomException(404, "Product not found");

            var vm = _mapper.Map<ProductUpdateVM>(product);

            return vm;
        }

        public async Task<bool> UpdateAsync(ProductUpdateVM vm, ModelStateDictionary modelState)
        {
            if(!modelState.IsValid) return false;
            var product = await _proRepo.Find(vm.Id).Include(p => p.Ratings).Include(p=>p.Category).FirstOrDefaultAsync();
            if (product == null) throw new CustomException(404, "Product not found");
            var image = product.Image;


            _mapper.Map(vm, product);

            if (product.Quantity > 0)
            {
                product.InStock = true;
            }
            else
            {
                product.InStock = false;
            }

            if (vm.Image != null) 
            {
                _fileService.Delete(product.Image, "admin/uploads/products");

                product.Image = await _fileService.UploadAsync(vm.Image, "admin/uploads/products");
            }
            else
            {
                product.Image = image;
            }

            await _proRepo.SaveChangesAsync();
            return true;
        }
    }
}
