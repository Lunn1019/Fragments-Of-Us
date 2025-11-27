using System.Collections.Generic;
using UnityEngine;

namespace InTerra.Vect3SDK
{
    public static partial class Comp_CharacterController
    {
        public static partial class Mod_Movement
        {
            #region APIs
            public static void Func_RegisterCharacterController(CharacterController character, float acceleration, float maxSpeed, Transform cameraPov)
            {
                Base_MovementAttributes movementAttributes = new();
                movementAttributes.controller = character;
                movementAttributes.acceleration = acceleration;
                movementAttributes.directionBase = cameraPov;
                movementAttributes.speed.minValue = 0f;
                movementAttributes.speed.currentValue = 0f;
                movementAttributes.speed.maxValue = maxSpeed;

                MovementDictionary[character] = movementAttributes;
            }

            public static void Func_AbsoluteMovement(CharacterController character, Vector2 movementInput)
            {
                if(MovementDictionary.TryGetValue(character, out Base_MovementAttributes movementAttributes))
                {
                    movementAttributes.speed.currentValue = (movementInput == Vector2.zero) ? 0f : movementAttributes.speed.currentValue;
                    Vector3 move = new(movementInput.x, 0f, movementInput.y);
                    move = move.normalized;

                    if (move != Vector3.zero)
                    {
                        movementAttributes.controller.Move(Func_GetMovementSpeed(movementAttributes) * Time.deltaTime * move);
                    }

                    return;
                }

                Debug.Log($"CharacterController {character.name} is not registered. Please register it first using Func_RegisterCharacterController.");
            }

            public static void Func_RelativeMovement(CharacterController character, Vector2 movementInput)
            {
                if (MovementDictionary.TryGetValue(character, out Base_MovementAttributes movementAttributes))
                {
                    movementAttributes.speed.currentValue = (movementInput == Vector2.zero) ? 0f : movementAttributes.speed.currentValue;
                    Vector3 move = new(movementInput.x, 0, movementInput.y);
                    move = movementAttributes.directionBase.forward * move.z + movementAttributes.directionBase.right * move.x;

                    if (move != Vector3.zero)
                    {
                        movementAttributes.controller.Move(Func_GetMovementSpeed(movementAttributes) * Time.deltaTime * move);
                    }

                    return;
                }

                Debug.Log($"CharacterController {character.name} is not registered. Please register it first using Func_RegisterCharacterController.");
            }

            public static void Func_RemoveRegistered(CharacterController character)
            {
                NotDestroyedList.Remove(character);
                MovementDictionary.Remove(character);
            }

            public static void Func_ClearAllRegistered()
            {
                NotDestroyedList.Clear();
                MovementDictionary.Clear();
            }

            public static void Func_ClearAllRegisteredExceptDontDestroy()
            {
                List<CharacterController> keyList = new List<CharacterController>();
                List<Base_MovementAttributes> valueList = new List<Base_MovementAttributes>();

                for(int i = (NotDestroyedList.Count - 1); i >=0; i--)
                {
                    CharacterController controller = NotDestroyedList[i];
                    if(controller == null)
                    {
                        NotDestroyedList.RemoveAt(i);
                        continue;
                    }

                    keyList.Add(controller);
                    valueList.Add(MovementDictionary[controller]);
                }

                MovementDictionary.Clear();

                for(int i = 0; i < keyList.Count; i++)
                {
                    MovementDictionary[keyList[i]] = valueList[i];
                }
            }

            public static void Func_DebugCentralController()
            {
                foreach (KeyValuePair<CharacterController, Base_MovementAttributes> pair in MovementDictionary)
                {
                    CharacterController key = pair.Key;
                    Base_MovementAttributes value = pair.Value;

                    Debug.Log($"[MovementDebug] Character: {key.name}\n" +
                              $"Min Speed: {value.speed.minValue}\n" +
                              $"Current Speed: {value.speed.currentValue}\n" +
                              $"Max Speed: {value.speed.maxValue}\n" +
                              $"Acceleration: {value.acceleration}\n" +
                              $"Direction: {value.directionBase.name}");
                }
            }

            public static void Func_DebugInstance(CharacterController key)
            {
                if(MovementDictionary.TryGetValue(key, out Base_MovementAttributes value))
                {
                    Debug.Log($"[MovementDebug] Character: {key.name}\n" +
                          $"Min Speed: {value.speed.minValue}\n" +
                          $"Current Speed: {value.speed.currentValue}\n" +
                          $"Max Speed: {value.speed.maxValue}\n" +
                          $"Acceleration: {value.acceleration}\n" +
                          $"Direction: {value.directionBase.name}");

                    return;
                }

                Debug.Log($"[MovementDebug] Character: {key.name} is not registered");
            }
            #endregion

            #region private
            private static float Func_GetMovementSpeed(Base_MovementAttributes movementAttributes)
            {
                movementAttributes.speed.currentValue += movementAttributes.acceleration * Time.deltaTime;
                return movementAttributes.speed.currentValue = Mathf.Clamp(movementAttributes.speed.currentValue, movementAttributes.speed.minValue, movementAttributes.speed.maxValue);
            }
            #endregion
        }
    }
}