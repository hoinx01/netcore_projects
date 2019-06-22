using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.Service.Common
{
    public class IdGenerator : IIdGenerator
    {
        public async Task<string> GenerateStringId()
        {
            return await Task.FromResult<string>(ObjectId.GenerateNewId().ToString());
        }
    }
}
