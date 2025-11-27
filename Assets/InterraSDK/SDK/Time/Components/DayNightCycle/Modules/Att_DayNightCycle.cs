using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InTerra.TimeSDK
{
    public static partial class Comp_DayNightCycleController
    {
        public static partial class Mod_DayNightCycle
        {
            #region private
            private static Dictionary<Light, Base_DayNightCyclesAttributes> LightDictionary = new();
            private static List<Light> NotDestroyedList = new List<Light>();
            #endregion
        }
    }
}