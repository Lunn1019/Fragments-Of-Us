using InTerra.HUB;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace InTerra.FilesSDK
{
    public static partial class Comp_IO
    {
        public static partial class Mod_SaveData
        {
            #region APIs
            public static void Func_Save<T>(InterraHUB.Base_SaveData<T> data)
            {
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                string encrypted = Comp_Memory.Mod_Encryption.Func_EncryptString(json);
                string path = Path.Combine(Application.persistentDataPath, Static_SaveFileAttributes.saveFileName);

                File.WriteAllText(path, encrypted);
            }

            public static InterraHUB.Base_SaveData<T> Func_Load<T>()
            {
                string path = Path.Combine(Application.persistentDataPath, Static_SaveFileAttributes.saveFileName);

                if (!File.Exists(path))
                {
                    Debug.LogWarning($"File Not Exist");
                    return default;
                }

                Debug.LogWarning($"File Exist");
                string encrypted = File.ReadAllText(path);
                string json = Comp_Memory.Mod_Encryption.Func_DecryptString(encrypted);
                return JsonConvert.DeserializeObject<InterraHUB.Base_SaveData<T>>(json);
            }

            public static void Func_Delete()
            {
                string path = Path.Combine(Application.persistentDataPath, Static_SaveFileAttributes.saveFileName);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }

            public static void Func_SetDirectory(string newDirectory, string newFileName)
            {
                if (!Directory.Exists(newDirectory))
                {
                    Directory.CreateDirectory(newDirectory);
                }

                Static_SaveFileAttributes.directoryPath = newDirectory;
                Static_SaveFileAttributes.saveFileName = newFileName + ".sav";
            }
            #endregion
        }

        //DOCUMENTATION

    }
}