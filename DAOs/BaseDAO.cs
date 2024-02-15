using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class BaseDAO<TEntity, TKey> where TEntity : class
    {
        private readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseDAO(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public bool Delete(TEntity entity)
        {
            try
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public TEntity? GetById(TKey id)
        {
            return _dbSet.Find(id);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
