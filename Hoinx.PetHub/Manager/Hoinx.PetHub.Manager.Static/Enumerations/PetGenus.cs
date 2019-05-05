using Hinox.Static.Enumerate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoinx.PetHub.Manager.Static.Enumerations
{
    public class PetGenus : Enumeration
    {
        public static PetGenus Dog = new PetGenus(1, "Dog");
        public static PetGenus Cat = new PetGenus(2, "Cat");
        public static PetGenus Bird = new PetGenus(3, "Bird");

        public PetGenus(int id, string name) : base(id, name)
        {

        }
    }
}
