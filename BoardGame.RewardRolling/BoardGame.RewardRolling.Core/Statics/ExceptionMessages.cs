using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Core.Statics
{
    public class ExceptionMessages
    {
        public class Authentication
        {
            public static string InvalidLoginInfo = "Thông tin đăng nhập không chính xác";
        }
        public class Reward
        {
            public static string NotFound = "Không tìm thấy thông tin quà tặng";
        }
    }
}
