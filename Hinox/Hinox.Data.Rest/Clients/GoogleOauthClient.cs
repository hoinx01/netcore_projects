using Hinox.Data.Rest.Models.Google;
using Hinox.Static.Application;
using Hinox.Static.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hinox.Data.Rest.Clients
{
    public class GoogleOauthClient : CustomHttpClient
    {
        private static readonly JsonSerializerSettings JSON_SETTING = NewtonJsonSerializerSettings.SNAKE;
        public GoogleOauthClient() : base()
        {
            try
            {
                BaseAddress = new Uri(AppSettings.Get<string>("Google:Api:Oauth:BaseUrl"));
                Timeout = TimeSpan.FromSeconds(30);
            }
            catch (Exception e)
            {
                throw;
            }

            //DefaultRequestHeaders.Add("Content-Type", "application/json; charset=utf-8");
        }
        public override string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, JSON_SETTING);
        }
        public override T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, JSON_SETTING);
        }
        public async Task<T> Get<T>(string accessToken, string path, Dictionary<string, string> queries = null)
        {
            if (queries == null)
                queries = new Dictionary<string, string>();
            queries.Add("id_token", accessToken);

            return await GetAsync<T>(path, queries);
        }
        public async Task<GgTokenInfo> GetTokenInfo(string accessToken)
        {
            string path = "oauth2/v3/tokeninfo";
            var tokenInfo = await Get<GgTokenInfo>(accessToken, path);
            return tokenInfo;
        }
    }
}
