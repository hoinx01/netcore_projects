using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.Service.Common
{
    public interface IIdGenerator
    {
        Task<string> GenerateStringId();
    }
}
