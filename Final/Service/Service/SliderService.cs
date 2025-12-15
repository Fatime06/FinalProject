using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.Exceptions;
using Service.Service.Interfaces;
using Service.ViewModels.Slider;

namespace Service.Service
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepo;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public SliderService(ISliderRepository sliderRepo, IMapper mapper, IFileService fileService)
        {
            _sliderRepo = sliderRepo;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<bool> CreateAsync(SliderCreateVM vm, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;
            var slider = _mapper.Map<Slider>(vm);
            slider.CreatedDate = DateTime.Now;
            string fileName = await _fileService.UploadAsync(vm.Image, "admin/uploads/sliders");
            slider.Image = fileName;
            await _sliderRepo.AddAsync(slider);
            await _sliderRepo.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var slider = await _sliderRepo.Find(id).FirstOrDefaultAsync();
            if (slider is null)
                throw new CustomException(404, "Slider not found");
            _fileService.Delete(slider.Image, "admin/uploads/sliders");
            _sliderRepo.Delete(slider);
            await _sliderRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<SliderVM>> GetAllAsync()
        {
            var sliders = await _sliderRepo.GetAll().ToListAsync();
            var sliderVMs = _mapper.Map<IEnumerable<SliderVM>>(sliders);
            return sliderVMs;
        }

        public async Task<SliderVM> GetAsync(int id)
        {
            var slider = await _sliderRepo.Find(id).FirstOrDefaultAsync();
            if (slider is null)
                throw new CustomException(404, "Slider not found");
            var sliderVm = _mapper.Map<SliderVM>(slider);
            return sliderVm;
        }

        public async Task<SliderUpdateVM> GetUpdatedVmAsync(int id)
        {
            var slider = await _sliderRepo.Find(id).FirstOrDefaultAsync();
            if (slider is null)
                throw new CustomException(404, "Slider not found");

            var vm = _mapper.Map<SliderUpdateVM>(slider);

            return vm;
        }

        public async Task<bool> UpdateAsync(SliderUpdateVM vm, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;
            var slider = await _sliderRepo.Find(vm.Id).FirstOrDefaultAsync();
            if (slider == null) throw new CustomException(404, "Slider not found");
            var image = slider.Image;

            _mapper.Map(vm, slider);

            if (vm.Image != null)
            {
                _fileService.Delete(slider.Image, "admin/uploads/sliders");

                slider.Image = await _fileService.UploadAsync(vm.Image, "admin/uploads/sliders");
            }
            else
            {
                slider.Image = image;
            }

            await _sliderRepo.SaveChangesAsync();
            return true;
        }
        public IQueryable<SliderVM> GetSlidersQuery()
        {
            return _sliderRepo.GetAll().Select(s => _mapper.Map<SliderVM>(s));   
        }   
    }
}
