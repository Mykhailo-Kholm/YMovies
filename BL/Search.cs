using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace BL
{
    public class Search<T>
    {
        /// <summary>
        /// Поиск фильмов, сериалов и тд. имеющих схожий параметр(genre,year,country,type)
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="expression"></param>
        /// <returns>Task&lt;ICollection&lt;T&gt;&gt;</returns>
        public async Task<ICollection<T>> SearchBy(
            ICollection<T> collection,
            string genre = null,
            string year = null,
            string country = null,
            string type = null
            )
        {
            List<T> tempCollection = new List<T>();

            int requiredNumbOfFilters = GetNumbOfRequiredFilters(genre, year, country, type);
            int passedNumbOfFilters = 0;

            foreach (var film in collection)
            {
                PropertyInfo[] properties = film.GetType().GetProperties();
                try
                {
                    foreach (var prop in properties)
                    {
                        if (!string.IsNullOrEmpty(genre) &&
                            prop.Name.ToLower().StartsWith("genre") &&
                            prop.GetValue(film).ToString().ToLower().Contains(genre.ToLower()))
                        {
                            passedNumbOfFilters++;
                            continue;
                        }
                        if (!string.IsNullOrEmpty(year) &&
                            prop.Name.ToLower().StartsWith("year") &&
                            prop.GetValue(film).ToString().ToLower().Contains(year.ToLower()))
                        {
                            passedNumbOfFilters++;
                            continue;
                        }
                        if (!string.IsNullOrEmpty(country) &&
                            prop.Name.ToLower().StartsWith("countr") &&
                            prop.GetValue(film).ToString().ToLower().Contains(country.ToLower()))
                        {
                            passedNumbOfFilters++;
                            continue;
                        }
                        if (!string.IsNullOrEmpty(type) &&
                            prop.Name.ToLower().StartsWith("type") &&
                            prop.GetValue(film).ToString().ToLower().Contains(type.ToLower()))
                        {
                            passedNumbOfFilters++;
                            continue;
                        }
                        if (requiredNumbOfFilters == passedNumbOfFilters)
                        {
                            tempCollection.Add(film);
                            break;
                        }
                    }
                }
                catch (NullReferenceException e)
                {
                    return tempCollection = new List<T>();
                }

                passedNumbOfFilters = 0;
                properties = null;
            }

            return tempCollection;
        }

        private int GetNumbOfRequiredFilters(params string[] filters)
        {
            int tempCount = 0;
            foreach (var filter in filters)
            {
                if (!string.IsNullOrEmpty(filter))
                {
                    tempCount++;
                }
            }

            return tempCount;
        }
    }
}
