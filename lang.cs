using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundirFlota
{
    internal class lang
    {
        /*
         * Programa para cambiar el idioma de la aplicación usando archivos json
         */
        private static JObject _strings;
        static lang()
        {
            LoadStrings("es"); // Idioma por efecto
        }
        public static void LoadStrings(string selectedLang) // Carga el json con el idioma seleccionado
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lang", $"strings_{selectedLang}.json");
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                _strings = JObject.Parse(json);
            }
        }
        public static string GetString(string key)
        {
            return _strings[key]?.Value<string>() ?? $"[Missing translation for '{key}']";
        }
    }
}
