using InTerra.TimeSDK;
using System;
using UnityEngine;

namespace InTerra.TimeSDK
{
    public static partial class Comp_DayNightCycleController
    {
        [Serializable]
        public class Base_DayNightCyclesAttributes : Base_TimeAttributes
        {
            public float sunAngle = 0;
            public Base_MinMaxFloatAttributes lightIntensityRange;
        }
    }
}
