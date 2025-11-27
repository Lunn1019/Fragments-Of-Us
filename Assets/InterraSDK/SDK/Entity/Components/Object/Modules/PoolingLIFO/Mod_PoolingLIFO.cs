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
            #region APIs
            public static void Func_CreatePool<T>(Enum key, GameObject prefab, bool isInstantiateAllowed, Transform parent) where T : MonoBehaviour
            {
                Base_Pool<T> pool = new Base_Pool<T>();
                pool.list = new List<T>();
                pool.isInstantiateAllowed = isInstantiateAllowed;
                pool.prefab = prefab;
                pool.parent = parent;

                Static_Pool<T>.pools[key] = pool;
            }

            public static IEnumerator Func_InstantiateObjects<T>(Enum key, int poolSize) where T : MonoBehaviour
            {
                if (!Static_Pool<T>.pools.TryGetValue(key, out var pool))
                {
                    throw new Exception("Pool not found");
                }

                for (int i = 0; i < poolSize; i++)
                {
                    pool.list.Add(InstantiateObject<T>(pool, pool.parent));
                    yield return null;
                }
            }

            public static bool Func_TryGetObject<T>(Enum key, Vector3 position, Quaternion rotation, out T outObject) where T : MonoBehaviour
            {
                if (!Static_Pool<T>.pools.TryGetValue(key, out var pool))
                {
                    throw new Exception($"Pool {key} not found");
                }

                outObject = null;

                if (pool.currentIndex >= pool.list.Count)
                {
                    if (!pool.isInstantiateAllowed)
                    {
                        return false;
                    }

                    pool.list.Add(InstantiateObject(pool, pool.parent));
                }

                outObject = GetObject<T>(pool, position, rotation);

                pool.currentIndex++;

                return true;
            }

            public static void Func_ReturnObject<T>(Enum key, T obj) where T : MonoBehaviour
            {
                if (!Static_Pool<T>.pools.TryGetValue(key, out var pool))
                {
                    throw new Exception("Pool not found");
                }

                pool.currentIndex--;
                pool.list[pool.currentIndex] = obj;
            }

            public static void Func_DeletePool<T>(Enum key) where T : MonoBehaviour
            {
                if (!Static_Pool<T>.pools.TryGetValue(key, out var pool))
                {
                    return;
                }

                RemoveObjectsInPool<T>(pool);

                Static_Pool<T>.pools.Remove(key);
            }

            public static void Func_DeleteAllPools()
            {
                foreach (var pool in allPools)
                {
                    foreach (DictionaryEntry entry in pool)
                    {
                        var poolBase = entry.Value;

                        var listField = poolBase.GetType().GetField("list");
                        var queue = listField.GetValue(poolBase) as System.Collections.IEnumerable;

                        foreach (var obj in queue)
                        {
                            if (obj is MonoBehaviour mono && mono != null)
                            {
                                GameObject.Destroy(mono.gameObject);
                            }
                        }
                    }

                    pool.Clear();
                }

                allPools.Clear();
            }

            public static void Func_DeleteAllPoolsOfGeneric<T>() where T : MonoBehaviour
            {
                foreach (Enum key in Static_Pool<T>.pools.Keys)
                {
                    Func_DeletePool<T>(key);
                }

                Static_Pool<T>.pools.Clear();
            }
            #endregion

            #region Private
            private static T InstantiateObject<T>(Base_Pool<T> pool, Transform parent) where T : MonoBehaviour
            {
                GameObject obj = GameObject.Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.SetParent(parent, false);
                T genericClass = obj.GetComponent<T>();
                return genericClass;
            }

            private static void RemoveObjectsInPool<T>(Base_Pool<T> pool) where T : MonoBehaviour
            {
                while (pool.list.Count > 0)
                {
                    T obj = pool.list[0];

                    pool.list.RemoveAt(0);
                    GameObject.Destroy(obj.gameObject);
                }
            }

            private static T GetObject<T>(Base_Pool<T> pool, Vector3 position, Quaternion rotation) where T : MonoBehaviour
            {
                T obj = pool.list[pool.currentIndex];
                obj.transform.position = position;
                obj.transform.rotation = rotation;

                return obj;
            }
            #endregion
        }
    }
}