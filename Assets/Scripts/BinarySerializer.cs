using Assets.Scripts.Models;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Assets.Scripts
{
    public static class BinarySerializer
    {

        public static string PathString { get; set; }
        public static string FileName { get; set; }
        private static string _fullPathFile => Path.Combine(PathString, FileName);
        private static bool CheckPath()
        {
            if (!string.IsNullOrEmpty(PathString) && !string.IsNullOrEmpty(FileName) && !File.Exists(_fullPathFile))
            {
                File.Create(_fullPathFile);
                return false;
            }
            return true;
        }
        /// <summary>
        /// Serialize data to binary file
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <param name="data">Data to save</param>
        public static void Serialize(DataGame data)
        {
            try
            {
                CheckPath();
                using (BinaryWriter writer = new BinaryWriter(File.Open(_fullPathFile, FileMode.OpenOrCreate, FileAccess.Write)))
                {
                    writer.Seek(0, SeekOrigin.End);
                    writer.Write(data.DialogStepId);
                    writer.Write(data.MoodValue);
                    writer.Write(data.Money);
                }
            }
            catch { throw; }
        }

        /// <summary>
        /// Deserialize data from binary file
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <returns>Object with type List<DataGame></returns>
        public static List<DataGame> Deserialize()
        {
            try
            {
                if(!CheckPath()) return null;
                List<DataGame> list = new List<DataGame>();
                using (BinaryReader reader = new BinaryReader(File.Open(_fullPathFile, FileMode.Open, FileAccess.Read)))
                {
                    while (reader.PeekChar() != -1)
                    {
                        var data = new DataGame();
                        data.DialogStepId = reader.ReadInt32();
                        data.MoodValue = reader.ReadInt32();
                        data.Money = reader.ReadInt32();
                        list.Add(data);
                    }
                }
                return list;
            }
            catch { throw; }
        }
        public static void CleanData()
        {
            if (!CheckPath()) return;
            using (File.Open(_fullPathFile, FileMode.Truncate)) { };

        }
    }
}
