using System.Collections.Generic;
using UnityEngine;

namespace InTerra.Vect3SDK 
{
    public static partial class Comp_CharacterController
    {
        public static partial class Mod_Gravity
        {
            #region APIs
            public static void Func_RegisterCharacterController(CharacterController controller)
            {
                GravityDictionary[controller] = Static_GravityAttributes.GRAVITY_STICK_FORCE;
            }
    
            public static void Func_ApplyGravity(CharacterController controller)
            {
                if (GravityDictionary.TryGetValue(controller, out float currentYVelocity))
                {
                    Vector3 velocity = new(0f, currentYVelocity, 0f);

                    if (controller.isGrounded && velocity.y < 0f)
                    {
                        velocity.y = Static_GravityAttributes.GRAVITY_STICK_FORCE;
                    }
                    else
                    {
                        velocity.y += Physics.gravity.y * Time.deltaTime;
                    }

                    controller.Move(velocity * Time.deltaTime);
                    GravityDictionary[controller] = velocity.y;
                    return;
                }

                Debug.Log($"CharacterController {controller.name} is not registered. Please register it first using Func_RegisterCharacterController.");
            }

            public static void Func_RemoveRegistered(CharacterController controller)
            {
                NotDestroyedList.Remove(controller);
                GravityDictionary.Remove(controller);
            }

            public static void Func_ClearAllRegistered()
            {
                NotDestroyedList.Clear();
                GravityDictionary.Clear();
            }

            public static void Func_ClearAllRegisteredExceptDontDestroy()
            {
                List<CharacterController> keyList = new List<CharacterController>();
                List<float> valueList = new List<float>();

                for (int i = (NotDestroyedList.Count - 1); i >= 0; i--)
                {
                    CharacterController controller = NotDestroyedList[i];

                    if (controller == null)
                    {
                        NotDestroyedList.RemoveAt(i);
                        continue;
                    }

                    keyList.Add(controller);
                    valueList.Add(GravityDictionary[controller]);
                }

                GravityDictionary.Clear();

                for (int i = 0; i < keyList.Count; i++)
                {
                    GravityDictionary[keyList[i]] = valueList[i];
                }
            }

            public static void Func_DebugCentralController()
            {
                foreach (KeyValuePair<CharacterController, float> pair in GravityDictionary)
                {
                    CharacterController key = pair.Key;
                    float value = pair.Value;

                    Debug.Log($"[CharacterControllerDebug] CharacterController: {key.name}\n" +
                              $"Gravity Velocity: {value}\n");
                }
            }

            public static void Func_DebugInstance(CharacterController key)
            {
                if (GravityDictionary.TryGetValue(key, out float value))
                {
                    Debug.Log($"[CharacterControllerDebug] CharacterController: {key.name}\n" +
                              $"Gravity Velocity: {value}\n");

                    return;
                }

                Debug.Log($"[CharacterControllerDebug] CharacterController: {key.name} is not registered");
            }
            #endregion
        }
    }
}