using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InTerra.InputSDK
{
    public static partial class Comp_Mobile
    {
        public static partial class Mod_TouchInputForCamera
        {
            #region APIs
            public static void Func_RegisterTouchPanel(MonoBehaviour touchPanel)
            {
                Static_TouchCameraAttributes newTouchInput = new();

                if(touchPanel.TryGetComponent<RectTransform>(out RectTransform panel))
                {
                    newTouchInput.touchArea = panel;
                    TouchCameraDictionary[touchPanel] = newTouchInput;
                    
                    return;
                }

                Debug.LogWarning($"MonoBehaviour {touchPanel.name} does not have the required TouchPanel RectTransform.");
            }

            public static void Func_OnPointerDown(MonoBehaviour touchPanel, PointerEventData eventData)
            {
                if (TouchCameraDictionary.TryGetValue(touchPanel, out Static_TouchCameraAttributes touchInputAtt))
                {
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        touchInputAtt.touchArea,
                        eventData.position,
                        eventData.pressEventCamera,
                        out touchInputAtt.previousPosition
                    );

                    touchInputAtt.isTouching = true;
                    touchInputAtt.didMoveThisFrame = false;

                    return;
                }

                Debug.LogWarning($"Touch Panel {touchPanel.name} is not registered. Please register it first using Func_RegisterInput.");
            }
            public static void Func_OnDrag(MonoBehaviour touchPanel, PointerEventData eventData)
            {
                if (TouchCameraDictionary.TryGetValue(touchPanel, out Static_TouchCameraAttributes touchInputAtt))
                {
                    if (!touchInputAtt.isTouching)
                    {
                        return;
                    }

                    if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        touchInputAtt.touchArea,
                        eventData.position,
                        eventData.pressEventCamera,
                        out Vector2 currentPosition
                        ))
                    {
                        touchInputAtt.inputDelta = currentPosition - touchInputAtt.previousPosition;
                        touchInputAtt.previousPosition = currentPosition;
                        touchInputAtt.didMoveThisFrame = true;
                    }

                    return;
                }

                Debug.LogWarning($"Touch Panel {touchPanel.name} is not registered. Please register it first using Func_RegisterInput.");
            }
            public static void Func_OnPointerUp(MonoBehaviour touchPanel, PointerEventData eventData)
            {
                if (TouchCameraDictionary.TryGetValue(touchPanel, out Static_TouchCameraAttributes touchInputAtt))
                {
                    touchInputAtt.isTouching = false;
                    touchInputAtt.inputDelta = Vector2.zero;
                    return;
                }

                Debug.LogWarning($"Touch Panel {touchPanel.name} is not registered. Please register it first using Func_RegisterInput.");
            }

            public static Vector2 Func_ConsumeDelta(MonoBehaviour inputArea)
            {
                if (TouchCameraDictionary.TryGetValue(inputArea, out Static_TouchCameraAttributes touchInput))
                {
                    if (touchInput.isTouching && !touchInput.didMoveThisFrame)
                    {
                        touchInput.inputDelta = Vector2.zero;
                    }

                    touchInput.didMoveThisFrame = false;
                    Vector2 tempDelta = touchInput.inputDelta;
                    touchInput.inputDelta = Vector2.zero;
                    return tempDelta;
                }
                else
                {
                    Debug.LogWarning($"Touch Panel {inputArea.name} is not registered. Please register it first using Func_RegisterInput.");
                    return Vector2.zero;
                }
            }

            public static void Func_RemoveRegistered(MonoBehaviour touchPanel)
            {
                NotDestroyedList.Remove(touchPanel);
                TouchCameraDictionary.Remove(touchPanel);
            }

            public static void Func_ClearAllRegistered()
            {
                NotDestroyedList.Clear();
                TouchCameraDictionary.Clear();
            }

            public static void Func_ClearAllRegisteredExceptDontDestroy()
            {
                List<MonoBehaviour> keyList = new List<MonoBehaviour>();
                List<Static_TouchCameraAttributes> valueList = new List<Static_TouchCameraAttributes>();

                for (int i = (NotDestroyedList.Count - 1); i >= 0; i--)
                {
                    MonoBehaviour touchPanel = NotDestroyedList[i];

                    if (touchPanel == null)
                    {
                        NotDestroyedList.Remove(touchPanel);
                        continue;
                    }

                    keyList.Add(touchPanel);
                    valueList.Add(TouchCameraDictionary[touchPanel]);
                }

                TouchCameraDictionary.Clear();

                for (int i = 0; i < keyList.Count; i++)
                {
                    TouchCameraDictionary[keyList[i]] = valueList[i];
                }
            }

            public static void Func_DebugCentralController()
            {
                foreach (KeyValuePair<MonoBehaviour, Static_TouchCameraAttributes> pair in TouchCameraDictionary)
                {
                    MonoBehaviour key = pair.Key;
                    Static_TouchCameraAttributes value = pair.Value;

                    Debug.Log($"[InputDebug] Touch Panel For Camera: {key.name}\n" +
                              $"Touch Area: {value.touchArea}\n" +
                              $"Is Touching: {value.isTouching}\n" +
                              $"Did Move This Frame: {value.didMoveThisFrame}\n" +
                              $"Input Delta: {value.inputDelta}\n" +
                              $"Previous Position: {value.previousPosition}");
                }
            }

            public static void Func_DebugInstance(MonoBehaviour key)
            {
                if (TouchCameraDictionary.TryGetValue(key, out Static_TouchCameraAttributes value))
                {
                    Debug.Log($"[InputDebug] Touch Panel For Camera: {key.name}\n" +
                              $"Touch Area: {value.touchArea}\n" +
                              $"Is Touching: {value.isTouching}\n" +
                              $"Did Move This Frame: {value.didMoveThisFrame}\n" +
                              $"Input Delta: {value.inputDelta}\n" +
                              $"Previous Position: {value.previousPosition}");

                    return;
                }

                Debug.Log($"[InputDebug] Touch Panel For Camera: {key.name} is not registered");
            }
            #endregion
        }
    }
}