using Ardalis.Specification;

namespace PracticeCalendar.Domain.Entities.Product.Specifications
{
    public class AllProductsSpecification : Specification<Product>
    {
        public AllProductsSpecification()
        {
            Query.AsNoTracking();
        }
    }
}