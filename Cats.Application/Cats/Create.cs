﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Domain;
using Cats.Infrastructure;
using MediatR;

namespace Cats.Application.Cats
{
    public class Create
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
                var cat = new Cat
                {
                    Name = request.cat.Name,
                    Color = request.cat.Color
                };
                _context.cats.Add(cat);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
