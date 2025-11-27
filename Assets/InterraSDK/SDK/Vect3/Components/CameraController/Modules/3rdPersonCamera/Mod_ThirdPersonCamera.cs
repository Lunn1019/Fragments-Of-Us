using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InTerra.Vect3SDK
{
    public static partial class Comp_CameraController
    {
        public static partial class Mod_ThirdPersonCamera
        {
            #region APIs
            public static void Func_RegisterCamera(MonoBehaviour camera, float distance, float minY, float maxY, float sensitivity, Transform lookAt)
            {
                Base_ThirdPersonCameraAttributes att = new Base_ThirdPersonCameraAttributes();
                att.cameraTransform = camera.transform;
                att.distance = distance;
                att.rotation.y.minValue = minY;
                att.rotation.y.maxValue = maxY;
                att.sensitivity = sensitivity;
                att.targetTransform = lookAt;

                CameraDictionary[camera] = att;
            }

            public static void Func_OrbitCamera(Vector2 inputDelta, MonoBehaviour camera)
            {
                if (!CameraDictionary.ContainsKey(camera))
                {
                    Debug.Log($"Camera {camera.name} is not registered. Please register it first using Func_RegisterCamera.");
                    return;
                }

                Base_ThirdPersonCameraAttributes camAttributes = CameraDictionary[camera];
                camAttributes.rotation.x += inputDelta.x * camAttributes.sensitivity;
                camAttributes.rotation.y.currentValue -= inputDelta.y * camAttributes.sensitivity;

                camAttributes.rotation.y.currentValue = Mathf.Clamp(camAttributes.rotation.y.currentValue, camAttributes.rotation.y.minValue, camAttributes.rotation.y.maxValue);
                Quaternion rotation = Quaternion.Euler(camAttributes.rotation.y.currentValue, camAttributes.rotation.x, 0f);

                Vector3 offset = rotation * new Vector3(0f, 0f, -camAttributes.distance);
                camAttributes.cameraTransform.position = camAttributes.targetTransform.position + offset;
                camAttributes.cameraTransform.rotation = rotation;
            }

            public static void Func_RemoveRegistered(MonoBehaviour camera)
            {
                NotDestroyedList.Remove(camera);
                CameraDictionary.Remove(camera);
            }

            public static void Func_ClearAllRegistered()
            {
                NotDestroyedList.Clear();
                CameraDictionary.Clear();
            }

            public static void Func_ClearAllRegisteredExceptDontDestroy()
            {
                List<MonoBehaviour> keyList = new List<MonoBehaviour>();
                List<Base_ThirdPersonCameraAttributes> valueList = new List<Base_ThirdPersonCameraAttributes>();

                for (int i = (NotDestroyedList.Count - 1); i >= 0; i--)
                {
                    MonoBehaviour camera = NotDestroyedList[i];

                    if (camera == null)
                    {
                        NotDestroyedList.Remove(camera);
                        continue;
                    }

                    keyList.Add(camera);
                    valueList.Add(CameraDictionary[camera]);
                }

                CameraDictionary.Clear();

                for (int i = 0; i < keyList.Count; i++)
                {
                    CameraDictionary[keyList[i]] = valueList[i];
                }
            }

            public static void Func_DebugCentralController()
            {
                foreach (KeyValuePair<MonoBehaviour, Base_ThirdPersonCameraAttributes> pair in CameraDictionary)
                {
                    MonoBehaviour key = pair.Key;
                    Base_ThirdPersonCameraAttributes value = pair.Value;

                    Debug.Log($"[CharacterControllerDebug] CharacterController: {key.name}\n" +
                              $"Camera Transform: {value.cameraTransform}\n" +
                              $"Camera Distance: {value.distance}\n" +
                              $"Min Rotation: {value.rotation.y.minValue}\n" +
                              $"Current Rotation: {value.rotation.y.currentValue}\n" +
                              $"Max Rotation: {value.rotation.y.maxValue}\n" +
                              $"Camera Sensitivity: {value.sensitivity}\n" +
                              $"Look At: {value.targetTransform}\n");
                }
            }

            public static void Func_DebugInstance(MonoBehaviour key)
            {
                if (CameraDictionary.TryGetValue(key, out Base_ThirdPersonCameraAttributes value))
                {
                    Debug.Log($"[CameraControllerDebug] Camera: {key.name}\n" +
                              $"Camera Transform: {value.cameraTransform}\n" +
                              $"Camera Distance: {value.distance}\n" +
                              $"Min Rotation: {value.rotation.y.minValue}\n" +
                              $"Current Rotation: {value.rotation.y.currentValue}\n" +
                              $"Max Rotation: {value.rotation.y.maxValue}\n" +
                              $"Camera Sensitivity: {value.sensitivity}\n" +
                              $"Look At: {value.targetTransform}\n");

                    return;
                }

                Debug.Log($"[CameraControllerDebug] Camera: {key.name} is not registered");
            }

            #endregion
        }
    }
}
