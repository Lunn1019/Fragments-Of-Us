using System;
using UnityEngine;

namespace InTerra.Vect3SDK
{
    public static partial class Comp_CameraController
    {
        #region public
        [Serializable]
        public class Base_CameraGeneralAttributes 
        {
            public float distance;
            public float sensitivity = 0f;
            public Transform cameraTransform;
        }
        #endregion
    }
}