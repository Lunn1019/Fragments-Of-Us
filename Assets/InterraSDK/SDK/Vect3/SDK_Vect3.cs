using System;
using UnityEngine;

namespace InTerra.Vect3SDK
{
    #region public
    [Serializable]
    public class Base_VectorAttributes
    {
        public Transform directionBase;
        public Base_MinMaxFloatAttributes speed = new Base_MinMaxFloatAttributes();
    }
    #endregion
}