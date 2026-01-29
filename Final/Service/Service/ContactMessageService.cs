using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.Exceptions;
using Service.Service.Interfaces;
using Service.ViewModels.ContactMessage;
using Service.ViewModels.History;

namespace Service.Service
{
    public class ContactMessageService : IContactMessageService
    {
        private readonly IContactMessageRepository _contactRepo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContactMessageService(IContactMessageRepository contactRepo, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _contactRepo = contactRepo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateAsync(ContactMessageCreateVM vm, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;

            var message = _mapper.Map<ContactMessage>(vm);
            message.CreatedDate = DateTime.UtcNow;
            await _contactRepo.AddAsync(message);
            await _contactRepo.SaveChangesAsync();

            return true;
        }
        public async Task DeleteAsync(int id)
        {
            var message = await _contactRepo.Find(id).FirstOrDefaultAsync();
            if (message is null)
                throw new CustomException(404, "Message not found");
            _contactRepo.Delete(message);
            await _contactRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<ContactMessageVM>> GetAllAsync()
        {
            var messages = await _contactRepo.GetAll().OrderBy(h => h.CreatedDate).ToListAsync();
            var messageDtos = _mapper.Map<IEnumerable<ContactMessageVM>>(messages);
            return messageDtos;
        }

        public async Task<ContactMessageVM> GetAsync(int id)
        {
            var contactMessage = await _contactRepo.Find(id).FirstOrDefaultAsync();
            if (contactMessage is null)
                throw new CustomException(404, "Message not found");
            var contactMessageVm = _mapper.Map<ContactMessageVM>(contactMessage);
            return contactMessageVm;
        }

        public IQueryable<ContactMessageVM> GetHistoriesQuery()
        {
            return _contactRepo.GetAll().OrderBy(c => c.CreatedDate).Select(c => _mapper.Map<ContactMessageVM>(c));
        }
    }
}
