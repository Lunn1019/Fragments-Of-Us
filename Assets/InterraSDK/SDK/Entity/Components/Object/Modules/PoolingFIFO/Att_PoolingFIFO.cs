using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InTerra.EntitySDK
{
    public static partial class Comp_Object
    {
        public static partial class Mod_PoolingFIFO
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
                public Queue<T> list = new Queue<T>();
                public bool isInstantiateAllowed = false;
                public GameObject prefab = null;
                public List<GameObject> prefabs;
                public int poolSize;
            }
        }
    }
}

