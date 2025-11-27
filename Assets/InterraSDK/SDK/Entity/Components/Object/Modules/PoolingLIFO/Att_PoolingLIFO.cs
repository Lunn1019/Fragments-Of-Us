using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InTerra.EntitySDK
{
    public static partial class Comp_Object
    {
        public static partial class Mod_PoolingLIFO
        {
            private static List<IDictionary> allPools = new List<IDictionary>();

            private static class Static_Pool<T> where T : MonoBehaviour
            {
                public static Dictionary<Enum, Base_Pool<T>> pools = new Dictionary<Enum, Base_Pool<T>>();

                static Static_Pool()
                {
                    allPools.Add(pools);
                }
            }

            private class Base_Pool<T> where T : MonoBehaviour
            {
                public List<T> list = new List<T>();
                public int currentIndex = 0;
                public bool isInstantiateAllowed = false;
                public GameObject prefab;
                public Transform parent;
            }
        }
    }
}

