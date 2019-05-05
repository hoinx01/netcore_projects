using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Queue.Kafka
{
    public class KafkaConfig
    {
        private static string KEY_COMMON = "common";
        public string Servers { get; set; }

        public Dictionary<string, Dictionary<string, object>> ProducerConfigs { get; set; }
        public Dictionary<string, string> TopicMap { get; set; }
        public Dictionary<string, Dictionary<string, object>> ConsumerConfigs { get; set; }

        public Dictionary<string, string> GetProducerConfig(string key = null)
        {
            Dictionary<string, string> config = new Dictionary<string, string>();
            config.Add("bootstrap.servers", Servers);

            if (ProducerConfigs == null)
                return config;

            if (ProducerConfigs.ContainsKey(KEY_COMMON))
            {
                var commonConfig = ProducerConfigs[KEY_COMMON];
                foreach (var pair in commonConfig)
                    config.Add(pair.Key.Replace("_", "."), pair.Value.ToString());
            }

            if (!string.IsNullOrWhiteSpace(key) && ProducerConfigs.ContainsKey(key))
            {
                var privateConfig = ProducerConfigs[key];
                foreach (var pair in privateConfig)
                {
                    string privateKey = pair.Key.Replace("_", ".");
                    if (!config.ContainsKey(pair.Key))
                        config.Add(privateKey, pair.Value.ToString());
                    else
                        config[key] = pair.Value.ToString();
                }

            }
            return config;
        }
        public Dictionary<string, string> GetConsumerConfig(string key = null)
        {
            Dictionary<string, string> config = new Dictionary<string, string>();
            config.Add("bootstrap.servers", Servers);

            if (ConsumerConfigs == null)
                return config;

            if (ConsumerConfigs.ContainsKey(KEY_COMMON))
            {
                var commonConfig = ConsumerConfigs[KEY_COMMON];
                foreach (var pair in commonConfig)
                    config.Add(pair.Key.Replace("_", "."), pair.Value.ToString());
            }

            if (!string.IsNullOrWhiteSpace(key) && ConsumerConfigs.ContainsKey(key))
            {
                var privateConfig = ConsumerConfigs[key];
                foreach (var pair in privateConfig)
                {
                    string privateKey = pair.Key.Replace("_", ".");
                    if (!config.ContainsKey(pair.Key))
                        config.Add(privateKey, pair.Value.ToString());
                    else
                        config[privateKey] = pair.Value.ToString();
                }

            }
            return config;
        }
    }
}
