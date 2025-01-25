using Entities.Models;
using System.Text;
using System.Linq.Dynamic.Core;
using Repository.Extensions.Utility;

namespace Repository.Extensions
{
    public static class RepositoryEmployeeExtension
    {
        public static IQueryable<Employees> FilterEmployees(this IQueryable<Employees> employees, uint minAge, uint maxAge) => employees.Where(e => (e.Age >= minAge && e.Age <= maxAge));

        public static IQueryable<Employees> Search(this IQueryable<Employees> employees, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return employees;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return employees.Where(e => e.Name!.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Employees> Sort(this IQueryable<Employees> employees, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString)) return employees.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Employees>(orderByQueryString);

           
            
            if (string.IsNullOrWhiteSpace(orderQuery)) return employees.OrderBy(e => e.Name);
            return employees.OrderBy(orderQuery);
        }
    }
}
