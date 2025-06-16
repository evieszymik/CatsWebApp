namespace Cats.Domain
{
    public class Cat
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
