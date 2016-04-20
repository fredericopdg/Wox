using System.IO;
using System.Reflection;
using System.Threading;
using Newtonsoft.Json;

namespace Wox.Infrastructure.Storage
{
    /// <summary>
    /// Serialize object using json format.
    /// </summary>
    public class  JsonStrorage<T> where T: new()
    {
        private T _data;
        internal JsonStrorage()
        {
            FileName = typeof(T).Name;
            DirectoryPath = Path.Combine(WoxDirectroy.Executable, DirectoryName);
            FilePath = Path.Combine(DirectoryPath, FileName + FileSuffix);
        }

        protected string FileName { get; set; }
        protected string FilePath { get; set; }
        protected const string FileSuffix = ".json";
        protected string DirectoryPath { get; set; }
        protected const string DirectoryName = "Config";

        public T Load()
        {
            CheckPath();
            _data = Deserializa();
            return _data;
        }

        private T Deserializa()
        {
            T json;
            string jsonString = File.ReadAllText(FilePath);
            if (!string.IsNullOrEmpty(jsonString))
            {
                try
                {
                    json = JsonConvert.DeserializeObject<T>(jsonString);
                }
                catch (System.Exception)
                {
                    //serializedObject = LoadDefault();
                    json = new T();
                }
            }
            else
            {
                //serializedObject = LoadDefault();
                json = new T();
            }
            // happlebao todo
            //OnAfterLoad();
            return json;
        }

        private void CheckPath()
        {
            if (!File.Exists(FilePath))
            {
                if (!Directory.Exists(DirectoryPath))
                {
                    Directory.CreateDirectory(DirectoryPath);
                }
                File.Create(FilePath).Close();
            }
        }

        public void Save()
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                string jsonString = JsonConvert.SerializeObject(_data, Formatting.Indented);
                File.WriteAllText(FilePath, jsonString);
            });
        }

        private void Populate(T target, T input)
        {
            var type = typeof(T);
            var filds = type.GetFields(BindingFlags.Public);
            foreach (var fild in filds)
            {
                var value = fild.GetValue(input);
                fild.SetValue(target, value);
            }
        }
    }
}
