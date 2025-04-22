using Domain.Abstractions;

namespace Domain.Entities
{
    public sealed class Car : Entity
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string EnginePower { get; set; }

    }
}
