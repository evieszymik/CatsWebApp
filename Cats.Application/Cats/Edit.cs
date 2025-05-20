using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Cats.Domain;
using Cats.Infrastructure;

namespace Cats.Application.Cats
{
    public class Edit
    {
        public class Command : IRequest<Unit>
        {
            public required Cat cat { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var cat = await _context.cats.FindAsync(request.cat.Id);
                if (cat == null) throw new Exception("Cat not found");

                cat.Name = request.cat.Name ?? cat.Name;
                cat.Color = request.cat.Color ?? cat.Color;
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
