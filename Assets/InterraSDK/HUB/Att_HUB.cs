using InTerra.FilesSDK;
using InTerra.InputSDK;
using InTerra.SceneAndUiSDK;
using InTerra.TimeSDK;
using InTerra.Vect3SDK;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace InTerra.HUB
{
    public static partial class InterraHUB
    {
        #region SaveData

        public class Base_SaveData<T>
        {
            public T Data_Game;
            public Base_Preferences Data_Preferences = new Base_Preferences();
        }

        public class Base_Preferences
        {
            public Dictionary<int, KeyCode> keyBindings = new Dictionary<int, KeyCode>(Comp_Dekstop.Mod_KeyBindings.DefaultKeyBindings);
        }

        #endregion
    }
}