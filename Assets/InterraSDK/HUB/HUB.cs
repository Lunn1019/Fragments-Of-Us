using InTerra.FilesSDK;
using InTerra.InputSDK;
using InTerra.SceneAndUiSDK;
using InTerra.TimeSDK;
using InTerra.Vect3SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace InTerra.HUB
{
    public static partial class InterraHUB
    {
        public static async Task Func_ClearGarbage()
        {
            #region Time
            Comp_DayNightCycleController.Mod_DayNightCycle.Func_ClearAllRegisteredExceptDontDestroy();
            #endregion

            #region Vect3
            Comp_CameraController.Mod_ThirdPersonCamera.Func_ClearAllRegisteredExceptDontDestroy();
            Comp_CharacterController.Mod_Gravity.Func_ClearAllRegisteredExceptDontDestroy();
            Comp_CharacterController.Mod_Movement.Func_ClearAllRegisteredExceptDontDestroy();
            #endregion

            #region Input
            Comp_Dekstop.Mod_KeyboardEvents.Func_RemoveAllRegistered();
            Comp_Mobile.Mod_Joystick.Func_ClearAllRegisteredExceptDontDestroy();
            Comp_Mobile.Mod_TouchInputForCamera.Func_ClearAllRegisteredExceptDontDestroy();
            #endregion

            await Task.Yield();
            Comp_BaseCanvas.Mod_Loading.FinishCleaningGC();
        }

        public static void Func_InitGameData<T>(Base_SaveData<T> saveData)
        {
            Comp_Dekstop.Mod_KeyBindings.Func_InitKeyBindings(saveData.Data_Preferences.keyBindings);
        }

        public static void Func_Singleton<T>(ref T Instance, T _this) where T : MonoBehaviour
        {
            if(Instance != null && Instance != _this)
            {
                UnityEngine.Object.Destroy(_this);
                return;
            }
            else
            {
                Instance = _this;
            }
        }
    }
}