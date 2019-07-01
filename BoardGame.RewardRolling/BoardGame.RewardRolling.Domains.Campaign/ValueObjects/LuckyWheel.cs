using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BoardGame.RewardRolling.Domains.Campaign.ValueObjects
{
    public class LuckyWheel : ValueObject
    {
        [Required]
        public LuckyWheelPart MainRim { get; set; }
        [Required]
        public LuckyWheelPart CenterCircle { get; set; }
        [Required]
        public LuckyWheelPart Pointer { get; set; }
        [Required]
        public LuckyWheelPart PointerRack { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return MainRim;
            yield return CenterCircle;
            yield return Pointer;
            yield return PointerRack;
        }
    }
    public class LuckyWheelPart : ValueObject
    {
        [Required]
        public string ImageSrc { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public int? RotationDeg { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return ImageSrc;
            yield return Width;
            yield return Height;
            yield return RotationDeg;
        }
    }
}
