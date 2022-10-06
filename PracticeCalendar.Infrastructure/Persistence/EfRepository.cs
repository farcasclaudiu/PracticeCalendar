using Ardalis.Specification.EntityFrameworkCore;
using PracticeCalendar.Domain.Common.Interfaces;

namespace PracticeCalendar.Infrastructure.Persistence
{
    public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public EfRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
