using System.Collections.Generic;
using UnityEngine;

namespace InTerra.InputSDK
{
    public static partial class Comp_Mobile
    {
        public static partial class Mod_TouchInputForCamera
        {
            #region private
            private static Dictionary<MonoBehaviour, Static_TouchCameraAttributes> TouchCameraDictionary = new();
            private static List<MonoBehaviour> NotDestroyedList = new List<MonoBehaviour>();

            private class Static_TouchCameraAttributes
            {
                public RectTransform touchArea;
                public bool isTouching;
                public bool didMoveThisFrame;
                public Vector2 inputDelta;
                public Vector2 previousPosition;
            }
            #endregion
        }
    }
}