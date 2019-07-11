using System;
using System.Collections.Generic;
using System.Text;
using Hinox.Data.Mongo.Attributes;
using Hinox.Data.Mongo.Dal.Entities;

namespace BoardGame.RewardRolling.Data.Mongo.Entities
{
    [MdCollection(Name = "Cities")]
    public class MdCity : BaseStringIdMongoEntity
    {
        public string Name { get; set; }
        public int LevelId { get; set; }
    }
    [MdCollection(Name = "Districts")]
    public class MdDistrict : BaseStringIdMongoEntity
    {
        public string CityId { get; set; }
        public string Name { get; set; }
        public int LevelId { get; set; }
    }

    [MdCollection(Name = "Communes")]
    public class MdCommune : BaseStringIdMongoEntity
    {
        public string CityId { get; set; }
        public string DistrictId { get; set; }
        public string Name { get; set; }
        public int LevelId { get; set; }
    }
}
