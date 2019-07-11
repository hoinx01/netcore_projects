using System;
using System.Collections.Generic;
using System.Text;
using Hinox.Static.Enumerate;

namespace BoardGame.RewardRolling.Core.Statics
{
    public class CityLevel : Enumeration
    {
        public static CityLevel City = new CityLevel(1, "Thành phố trực thuộc trung ương");
        public static CityLevel Province = new CityLevel(2, "Tỉnh");
        public CityLevel(int id, string name) : base(id, name)
        {

        }
    }
    public class DistrictLevel : Enumeration
    {
        public static DistrictLevel District = new DistrictLevel(1, "Quận");
        public static DistrictLevel Borough = new DistrictLevel(2, "Thị xã");
        public static DistrictLevel Town = new DistrictLevel(3, "Huyện");
        public DistrictLevel(int id, string name) : base(id, name)
        {

        }
    }

    public class CommuneLevel : Enumeration
    {
        public static CommuneLevel Ward = new CommuneLevel(1, "Phường");
        public static CommuneLevel HighCommune = new CommuneLevel(2, "Thị trấn");
        public static CommuneLevel Commune = new CommuneLevel(3, "Xã");
        public CommuneLevel(int id, string name) : base(id, name)
        {

        }
    }
}
