using MicroApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroApi.Services
{
    public class CountryService
    {
        private readonly List<Country> _countries;
        public CountryService()
        {
            _countries = new List<Country>
            {
                new Country
                {
                    Id = 1,
                    Name = "Canada"
                },
                new Country
                {
                    Id = 2,
                    Name = "USA"
                },
                new Country
                {
                    Id = 3,
                    Name = "Mexico"
                }
            };
        }

        public async Task<Country> Get(int id)
        {
            return await Task.FromResult(_countries.FirstOrDefault(c => c.Id == id));
        }

        public async Task<List<Country>> Get()
        {
            return await Task.FromResult(_countries);
        }
    }
}
