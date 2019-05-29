using Hinox.Static.Enumerate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoinx.PetHub.Manager.Static.Enumerations
{
    public class SpeciesStatus : Enumeration
    {
        public static SpeciesStatus Active = new SpeciesStatus(1, "active");
        public static SpeciesStatus Deleted = new SpeciesStatus(1, "deleted");
        public SpeciesStatus(int id, string name) : base(id, name)
        {

        }
    }
}
