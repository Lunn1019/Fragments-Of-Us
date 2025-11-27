using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace InTerra.EntitySDK
{
    public static partial class Comp_Object
    {
        public static partial class Mod_PoolingFIFO
        {
            #region APIs
            public static void Func_CreatePoolSinglePrefab<T>(Enum key, GameObject prefab, bool isInstantiateMoreAllowed, int poolSize) where T : MonoBehaviour
            {
                Base_Pool<T> pool = new Base_Pool<T>();
                pool.list = new Queue<T>();
                pool.isInstantiateAllowed = isInstantiateMoreAllowed;
                pool.prefab = prefab;
                pool.poolSize = poolSize;

                Static_Pool<T>.pools[key] = pool;
            }

            public static void Func_CreatePoolMultiplePrefabs<T>(Enum key, List<GameObject> prefabs) where T : MonoBehaviour
            {
                Base_Pool<T> pool = new Base_Pool<T>();
                pool.list = new Queue<T>();
                pool.isInstantiateAllowed = false;
                pool.prefabs = prefabs;
                pool.poolSize = prefabs.Count;

                Static_Pool<T>.pools[key] = pool;
            }

            public static void Func_CreateEmptyPool<T>(Enum key) where T : MonoBehaviour
            {
                Base_Pool<T> pool = new Base_Pool<T>();
                pool.list = new Queue<T>();
                pool.isInstantiateAllowed = false;

                Static_Pool<T>.pools[key] = pool;
            }

            public static IEnumerator Func_InstantiateObjects<T>(Enum key) where T : MonoBehaviour
            {
                if (!Static_Pool<T>.pools.TryGetValue(key, out var pool))
                {
                    throw new Exception("Pool not found");
                }

                for (int i = 0; i < pool.poolSize; i++)
                {
                    pool.list.Enqueue(InstantiateObject<T>((pool.prefab == null) ? pool.prefabs[i] : pool.prefab));

                    yield return null;
                }
            }

            public static bool Func_TryRemoveObjectFromQueue<T>(Enum key) where T : MonoBehaviour
            {
                if (!Static_Pool<T>.pools.TryGetValue(key, out Base_Pool<T> pool))
                {
                    throw new Exception("Pool not found");
                }

                if (pool.list.Count == 0)
                {
                    return false;
                }

                pool.list.Dequeue();

                return true;
            }

            public static bool Func_TryGetObjectAndRemoveFromQueue<T>(Enum key, Vector3 position, Quaternion rotation, out T outObject) where T : MonoBehaviour
            {
                if (!Static_Pool<T>.pools.TryGetValue(key, out Base_Pool<T> pool))
                {
                    throw new Exception("Pool not found");
                }

                outObject = null;

                if (pool.list.Count == 0)
                {
                    if (!pool.isInstantiateAllowed)
                    {
                        return false;
                    }

                    pool.list.Enqueue(InstantiateObject<T>(pool.prefab)); ;
                }

                outObject = GetObject<T>(pool, position, rotation);

                return true;
            }

            public static bool Func_TryGetObjectFromQueue<T>(Enum key, out T outObject) where T : MonoBehaviour
            {
                outObject = null;

                if (!Static_Pool<T>.pools.TryGetValue(key, out Base_Pool<T> pool))
                {
                    return false;
                }

                if (pool.list.Count == 0)
                {
                    return false;
                }

                outObject = PeekObject<T>(pool);

                return true;
            }

            public static bool Func_TryGetLastObjectFromQueue<T>(Enum key, out T outObject) where T : MonoBehaviour
            {
                outObject = null;

                if (!Static_Pool<T>.pools.TryGetValue(key, out Base_Pool<T> pool))
                {
                    return false;
                }

                if (pool.list.Count == 0)
                {
                    return false;
                }

                outObject = PeekLastObject<T>(pool);

                return true;
            }

            public static void Func_ReturnObject<T>(Enum key, T obj) where T : MonoBehaviour
            {
                if (!Static_Pool<T>.pools.TryGetValue(key, out Base_Pool<T> pool))
                {
                    throw new Exception("Pool not found");
                }

                pool.list.Enqueue(obj);
            }

            public static void Func_DeletePool<T>(Enum key) where T : MonoBehaviour
            {
                if (!Static_Pool<T>.pools.TryGetValue(key, out var pool))
                {
                    return;
                }

                RemoveObjectsInPool(pool);

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
            private static T InstantiateObject<T>(GameObject prefab) where T : MonoBehaviour
            {
                GameObject obj = GameObject.Instantiate(prefab);
                obj.SetActive(false);
                T genericObject = obj.GetComponent<T>();

                return genericObject;
            }

            private static void RemoveObjectsInPool<T>(Base_Pool<T> pool) where T : MonoBehaviour
            {
                while (pool.list.Count > 0)
                {
                    T obj = pool.list.Dequeue();

                    GameObject.Destroy(obj.gameObject);
                }
            }

            private static T GetObject<T>(Base_Pool<T> pool, Vector3 position, Quaternion rotation) where T : MonoBehaviour
            {
                T obj = pool.list.Dequeue();
                obj.transform.position = position;
                obj.transform.rotation = rotation;

                return obj;
            }

            private static T PeekObject<T>(Base_Pool<T> pool) where T : MonoBehaviour
            {
                T obj = pool.list.Peek();

                return obj;
            }

            private static T PeekLastObject<T>(Base_Pool<T> pool) where T : MonoBehaviour
            {
                T obj = pool.list.Last();

                return obj;
            }
            #endregion
        }
    }
}