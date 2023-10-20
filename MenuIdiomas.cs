using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundirFlota
{
    internal class MenuIdiomas
    {
        public static string LangMenu()
        {
            bool validLang = false;
            while (!validLang)
            {
                Console.Clear();
                Console.WriteLine(lang.GetString("lang1"));
                Console.WriteLine(lang.GetString("lang2"));
                Console.Write(lang.GetString("lang3"));
                string langSelect = Console.ReadLine();
                switch (langSelect.ToLower())
                {
                    case "en": return "en"; validLang = true; break;
                    case "es": return "es"; validLang = true; break;
                    default: Console.WriteLine(lang.GetString("lang4"));
                        Thread.Sleep(2500); break;
                }
            }
            return "";
        }
    }
}
