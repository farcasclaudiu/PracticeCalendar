using Ardalis.Specification;

namespace PracticeCalendar.Domain.Common.Interfaces
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}
