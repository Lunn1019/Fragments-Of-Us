using System;
using System.Collections.Generic;
using UnityEngine;

namespace InTerra.Vect3SDK
{
    public static partial class Comp_CameraController
    {
        public static partial class Mod_ThirdPersonCamera
        {
            #region public
            [Serializable]
            public class Base_ThirdPersonCameraAttributes : Base_CameraGeneralAttributes
            {
                public Base_ThirdPersonCameraRotationAttributes rotation = new Base_ThirdPersonCameraRotationAttributes();
                public Transform targetTransform;
            }

            [Serializable]
            public class Base_ThirdPersonCameraRotationAttributes
            {
                public float x = 0f;
                public Base_MinMaxFloatAttributes y = new Base_MinMaxFloatAttributes();
            }
            #endregion

            #region private
            private static Dictionary<MonoBehaviour, Base_ThirdPersonCameraAttributes> CameraDictionary = new ();
            private static List<MonoBehaviour> NotDestroyedList = new List<MonoBehaviour>();
            #endregion
        }
    }
}