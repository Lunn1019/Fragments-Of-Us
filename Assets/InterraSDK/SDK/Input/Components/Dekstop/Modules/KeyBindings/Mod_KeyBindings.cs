using System.Collections.Generic;
using UnityEngine;
using static InTerra.InputSDK.Comp_Dekstop.Mod_KeyBindings.Static_Actions;

namespace InTerra.InputSDK
{
	public static partial class Comp_Dekstop
	{
        public static partial class Mod_KeyBindings
        {
            public static void Func_InitKeyBindings(Dictionary<int, KeyCode> dict)
            {
                if (dict != null && dict.Count != 0)
                {
                    KeyBindingsByAction = new Dictionary<int, KeyCode>(dict);
                }
                else
                {
                    KeyBindingsByAction = new Dictionary<int, KeyCode>(DefaultKeyBindings);
                }
                 
                KeyBindingsByKey = new Dictionary<KeyCode, int>();

                foreach (KeyValuePair<int, KeyCode> pair in KeyBindingsByAction)
                {
                    KeyBindingsByKey[pair.Value] = pair.Key;
                }
            }

            public static void Func_RestoreDefaultKeyBindings()
            {
                KeyBindingsByAction = new Dictionary<int, KeyCode>(DefaultKeyBindings);

                KeyBindingsByKey = new Dictionary<KeyCode, int>();
                foreach (var pair in KeyBindingsByAction)
                {
                    KeyBindingsByKey[pair.Value] = pair.Key;
                }
            }
        
            public static void Func_ChangeKeyBindings(KeyAction action, KeyCode keyBind)
            {
                Func_ChangeKeyBindingsDictionary(action.code, keyBind);
            }

            public static void Func_SwapKeyBindings(KeyAction action1, KeyAction action2)
            {
                KeyCode key1 = Func_GetKeyBindingByAction(action1);
                KeyCode key2 = Func_GetKeyBindingByAction(action2);

                Func_ChangeKeyBindings(action1, key2);
                Func_ChangeKeyBindings(action2, key1);
            }

            public static void Func_SwapKeyBindings(KeyAction action, KeyCode newKey)
            {
                KeyCode oldKey = Func_GetKeyBindingByAction(action);
                int oldAction = Func_GetKeyBindingByKey(newKey);

                Func_ChangeKeyBindingsDictionary(oldAction, oldKey);
                Func_ChangeKeyBindingsDictionary(action.code, newKey);
            }

            public static bool Func_DoesKeyBindingExists(KeyAction newAction, KeyCode newKey)
            {
                return (KeyBindingsByKey.TryGetValue(newKey, out int function) && function != newAction.code);
            }

            public static int Func_GetKeyBindingByKey(KeyCode key)
            {
                return KeyBindingsByKey[key];
            }
        
            public static KeyCode Func_GetKeyBindingByAction(KeyAction action)
            {
                return KeyBindingsByAction[action.code];
            }
        
            private static void Func_ChangeKeyBindingsDictionary(int function, KeyCode keyBind)
            {
                KeyBindingsByAction[function] = keyBind;
                KeyBindingsByKey[keyBind] = function;
            }
        }
    }
} 