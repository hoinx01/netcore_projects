using Hinox.Static.Enumerate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoinx.PetHub.Manager.Static.Enumerations
{
    public class PetBiologyClass : Enumeration
    {
        public static PetBiologyClass Mammalia = new PetBiologyClass(1, "Mammalia");
        public static PetBiologyClass Aves = new PetBiologyClass(2, "Aves");
        public PetBiologyClass(int id, string name) : base(id, name)
        {

        }
    }
}
