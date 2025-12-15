using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.Exceptions;
using Service.Service.Interfaces;
using Service.ViewModels.Blog;

namespace Service.Service
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepo;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public BlogService(IBlogRepository blogRepo, IMapper mapper, IFileService fileService)
        {
            _blogRepo = blogRepo;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<bool> CreateAsync(BlogCreateVM vm, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;
            var blog = _mapper.Map<Blog>(vm);
            blog.CreatedDate = DateTime.Now;


            blog.MainImage = await _fileService.UploadAsync(vm.Image, "admin/uploads/blogs");

            await _blogRepo.AddAsync(blog);
            await _blogRepo.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var blog = await _blogRepo.Find(id).FirstOrDefaultAsync();

            if (blog == null) throw new CustomException(404, "Blog not found");

            if (!string.IsNullOrEmpty(blog.MainImage))
            {
                _fileService.Delete(blog.MainImage, "admin/uploads/blogs");
            }

            _blogRepo.Delete(blog);
            await _blogRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<BlogVM>> GetAllAsync()
        {
            var blogs = await _blogRepo.GetAll()
            .Include(b => b.BlogTags).ThenInclude(bt => bt.Tag)
            .Include(b => b.BlogCategories).ThenInclude(bc => bc.Category)
            .Include(b => b.Comments).ThenInclude(c => c.AppUser)
            .ToListAsync();

            return _mapper.Map<IEnumerable<BlogVM>>(blogs);
        }

        public async Task<BlogDetailVM> GetAsync(int id)
        {
            var blog = await _blogRepo.Find(id)
            .Include(b => b.BlogTags).ThenInclude(bt => bt.Tag)
            .Include(b => b.BlogCategories).ThenInclude(bc => bc.Category)
            .Include(b => b.Comments)
            .ThenInclude(c=>c.AppUser)
            .FirstOrDefaultAsync();

            if (blog == null) throw new CustomException(404, "Blog not found");

            var vm = _mapper.Map<BlogDetailVM>(blog);

            return vm;
        }

        public async Task<BlogUpdateVM> GetUpdatedVmAsync(int id)
        {
            var blog = await _blogRepo.Find(id)
                .Include(b => b.BlogTags).ThenInclude(bt => bt.Tag)
                .Include(b => b.BlogCategories).ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync();
            if (blog is null)
                throw new CustomException(404, "Blog not found");

            var vm = _mapper.Map<BlogUpdateVM>(blog);

            return vm;
        }

        public async Task<bool> UpdateAsync(BlogUpdateVM vm, ModelStateDictionary modelstate)
        {
            if (!modelstate.IsValid) return false;
            var blog = await _blogRepo.Find(vm.Id)
           .Include(b => b.BlogCategories)
           .Include(b => b.BlogTags).FirstOrDefaultAsync();

            if (blog == null) throw new CustomException(404, "Blog not found");
            var image = blog.MainImage;

            _mapper.Map(vm, blog);

            if (vm.Image != null)
            {
                if (!string.IsNullOrEmpty(blog.MainImage))
                {
                    _fileService.Delete(blog.MainImage, "admin/uploads/blogs");
                }

                blog.MainImage = await _fileService.UploadAsync(vm.Image, "admin/uploads/blogs");
            }
            else
            {
                blog.MainImage = image;
            }

            await _blogRepo.SaveChangesAsync();
            return true;
        }
        public async Task<PaginatedList<BlogVM>> GetPaginatedAsync(int page, int pageSize)
        {
            var query = _blogRepo
                .GetAll()
                .Include(b => b.Comments)
                .OrderByDescending(b => b.CreatedDate);

            var blogs = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var mapped = _mapper.Map<List<BlogVM>>(blogs);
            return new PaginatedList<BlogVM>(mapped, await query.CountAsync(), page, pageSize);
        }
        public async Task<IEnumerable<BlogCategoryVM>> GetBlogCategoriesAsync()
        {
            var categories = await _blogRepo
                .GetAll()
                .Include(b => b.BlogCategories)
                    .ThenInclude(bc => bc.Category)
                .SelectMany(b => b.BlogCategories)
                .GroupBy(bc => bc.Category.Name)
                .Select(g => new BlogCategoryVM
                {
                    Name = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .ToListAsync();

            return categories;
        }
        public async Task<IEnumerable<BlogVM>> GetLatestPostsAsync()
        {
            var blogs = await _blogRepo
                .GetAll()
                .OrderByDescending(b => b.CreatedDate)
                .Take(5)
                .ToListAsync();

            return _mapper.Map<IEnumerable<BlogVM>>(blogs);
        }

        public IQueryable<Blog> GetBlogsQuery()
        {
            return _blogRepo.GetAll()
            .AsNoTracking()
            .Include(b => b.Comments)
            .ThenInclude(c => c.AppUser)
            .Include(b => b.BlogCategories).ThenInclude(bc => bc.Category)
            .Include(b => b.BlogTags).ThenInclude(bt => bt.Tag);
        }
    }
}
