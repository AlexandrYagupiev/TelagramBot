using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Telegram.BotSettings.Abstractions;

namespace Telegram.BotSettings
{
    public class SettingsManager : ISettingsManager
    {
        private readonly string path;
        private Dictionary<string, string> settings = new Dictionary<string, string>();

        public int Count { get => settings.Count;}

        public SettingsManager(string path)
        {
            if (!File.Exists(path))
            {
                var stream = File.Create(path);
                stream.Close();
            }
            this.path = path;
        }

        public T GetSettings<T>() where T : ISettings
        {
            var typeName = typeof(T).Name;
            if (this.settings.ContainsKey(typeName))
                return JsonSerializer.Deserialize<T>(this.settings[typeName]);
            return default;
        }

        public void SetSettings<T>(T settings) where T : ISettings
        {
            var typeName = typeof(T).Name;

            var options = new JsonSerializerOptions()
            {
                MaxDepth = 0,
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true,
                WriteIndented =true
            };
            var serialized = JsonSerializer.Serialize(settings, options);
            if (!this.settings.ContainsKey(typeName))
                this.settings.Add(typeName, serialized);
            else
                this.settings[typeName] = serialized;
        }

        public void LoadSettings()
        {
            var serialized = File.ReadAllText(path);
            if (String.IsNullOrEmpty(serialized))
                settings = new Dictionary<string, string>();
            else
                settings = JsonSerializer.Deserialize<Dictionary<string, string>>(serialized);
        }

        public void SaveSettings()
        {
            var serialized = JsonSerializer.Serialize(this.settings);
            File.WriteAllText(path, serialized);
        }
    }
}
