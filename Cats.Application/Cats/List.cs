using MediatR;
using Cats.Domain;
using Cats.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Cats.Application.Cats
{
    public class List
    {
        public class Query : IRequest<List<Cat>> { }

        public class Handler : IRequestHandler<Query, List<Cat>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<List<Cat>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.cats.ToListAsync(cancellationToken);
            }
        }
    }
}
