using InTerra.HUB;
using InTerra.InputSDK;
using UnityEngine;

public class testDekstopInput : MonoBehaviour
{
    void Awake()
    {
        InterraHUB.Base_SaveData<testData> saveData = new InterraHUB.Base_SaveData<testData>();
        InterraHUB.Func_InitGameData<testData>(saveData);

        Comp_Dekstop.Mod_KeyBindings.Func_SwapKeyBindings(Comp_Dekstop.Mod_KeyBindings.Static_Actions.Act_Movement.FORWARD, KeyCode.G);
    }

    void Update()
    {
        if (Comp_Dekstop.Mod_KeyboardEvents.Func_IsKeyHeldLongEnough(Comp_Dekstop.Mod_KeyBindings.Static_Actions.Act_Movement.FORWARD, 3, false))
        {
            Debug.Log("key w pressed");
        }

        if (Comp_Dekstop.Mod_KeyboardEvents.Func_IsKeyHeldWithinDuration(Comp_Dekstop.Mod_KeyBindings.Static_Actions.Act_Movement.LEFT, 1, false))
        { 
            Debug.Log("key a pressed");
        }

        if (Comp_Dekstop.Mod_KeyboardEvents.Func_IsKeyDown(Comp_Dekstop.Mod_KeyBindings.Static_Actions.Act_Combat.PRIMARY))
        {
            Debug.Log("mouse0 pressed");
        }

        if (Comp_Dekstop.Mod_KeyboardEvents.Func_IsKeyHeld(Comp_Dekstop.Mod_KeyBindings.Static_Actions.Act_Movement.RIGHT))
        {
            Debug.Log("key d pressed");
        }
    }
}

public class testData
{

}
