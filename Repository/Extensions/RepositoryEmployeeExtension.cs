using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class RepositoryEmployeeExtension
    {
        public static IQueryable<Employees> FilterEmployees(this IQueryable<Employees> employees,uint minAge,uint maxAge) => employees.Where(e => (e.Age >= minAge && e.Age <= maxAge)); 

        public static IQueryable<Employees> Search(this IQueryable<Employees> employees, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return employees;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return employees.Where(e => e.Name!.ToLower().Contains(lowerCaseTerm));
        }
    }
}
