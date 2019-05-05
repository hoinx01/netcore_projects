using Hinox.Static.Enumerate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Static.Enumerations
{
    public class PetGender : Enumeration
    {
        public static PetGender Male = new PetGender(1, "Male");
        public static PetGender Female = new PetGender(2, "Female");
        public PetGender(int id, string name) : base(id, name)
        {

        }
    }
}
