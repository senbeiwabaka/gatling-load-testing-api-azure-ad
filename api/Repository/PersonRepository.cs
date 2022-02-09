using System.Threading;
using System.Threading.Tasks;
using load_testing_api.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace load_testing_api.Repository
{
    internal sealed class PersonRepository : IPersonRepository
    {
        private readonly Context context;

        public PersonRepository(Context context)
        {
            this.context = context;
        }

        async Task IPersonRepository.AddAsync(Person person, CancellationToken cancellationToken)
        {
            context.Persons.Add(person);

            _ = await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        async Task<Person?> IPersonRepository.GetAsync(int id, CancellationToken cancellationToken)
        {
            return await context.Persons.SingleOrDefaultAsync(x => x.Id == id, cancellationToken).ConfigureAwait(false);
        }
    }
}