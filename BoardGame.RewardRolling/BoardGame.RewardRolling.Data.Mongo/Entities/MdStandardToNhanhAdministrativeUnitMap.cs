using System;
using System.Collections.Generic;
using System.Text;
using Hinox.Data.Mongo.Attributes;
using Hinox.Data.Mongo.Dal.Entities;

namespace BoardGame.RewardRolling.Data.Mongo.Entities
{
    [MdCollection(Name = "StandardToNhanhAdministrativeUnitMaps")]
    public class MdStandardToNhanhAdministrativeUnitMap : BaseObjectIdMongoEntity
    {
        public string StandardCityId { get; set; }
        public string StandardDistrictId { get; set; }
        public string NhanhCityId { get; set; }
        public string NhanhDistrictId { get; set; }
    }
}
