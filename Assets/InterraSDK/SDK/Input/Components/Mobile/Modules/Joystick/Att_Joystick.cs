using System.Collections.Generic;
using UnityEngine;

namespace InTerra.InputSDK
{
    public static partial class Comp_Mobile
    {
        public static partial class Mod_Joystick
        {
            #region private
            private static Dictionary<MonoBehaviour, Static_JoystickAttributes> JoystickDictionary = new Dictionary<MonoBehaviour, Static_JoystickAttributes>();
            private static List<MonoBehaviour> NotDestroyedList = new List<MonoBehaviour>();
            private class Static_JoystickAttributes
            {
                public RectTransform knob;
                public RectTransform background;
                public float maxRadius = 100f;

                public Vector2 localPoint = Vector2.zero;
                public Vector2 direction = Vector2.zero;
            }
            #endregion
        }
    }
}