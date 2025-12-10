using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Repository.Repositories.Interfaces;
using Service.Exceptions;
using Service.Service.Interfaces;
using Service.ViewModels.ContactMessage;
using System.Security.Claims;

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
    }
}
