using System.Reflection;

namespace Application
{
    public static class AssemblyReference
    {
        public static readonly Assembly assembly = typeof(Assembly).Assembly;
    }
}
