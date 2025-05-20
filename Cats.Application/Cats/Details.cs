using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Domain;
using Cats.Infrastructure;
using MediatR;

namespace Cats.Application.Cats
{
    public class Details
    {
        public class Query : IRequest<Cat> 
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Cat>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Cat> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.cats.FindAsync(request.Id);
            }
        }
    }
}
