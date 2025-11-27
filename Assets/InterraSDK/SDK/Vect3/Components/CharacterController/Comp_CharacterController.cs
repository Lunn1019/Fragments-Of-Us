using System;
using UnityEngine;

namespace InTerra.Vect3SDK
{
    public static partial class Comp_CharacterController
    {
        #region public
        [Serializable]
        public class Base_ControllerAttributes : Base_VectorAttributes
        {
            public CharacterController controller;
        }
        #endregion
    }
}