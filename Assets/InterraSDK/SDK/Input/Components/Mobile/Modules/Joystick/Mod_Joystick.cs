using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InTerra.InputSDK
{
    //#if UNITY_ANDROID
    public static partial class Comp_Mobile
    {
        public static partial class Mod_Joystick
        {
            #region APIs
            public static void Func_RegisterJoystick(MonoBehaviour joystick, RectTransform knob, RectTransform background)
            {
                Static_JoystickAttributes newAtt = new Static_JoystickAttributes();

                newAtt.knob = knob;
                newAtt.background = background;
                newAtt.maxRadius = background.rect.width / 2;

                JoystickDictionary[joystick] = newAtt;
            }

            public static Vector2 Func_ReadJoystickValue(MonoBehaviour joystick)
            {
                if (JoystickDictionary.TryGetValue(joystick, out Static_JoystickAttributes joystickAtt))
                {
                    return joystickAtt.direction;
                }

                Debug.LogWarning($"Joystick {joystick.name} is not registered. Please register it first using Func_RegisterJoystick.");
                return Vector2.zero;
            }

            public static void Func_OnPointerDown(PointerEventData eventData, MonoBehaviour joystick)
            {
                Func_UpdateJoystick(eventData, joystick);
            }

            public static void Func_OnDrag(PointerEventData eventData, MonoBehaviour joystick)
            {
                Func_UpdateJoystick(eventData, joystick);
            }

            public static void Func_OnPointerUp(PointerEventData eventData, MonoBehaviour joystick)
            {
                if(JoystickDictionary.TryGetValue(joystick, out Static_JoystickAttributes joystickAtt))
                {
                    joystickAtt.localPoint = Vector2.zero;
                    joystickAtt.direction = Vector2.zero;
                    joystickAtt.knob.anchoredPosition = Vector2.zero;
                    return;
                }
            
                Debug.LogWarning($"Joystick {joystick.name} is not registered. Please register it first using Func_RegisterJoystick.");
            }

            private static void Func_UpdateJoystick(PointerEventData eventData, MonoBehaviour joystick)
            {
                if (JoystickDictionary.TryGetValue(joystick, out Static_JoystickAttributes joystickAtt))
                {
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        joystickAtt.background,
                        eventData.position,
                        eventData.pressEventCamera,
                        out joystickAtt.localPoint
                    );

                    Vector2 clamped = Vector2.ClampMagnitude(joystickAtt.localPoint, joystickAtt.maxRadius);
                    joystickAtt.direction = clamped / joystickAtt.maxRadius;

                    joystickAtt.knob.anchoredPosition = clamped;
                    return;
                }

                Debug.LogWarning($"Joystick {joystick.name} is not registered. Please register it first using Func_RegisterJoystick.");
            }

            public static void Func_RemoveRegistered(MonoBehaviour joystick)
            {
                NotDestroyedList.Remove(joystick);
                JoystickDictionary.Remove(joystick);
            }

            public static void Func_ClearAllRegistered()
            {
                NotDestroyedList.Clear();
                JoystickDictionary.Clear();
            }

            public static void Func_ClearAllRegisteredExceptDontDestroy()
            {
                List<MonoBehaviour> keyList = new List<MonoBehaviour>();
                List<Static_JoystickAttributes> valueList = new List<Static_JoystickAttributes>();

                for (int i = (NotDestroyedList.Count - 1); i >= 0; i--)
                {
                    MonoBehaviour joystick = NotDestroyedList[i];

                    if (joystick == null)
                    {
                        NotDestroyedList.Remove(joystick);
                        continue;
                    }

                    keyList.Add(joystick);
                    valueList.Add(JoystickDictionary[joystick]);
                }

                JoystickDictionary.Clear();

                for (int i = 0; i < keyList.Count; i++)
                {
                    JoystickDictionary[keyList[i]] = valueList[i];
                }
            }

            public static void Func_DebugCentralController()
            {
                foreach (KeyValuePair<MonoBehaviour, Static_JoystickAttributes> pair in JoystickDictionary)
                {
                    MonoBehaviour key = pair.Key;
                    Static_JoystickAttributes value = pair.Value;

                    Debug.Log($"[InputDebug] Joystick: {key.name}\n" +
                              $"Knob: {value.knob}\n" +
                              $"Background: {value.background}\n" +
                              $"Max Radius: {value.maxRadius}");
                }
            }

            public static void Func_DebugInstance(MonoBehaviour key)
            {
                if (JoystickDictionary.TryGetValue(key, out Static_JoystickAttributes value))
                {
                    Debug.Log($"[InputDebug] Joystick: {key.name}\n" +
                          $"Knob: {value.knob}\n" +
                          $"Background: {value.background}\n" +
                          $"Max Radius: {value.maxRadius}");

                    return;
                }

                Debug.Log($"[InputDebug] Joystick: {key.name} is not registered");
            }
            #endregion
        }
    }
    //#endif
}