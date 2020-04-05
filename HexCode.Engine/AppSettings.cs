using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HexCode.Engine
{
    public class AppSettings<T> where T : new()
    {
        private const string DEFAULT_FILENAME = "settings.json";

        public void Save(string fileName = DEFAULT_FILENAME)
        {

            JsonSerializer serializer = new JsonSerializer();

            //serializer.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;

            using (StreamWriter file = File.CreateText(@".\" + fileName)) {
                serializer.Serialize(file, this);
            }
        }

        //public static void Save(T pSettings, string fileName = DEFAULT_FILENAME)
        //{

        //    JsonSerializer serializer = new JsonSerializer();

        //    //serializer.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;

        //    using (StreamWriter file = File.CreateText(@".\" + fileName)) {
        //        serializer.Serialize(file, pSettings);
        //    }


        //    File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(pSettings));


        //}

        public static T Load(string fileName = DEFAULT_FILENAME)
        {
            T ret = new T();
            if (File.Exists(fileName)) {

                JsonSerializer serializer = new JsonSerializer();
                //new JsonSerializerSettings {ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor}

                using (StreamReader file = File.OpenText(@".\" + fileName)) {
                    ret = (T)serializer.Deserialize(file, typeof(T));

                }
            }

            return ret;
        }
    }
}
