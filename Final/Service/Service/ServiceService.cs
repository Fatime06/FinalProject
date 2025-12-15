using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Exceptions;
using Service.Service.Interfaces;
using Service.ViewModels.Brand;
using Service.ViewModels.Service;

namespace Service.Service
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepo;
        private readonly IMapper _mapper;

        public ServiceService(IServiceRepository serviceRepo, IMapper mapper)
        {
            _serviceRepo = serviceRepo;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(ServiceCreateVM vm, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;
            var service = _mapper.Map<Domain.Entities.Service>(vm);
            service.CreatedDate = DateTime.Now;
            await _serviceRepo.AddAsync(service);
            await _serviceRepo.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var service = await _serviceRepo.Find(id).FirstOrDefaultAsync();
            if (service is null)
                throw new CustomException(404, "Service not found");
            _serviceRepo.Delete(service);
            await _serviceRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<ServiceVM>> GetAllAsync()
        {
            var services = await _serviceRepo.GetAll().ToListAsync();
            var servicesVms = _mapper.Map<IEnumerable<ServiceVM>>(services);
            return servicesVms;
        }

        public async Task<ServiceVM> GetAsync(int id)
        {
            var service = await _serviceRepo.Find(id).FirstOrDefaultAsync();
            if (service is null)
                throw new CustomException(404, "Service not found");
            var serviceVm = _mapper.Map<ServiceVM>(service);
            return serviceVm;
        }

        public async Task<ServiceUpdateVM> GetUpdatedVmAsync(int id)
        {
            var service = await _serviceRepo.Find(id).FirstOrDefaultAsync();
            if (service is null)
                throw new CustomException(404, "Service not found");

            var vm = _mapper.Map<ServiceUpdateVM>(service);

            return vm;
        }

        public async Task<bool> UpdateAsync(ServiceUpdateVM vm, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;
            var service = await _serviceRepo.Find(vm.Id).FirstOrDefaultAsync();
            if (service == null) throw new CustomException(404, "Service not found");

            _mapper.Map(vm, service);

            await _serviceRepo.SaveChangesAsync();
            return true;
        }
        public IQueryable<ServiceVM> GetServicesQuery()
        {
            return _serviceRepo.GetAll().Select(s => _mapper.Map<ServiceVM>(s));
        }
    }
}
