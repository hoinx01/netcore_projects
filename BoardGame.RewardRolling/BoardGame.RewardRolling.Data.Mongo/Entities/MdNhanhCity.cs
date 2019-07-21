using System;
using System.Collections.Generic;
using System.Text;
using Hinox.Data.Mongo.Attributes;
using Hinox.Data.Mongo.Dal.Entities;

namespace BoardGame.RewardRolling.Data.Mongo.Entities
{
    [MdCollection(Name="NhanhCities")]
    public class MdNhanhCity : BaseStringIdMongoEntity
    {
        public string Name { get; set; }
        public List<MdNhanhDistrict> Districts { get; set; }
    }

    public class MdNhanhDistrict
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
