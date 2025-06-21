using Cats.Domain;

namespace Cats.Infrastructure
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.cats.Any()) return;

            var cats = new List<Cat>
            {
                new Cat
                {
                    Name ="Pralinka",
                    Color = "Black"
                },
                new Cat
                {
                    Name = "Luna",
                    Color = "White"
                }
            };
            await context.cats.AddRangeAsync(cats);
            await context.SaveChangesAsync();
        }
    }
}
