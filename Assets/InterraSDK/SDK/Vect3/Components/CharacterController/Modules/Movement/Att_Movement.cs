using System;
using System.Collections.Generic;
using UnityEngine;

namespace InTerra.Vect3SDK
{
    public static partial class Comp_CharacterController
    {
        public static partial class Mod_Movement
        {
            #region public
            [Serializable]
            public class Base_MovementAttributes : Base_ControllerAttributes
            {
                public float acceleration = 0f;
            }
            #endregion

            #region private
            private static Dictionary<CharacterController, Base_MovementAttributes> MovementDictionary = new Dictionary<CharacterController, Base_MovementAttributes>();
            private static List<CharacterController> NotDestroyedList = new List<CharacterController>();
            #endregion
        }
    }
}