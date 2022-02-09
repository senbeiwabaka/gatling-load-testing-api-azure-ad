using System.Threading;
using System.Threading.Tasks;
using load_testing_api.Repository.Entities;

namespace load_testing_api.Repository
{
    public interface IPersonRepository
    {
        public Task<Person?> GetAsync(int id, CancellationToken cancellationToken);

        public Task AddAsync(Person person, CancellationToken cancellationToken);
    }
}