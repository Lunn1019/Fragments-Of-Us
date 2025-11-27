using System.Collections.Generic;
using UnityEngine;

namespace InTerra.Vect3SDK
{
    public static partial class Comp_CharacterController
    {
        public static partial class Mod_Gravity
        {
            #region private
            private static Dictionary<CharacterController, float> GravityDictionary = new Dictionary<CharacterController, float>();
            private static List<CharacterController> NotDestroyedList = new List<CharacterController>();

            private static class Static_GravityAttributes
            {
                public const float GRAVITY_STICK_FORCE = -1f;
            }
            #endregion
        }
    }
}