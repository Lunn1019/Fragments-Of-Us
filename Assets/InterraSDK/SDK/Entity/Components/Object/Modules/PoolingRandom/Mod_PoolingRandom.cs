using System;
using System.Collections.Generic;
using UnityEngine;

namespace InTerra.EntitySDK
{
    public static partial class Comp_Object
    {
        public static partial class Mod_PoolingRandom
        {
            #region APIs
            public static List<T> Func_RandomizePool<T>(List<T> objectList, int maxCount, bool isFixedSize)
            {
                for (int i = objectList.Count - 1; i > 0; i--)
                {
                    int j = UnityEngine.Random.Range(0, i + 1);
                    T temp = objectList[i];
                    objectList[i] = objectList[j];
                    objectList[j] = temp;
                }

                int maxQty = isFixedSize ? Math.Min(maxCount, objectList.Count)
                                         : UnityEngine.Random.Range(0, objectList.Count + 1);

                List<T> selectedObjects = objectList.GetRange(0, maxQty);

                return selectedObjects;
            }

            public static List<T> Func_RandomizeWeightedPool<T>(List<Base_WeightedPooling<T>> objectList, int maxCount, bool isFixedSize, bool isDuplicatesAllowed)
            {
                List<T> selectedObjects = new List<T>();
                List<Base_WeightedPooling<T>> tempList = new List<Base_WeightedPooling<T>>(objectList);

                int maxQty = isFixedSize ? Math.Min(maxCount, tempList.Count)
                                         : UnityEngine.Random.Range(0, tempList.Count + 1);

                for (int i = 0; i < maxQty; i++)
                {
                    if (tempList.Count == 0) break;

                    int totalWeight = 0;
                    foreach (Base_WeightedPooling<T> item in tempList)
                        totalWeight += item.weight;

                    int r = UnityEngine.Random.Range(0, totalWeight);

                    for (int j = 0; j < tempList.Count; j++)
                    {
                        r -= tempList[j].weight;
                        if (r < 0)
                        {
                            selectedObjects.Add(tempList[j].obj);

                            if (!isDuplicatesAllowed)
                            {
                                tempList.RemoveAt(j);
                            }
                            break;
                        }
                    }
                }

                return selectedObjects;
            }

            public static List<GameObject> Func_RandomizePoolWithInstantiate(List<GameObject> prefabList, int maxCount, bool isFixedSize)
            {
                List<GameObject> tempPrefabList = Func_RandomizePool<GameObject>(prefabList, maxCount, isFixedSize);

                return RandomizePoolWithInstantiate(tempPrefabList, maxCount, isFixedSize);
            }

            public static List<GameObject> Func_RandomizeWeightedPoolWithInstantiate(List<Base_WeightedPooling<GameObject>> prefabList, int maxCount, bool isFixedSize, bool isDuplicateAllowed)
            {
                List<GameObject> tempPrefabList = Func_RandomizeWeightedPool<GameObject>(prefabList, maxCount, isFixedSize, isDuplicateAllowed);

                return RandomizePoolWithInstantiate(tempPrefabList, maxCount, isFixedSize);
            }

            public static List<GameObject> Func_RandomizePoolAndPositionWithInstantiate(List<GameObject> prefabList, List<Transform> spawnTransformList, int maxCount, bool isFixedSize)
            {
                List<GameObject> tempPrefabList = Func_RandomizePool<GameObject>(prefabList, maxCount, isFixedSize);
                List<Transform> tempSpawnList = Func_RandomizePool<Transform>(spawnTransformList, tempPrefabList.Count, isFixedSize);

                return RandomizePoolWithInstantiateAndPosition(tempPrefabList, tempSpawnList);
            }

            public static List<GameObject> Func_RandomizeWeightedPoolAndPositionWithInstantiate(List<Base_WeightedPooling<GameObject>> prefabList, List<Transform> spawnTransformList, int maxCount, bool isFixedSize, bool isDuplicateAllowed)
            {
                List<GameObject> tempPrefabList = Func_RandomizeWeightedPool<GameObject>(prefabList, maxCount, isFixedSize, isDuplicateAllowed);
                List<Transform> tempSpawnList = Func_RandomizePool<Transform>(spawnTransformList, tempPrefabList.Count, isFixedSize);

                return RandomizePoolWithInstantiateAndPosition(tempPrefabList, tempSpawnList);
            }

            public static List<GameObject> Func_RandomizePoolAndWeightedPositionWithInstantiate(List<GameObject> prefabList, List<Base_WeightedPooling<Transform>> spawnTransformList, int maxCount, bool isFixedSize)
            {
                List<GameObject> tempPrefabList = Func_RandomizePool<GameObject>(prefabList, maxCount, isFixedSize);
                List<Transform> tempSpawnList = Func_RandomizeWeightedPool<Transform>(spawnTransformList, tempPrefabList.Count, isFixedSize, false);

                return RandomizePoolWithInstantiateAndPosition(tempPrefabList, tempSpawnList);
            }

            public static List<GameObject> Func_RandomizeWeightedPoolAndWeightedPositionWithInstantiate(List<Base_WeightedPooling<GameObject>> prefabList, List<Base_WeightedPooling<Transform>> spawnTransformList, int maxCount, bool isFixedSize, bool isDuplicateAllowed)
            {
                List<GameObject> tempPrefabList = Func_RandomizeWeightedPool<GameObject>(prefabList, maxCount, isFixedSize, isDuplicateAllowed);
                List<Transform> tempSpawnList = Func_RandomizeWeightedPool<Transform>(spawnTransformList, tempPrefabList.Count, isFixedSize, isDuplicateAllowed);

                return RandomizePoolWithInstantiateAndPosition(tempPrefabList, tempSpawnList);
            }
            #endregion

            #region Private
            private static List<GameObject> RandomizePoolWithInstantiate(List<GameObject> prefabList, int maxCount, bool isFixedSize)
            {
                List<GameObject> objects = new List<GameObject>();
                for (int i = 0; i < prefabList.Count; i++)
                {
                    GameObject gameObject = GameObject.Instantiate(prefabList[i]);
                    objects.Add(gameObject);
                }

                return objects;
            }

            private static List<GameObject> RandomizePoolWithInstantiateAndPosition(List<GameObject> prefabList, List<Transform> spawnList)
            {
                List<GameObject> objects = new List<GameObject>();
                for (int i = 0; i < prefabList.Count; i++)
                {
                    GameObject gameObject = GameObject.Instantiate(prefabList[i]);
                    gameObject.transform.SetPositionAndRotation(spawnList[i].position, spawnList[i].rotation);
                    objects.Add(gameObject);
                }

                return objects;
            }
            #endregion
        }
    }
}

