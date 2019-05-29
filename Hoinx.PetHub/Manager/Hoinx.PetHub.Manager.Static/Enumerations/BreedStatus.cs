using Hinox.Static.Enumerate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoinx.PetHub.Manager.Static.Enumerations
{
    public class BreedStatus : Enumeration
    {
        public static BreedStatus Active = new BreedStatus(1, "active");
        public static BreedStatus Deleted = new BreedStatus(1, "deleted");
        public BreedStatus(int id, string name) : base(id, name)
        {

        }
    }
}
