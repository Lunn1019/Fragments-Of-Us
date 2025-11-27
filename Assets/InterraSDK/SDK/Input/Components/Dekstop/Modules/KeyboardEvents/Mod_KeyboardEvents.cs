using System;
using UnityEngine;

namespace InTerra.InputSDK
{
    public static partial class Comp_Dekstop
    {
        public static partial class Mod_KeyboardEvents
        {
            #region APIs
            public static bool Func_IsKeyHeldLongEnough(Mod_KeyBindings.Static_Actions.KeyAction action, float minHoldTime, bool isRemovedAfterHoldTime)
            {
                KeyCode key = Mod_KeyBindings.Func_GetKeyBindingByAction(action);
                if (Input.GetKey(key))
                {
                    float holdTime = (KeyHoldTimeDictionary.TryGetValue(key, out float time)) ? time : 0f;
                    holdTime += Time.deltaTime;

                    if (holdTime > minHoldTime)
                    {
                        KeyHoldTimeDictionary[key] = (isRemovedAfterHoldTime) ? 0f : holdTime;

                        return true;
                    }

                    KeyHoldTimeDictionary[key] = holdTime;
                }
                else
                {
                    KeyHoldTimeDictionary.Remove(key);
                }

                return false;
            }

            public static bool Func_IsKeyHeldWithinDuration(Mod_KeyBindings.Static_Actions.KeyAction action, float maxHoldTime, bool isRemovedAfterHoldTime)
            {
                KeyCode key = Mod_KeyBindings.Func_GetKeyBindingByAction(action);
                if (Input.GetKey(key))
                {
                    float holdTime = (KeyHoldTimeDictionary.TryGetValue(key, out float time)) ? time : 0f;
                    holdTime += Time.deltaTime;

                    if (holdTime < maxHoldTime)
                    {
                        KeyHoldTimeDictionary[key] = holdTime;

                        return true;
                    }

                    KeyHoldTimeDictionary[key] = (isRemovedAfterHoldTime) ? 0f : holdTime;
                }
                else
                {
                    KeyHoldTimeDictionary.Remove(key);
                }

                return false;
            }

            public static bool Func_IsKeyHeld(Mod_KeyBindings.Static_Actions.KeyAction action)
            {
                return Input.GetKey(Mod_KeyBindings.Func_GetKeyBindingByAction(action));
            }

            public static bool Func_IsKeyDown(Mod_KeyBindings.Static_Actions.KeyAction action)
            {
                return Input.GetKeyDown(Mod_KeyBindings.Func_GetKeyBindingByAction(action));
            }

            public static void Func_RemoveAllRegistered()
            {
                KeyHoldTimeDictionary.Clear();
            }
            #endregion
        }
    }
}