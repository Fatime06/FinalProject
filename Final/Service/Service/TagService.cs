using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.Exceptions;
using Service.Service.Interfaces;
using Service.ViewModels.Tag;

namespace Service.Service
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepo;
        private readonly IMapper _mapper;

        public TagService(ITagRepository tagRepo, IMapper mapper)
        {
            _tagRepo = tagRepo;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(TagCreateVM vm, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;
            if (await _tagRepo.IsExistAsync(vm.Name.Trim()))
            {
                modelState.AddModelError("Name", "This tag already exists");
                return false;
            }
            var tag = _mapper.Map<Tag>(vm);
            tag.CreatedDate = DateTime.Now;
            await _tagRepo.AddAsync(tag);
            await _tagRepo.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var tag = await _tagRepo.Find(id).FirstOrDefaultAsync();

            if (tag == null) throw new CustomException(404, "Tag not found");

            _tagRepo.Delete(tag);
            await _tagRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<TagVM>> GetAllAsync()
        {
            var tags = await _tagRepo.GetAll()
                .Include(t => t.BlogTags)
                .ThenInclude(bt => bt.Blog)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TagVM>>(tags);
        }

        public async Task<TagVM> GetAsync(int id)
        {
            var tag = await _tagRepo.Find(id)
                .Include(t => t.BlogTags)
                .ThenInclude(bt => bt.Blog)
                .FirstOrDefaultAsync();

            return _mapper.Map<TagVM>(tag);
        }

        public async Task<TagUpdateVM> GetUpdatedVmAsync(int id)
        {
            var tag = await _tagRepo.Find(id).FirstOrDefaultAsync();
            if (tag is null)
                throw new CustomException(404, "Tag not found");

            var vm = _mapper.Map<TagUpdateVM>(tag);

            return vm;
        }

        public async Task<bool> UpdateAsync(TagUpdateVM vm, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;
            var tag = await _tagRepo.Find(vm.Id).FirstOrDefaultAsync();
            if (tag == null) throw new CustomException(404, "Tag not found");

            if (await _tagRepo.IsExistAsync(vm.Name.Trim()) && _tagRepo.GetTagByNameAsync(vm.Name).Result.Id != vm.Id)
            {
                modelState.AddModelError("Name", "This tag already exists");
                return false;
            }

            _mapper.Map(vm, tag);

            await _tagRepo.SaveChangesAsync();
            return true;
        }
        public IQueryable<TagVM> GetTagsQuery()
        {
            return _tagRepo.GetAll().Select(t => _mapper.Map<TagVM>(t));
        }
    }
}
