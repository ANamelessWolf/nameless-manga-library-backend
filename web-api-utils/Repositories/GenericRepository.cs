using Nameless.WebApi.Models;
using Nameless.WebApi.Repositories;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Nameless.WebApi.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public DbContext _context;

        public Action<T> DataIsSelected;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            if (entity == null)
                throw new Exception("The entity is null");
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var result = await _context.Set<T>().ToListAsync();
            if (this.DataIsSelected != null)
                foreach (var entity in result)
                    this.DataIsSelected(entity);
            return result;
        }




        public async Task<T> GetById(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (this.DataIsSelected != null)
                this.DataIsSelected(entity);
            return entity;
        }

        public async Task<T> Insert(T entity)
        {
            if (entity == null)
                throw new Exception("The entity is null");
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            if (this.DataIsSelected != null)
                this.DataIsSelected(entity);
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            if (entity == null)
                throw new Exception("The entity is null");
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> Search(string filter)
        {
            IQueryable<T> query = _context.Set<T>();
            filter = "a => " + filter;
            var options = ScriptOptions.Default.AddReferences(typeof(T).Assembly);
            Func<T, bool> expression = await CSharpScript.EvaluateAsync<Func<T, bool>>(filter, options);
            IEnumerable<T> result = query.Where(expression);
            return result;
        }

        public async Task<IEnumerable<T>> Filter(string filter)
        {
            IQueryable<T> query = _context.Set<T>();

            if (String.IsNullOrEmpty(filter))
                throw new ArgumentNullException("Expression is null or empty");
            if (!CheckLexicon(filter))
                throw new ArgumentException("Expression is not correct");
            var value = typeof(T);
            Type typeT = query.GetType();
            String[] properties = value.GetProperties().Select(p => p.Name).ToArray();
            Regex rx = new Regex(@"(\w+|\"".*\"")");
            int index = 0;
            int intTmp;
            int delta;
            int extra = 0;
            foreach (Match match in rx.Matches(filter, index))
            {
                delta = 0;
                if (!match.Value.StartsWith('"') && !Int32.TryParse(match.Value, out intTmp) && match.Value.ToUpper() != "TRUE" && match.Value.ToUpper() != "FALSE" &&
                    match.Value != "StartsWith" && match.Value != "EndsWith" && match.Value != "Contains" && match.Value != "StringComparison" && match.Value != "OrdinalIgnoreCase" && match.Value != "System")
                {
                    if (properties.Contains(match.Value))
                    {
                        filter = filter.Substring(0, extra + match.Index) + "a." + filter.Substring(extra + match.Index, filter.Length - (extra + match.Index));
                        delta = 2;
                        extra += 2;
                    }
                    else
                        throw new ArgumentException(match.Value + " is not recognized");
                }
                index = match.Index + match.Length + delta;
            }
            filter = "a => " + filter;
            var options = ScriptOptions.Default.AddReferences(typeof(T).Assembly);
            Func<T, bool> expression = await CSharpScript.EvaluateAsync<Func<T, bool>>(filter, options);
            IEnumerable<T> result = query.Where(expression);
            return result;
        }



        public async Task<Tuple<int, IEnumerable<T>>> GetPage(int pageIndex, int pageSize, string filter = null, OrderRequest orderRequest = null)
        {
            int totalRows = 0;
            IEnumerable<T> query = _context.Set<T>();

            if (String.IsNullOrEmpty(filter))
                query = await GetAll();
            else
                query = await Filter(filter);

            totalRows = query.Count();

            if (orderRequest != null)
            {
                if (string.IsNullOrEmpty(orderRequest.orderField))
                    throw new Exception("Order field is not especificated");
                var options = ScriptOptions.Default.AddReferences(typeof(T).Assembly);
                string orden = "o=> o." + orderRequest.orderField;


                var typeEntity = typeof(T);
                var propertyType = typeEntity.GetProperty(orderRequest.orderField).PropertyType;


                Func<T, object> expression = await CSharpScript.EvaluateAsync<Func<T, object>>(orden, options);
                if (orderRequest.orderType == OrderType.Asc)
                    query = query.OrderBy(expression);
                else
                    query = query.OrderByDescending(expression);
            }

            if (pageIndex > 0)
                query = query.Skip((pageIndex - 1) * pageSize);
            else
                throw new ArgumentOutOfRangeException("pageIndex must be greater than 0");

            if (pageSize > 0)
                query = query.Take(pageSize);
            else
                throw new ArgumentOutOfRangeException("pageSize must be greater than 0");

            return new Tuple<int, IEnumerable<T>>(totalRows, query);
        }
        private bool CheckLexicon(string filter)
        {
            string filterResume = "";
            string lastFilter = filter;
            // elimina espacios del filtro
            lastFilter = Regex.Replace(lastFilter, @"(\t+)", "");
            // reduce las expresiones
            while (filterResume != lastFilter)
            {
                filterResume = lastFilter;
                // cambia sentencias válidas por la letra s
                lastFilter = Regex.Replace(lastFilter, @"((\w+|-?\d+(\.\d+)?)\s*(!=|==|&&|\|\|)\s*(\w+|\"".*\""))|(\((\w+|-?\d+(\.\d+)?)\s*(!=|==|&&|\|\|)\s*(\w+|\"".*\"")\))", "s");
                lastFilter = Regex.Replace(lastFilter, @"(\w+.(StartsWith|EndsWith|Contains)\(\s*\"".*\""\s*,\s*System\.StringComparison\.OrdinalIgnoreCase\s*)\)|(\(\w+.(StartsWith|EndsWith|Contains)\(\s*\"".*\""\s*,\s*System\.StringComparison\.OrdinalIgnoreCase\s*\)\))", "s");
            }
            return lastFilter.Trim() == "s";
        }




    }
}
