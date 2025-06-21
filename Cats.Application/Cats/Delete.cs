using Cats.Infrastructure;
using MediatR;

namespace Cats.Application.Cats
{
    public class Delete
    {
        public class Command : IRequest<Unit>
        {
            public Guid Id { get; set; }
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
                var cat = await _context.cats.FindAsync(request.Id);
                if(cat == null)
                    throw new KeyNotFoundException("Cat not found");
                _context.cats.Remove(cat);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
