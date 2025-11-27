using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InTerra.SceneAndUiSDK
{
    public static partial class Comp_BaseCanvas
    {
        public static partial class Mod_Audio
        {
            #region private
            private static Dictionary<AudioSource, (Slider, float)> AudioTypeDictionary = new Dictionary<AudioSource, (Slider, float)>();
            private static List<AudioSource> NotDestroyedList = new List<AudioSource>();

            private static class Static_AudioAttributes
            {
                public static float masterVolume = 1.0f;
            }
            #endregion
        }
    }
}