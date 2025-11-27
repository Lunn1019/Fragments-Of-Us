using System;
using System.Collections.Generic;
using UnityEngine;

namespace InTerra.EntitySDK
{
    public static partial class Comp_Object
    {
        public static partial class Mod_PoolingRandom
        {
            public class Base_WeightedPooling<T>
            {
                public T obj;
                public int weight = 0;
            }
        }
    }
}

