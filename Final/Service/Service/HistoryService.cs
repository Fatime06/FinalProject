using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.DTOs.Category;
using Service.Exceptions;
using Service.Service.Interfaces;
using Service.ViewModels.History;

namespace Service.Service
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository _historyRepo;
        private readonly IMapper _mapper;

        public HistoryService(IHistoryRepository historyRepo, IMapper mapper)
        {
            _historyRepo = historyRepo;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(HistoryCreateVM vm, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;
            var history = _mapper.Map<History>(vm);
            history.CreatedDate = DateTime.Now;
            await _historyRepo.AddAsync(history);
            await _historyRepo.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var history = await _historyRepo.Find(id).FirstOrDefaultAsync();
            if (history is null)
                throw new CustomException(404, "History not found");
            _historyRepo.Delete(history);
            await _historyRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<HistoryVM>> GetAllAsync()
        {
            var histories = await _historyRepo.GetAll().OrderBy(h => h.Year).ToListAsync();
            var historyDtos = _mapper.Map<IEnumerable<HistoryVM>>(histories);
            return historyDtos;
        }

        public async Task<HistoryVM> GetAsync(int id)
        {
            var history = await _historyRepo.Find(id).FirstOrDefaultAsync();
            if (history is null)
                throw new CustomException(404, "History not found");
            var historyVm = _mapper.Map<HistoryVM>(history);
            return historyVm;
        }

        public async Task<HistoryUpdateVM> GetUpdatedVmAsync(int id)
        {
            var history = await _historyRepo.Find(id).FirstOrDefaultAsync();
            if (history is null)
                throw new CustomException(404, "History not found");

            var vm = _mapper.Map<HistoryUpdateVM>(history);

            return vm;
        }

        public async Task<bool> UpdateAsync(HistoryUpdateVM vm, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;
            var existHistory = await _historyRepo.Find(vm.Id).FirstOrDefaultAsync();
            if (existHistory is null) throw new CustomException(404, "History not found");
            existHistory = _mapper.Map(vm, existHistory);
            _historyRepo.Update(existHistory);
            await _historyRepo.SaveChangesAsync();
            return true;
        }
    }
}
