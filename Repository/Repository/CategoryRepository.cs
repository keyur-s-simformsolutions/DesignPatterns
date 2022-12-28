using Repository.Data;
using Repository.Data.Entity;
using Repository.Repository.Base;
using Repository.Repository.IRepository;

namespace Repository.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {

        }
    }
}
