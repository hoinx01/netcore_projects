using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Core.ValueObjects
{
    public class Address : ValueObject
    {
        public string CountryId { get; set; }
        public string ProvinceId { get; set; }
        public string DistrictId { get; set; }
        public string WardId { get; set; }
        public string Label { get; set; }
        public string Detail { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CountryId;
            yield return ProvinceId;
            yield return DistrictId;
            yield return WardId;
            yield return Label;
            yield return Detail;
        }
    }
}
