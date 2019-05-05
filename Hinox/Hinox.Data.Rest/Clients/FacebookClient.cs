using Hinox.Data.Rest.Models.Facebook;
using Hinox.Static.Application;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hinox.Data.Rest.Clients
{
    public class FacebookClient : CustomHttpClient
    {
        private readonly string apiVersion = AppSettings.Get<string>("Facebook:Api:Version");
        private static readonly JsonSerializerSettings JSON_SETTING = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };
        public FacebookClient() : base()
        {
            try
            {
                BaseAddress = new Uri(AppSettings.Get<string>("Facebook:Api:BaseUrl"));
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

        public override HttpRequestMessage InitRequest(HttpMethod method, string path, Dictionary<string, string> queries)
        {
            string realPath = string.Format("{0}/{1}", apiVersion, path);
            return base.InitRequest(method, realPath, queries);
        }
        public async Task<T> Get<T>(string token, string path, Dictionary<string, string> queries)
        {
            if (queries == null)
                queries = new Dictionary<string, string>();
            queries.Add("access_token", token);

            return await GetAsync<T>(path, queries);
        }

        public async Task<FbUser> GetCurrentUser(string token)
        {
            string path = "me";
            List<string> fields = new List<string>()
            {
                "id",
                "first_name",
                "last_name",
                "email",
                "gender",
                "picture.type(large)",
                "birthday"
            };
            Dictionary<string, string> queries = new Dictionary<string, string>()
            {
                {"fields", string.Join(",", fields) }
            };
            var user = await Get<FbUser>(token, path, queries);
            return user;
        }
    }
}
