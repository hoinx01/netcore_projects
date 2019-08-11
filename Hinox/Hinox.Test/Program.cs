using Hinox.Static.Utilities;
using System;

namespace Hinox.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var propTypeMap = ObjectUtils.GetPropertyTypeMap(typeof(Derived));
            Console.ReadKey();
        }
    }

    public abstract class Base
    {
        public int Id { get; set; }
    }
    public class Derived : Base
    {
        public string Name { get; set; }
    }
}
