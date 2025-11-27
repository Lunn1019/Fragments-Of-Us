using System.Collections.Generic;
using UnityEngine;
using static InTerra.Vect3SDK.Comp_CameraController.Mod_ThirdPersonCamera;

namespace InTerra.TimeSDK
{
    public static partial class Comp_DayNightCycleController
    {
        public static partial class Mod_DayNightCycle
        {
            #region APIs
            public static void Func_RegisterDayNightCycle(Light light, float dayDuration, Base_MinMaxFloatAttributes intensity)
            {
                Base_DayNightCyclesAttributes DayNightAttributes = new();

                DayNightAttributes.dayDuration = dayDuration;
                DayNightAttributes.lightIntensityRange = intensity;

                LightDictionary.Add(light, DayNightAttributes);
            }

            public static void Func_RunDayNightCycle(Light light)
            {
                if(LightDictionary.TryGetValue(light, out Base_DayNightCyclesAttributes value))
                {
                    value.startTimeOfDay += (Static_TimeAttributes.fullDay / value.dayDuration) * Time.deltaTime;

                    if(value.startTimeOfDay > Static_TimeAttributes.fullDay)
                    {
                        value.startTimeOfDay = 0f;
                    }

                    value.sunAngle = (value.startTimeOfDay / Static_TimeAttributes.fullDay) * 360f - 90f;

                    light.transform.localRotation = Quaternion.Euler(value.sunAngle, 0f, 0f);
                    float normalized = Mathf.Clamp01(Mathf.Cos(Mathf.Deg2Rad * value.sunAngle));

                    light.intensity = Mathf.Lerp(value.lightIntensityRange.minValue, value.lightIntensityRange.maxValue, normalized);

                    return;
                }

                Debug.Log($"[TimeDebug] LightSource: {light.name} is not registered");
            }

            public static void Func_RemoveRegistered(Light light)
            {
                NotDestroyedList.Remove(light);
                LightDictionary.Remove(light);
            }

            public static void Func_ClearAllRegistered()
            {
                NotDestroyedList.Clear();
                LightDictionary.Clear();
            }

            public static void Func_ClearAllRegisteredExceptDontDestroy()
            {
                List<Light> keyList = new List<Light>();
                List<Base_DayNightCyclesAttributes> valueList = new List<Base_DayNightCyclesAttributes>();

                for (int i = (NotDestroyedList.Count - 1); i >= 0; i--)
                {
                    Light light = NotDestroyedList[i];

                    if (light == null)
                    {
                        NotDestroyedList.Remove(light);
                        continue;
                    }

                    keyList.Add(light);
                    valueList.Add(LightDictionary[light]);
                }

                LightDictionary.Clear();

                for (int i = 0; i < keyList.Count; i++)
                {
                    LightDictionary[keyList[i]] = valueList[i];
                }
            }

            public static void Func_DebugCentralController()
            {
                foreach (KeyValuePair<Light, Base_DayNightCyclesAttributes> pair in LightDictionary)
                {
                    Light key = pair.Key;
                    Base_DayNightCyclesAttributes value = pair.Value;

                    Debug.Log($"[TimeDebug] LightSource: {key.name}\n" +
                              $"Sun Angle: {value.sunAngle}\n" +
                              $"Day Duration: {value.dayDuration}\n" +
                              $"Min Rotation: {value.startTimeOfDay}\n" +
                              $"Min Light Intensity: {value.lightIntensityRange.minValue}\n" +
                              $"Current Light Intensity: {value.lightIntensityRange.currentValue}\n" +
                              $"Max Light Intensity: {value.lightIntensityRange.maxValue}\n");
                }
            }

            public static void Func_DebugInstance(Light key)
            {
                if (LightDictionary.TryGetValue(key, out Base_DayNightCyclesAttributes value))
                {
                    Debug.Log($"[TimeDebug] LightSource: {key.name}\n" +
                              $"Sun Angle: {value.sunAngle}\n" +
                              $"Day Duration: {value.dayDuration}\n" +
                              $"Min Rotation: {value.startTimeOfDay}\n" +
                              $"Min Light Intensity: {value.lightIntensityRange.minValue}\n" +
                              $"Current Light Intensity: {value.lightIntensityRange.currentValue}\n" +
                              $"Max Light Intensity: {value.lightIntensityRange.maxValue}\n");

                    return;
                }

                Debug.Log($"[TimeDebug] LightSource: {key.name} is not registered"); ;
            }

            #endregion
        }
    }
}
