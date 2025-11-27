using InTerra.InputSDK;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Windows;

namespace InTerra.FilesSDK
{
    public static partial class Comp_IO
    {
        public static partial class Mod_SaveData
        {
            #region private
            private static class Static_SaveFileAttributes
            {
                public static string directoryPath = Application.persistentDataPath;
                public static string saveFileName = "SaveFile.sav";
            }
            #endregion
        }
    }
}