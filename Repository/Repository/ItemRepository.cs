using Repository.Data;
using Repository.Data.Entity;
using Repository.Repository.Base;
using Repository.Repository.IRepository;

namespace Repository.Repository
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository

    {
        public ItemRepository(AppDbContext context) : base(context)
        {

        }
    }
}
