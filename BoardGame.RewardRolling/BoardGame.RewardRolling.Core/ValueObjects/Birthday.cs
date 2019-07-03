using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Core.ValueObjects
{
    public class Birthday : ValueObject
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Year;
            yield return Month;
            yield return Day;
        }
    }
}
