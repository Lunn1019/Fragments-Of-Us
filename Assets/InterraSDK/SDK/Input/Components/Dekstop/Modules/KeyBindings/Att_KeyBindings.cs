using System.Collections.Generic;
using UnityEngine;

namespace InTerra.InputSDK
{
	public static partial class Comp_Dekstop
	{
		public static partial class Mod_KeyBindings
		{
			private static Dictionary<int, KeyCode> KeyBindingsByAction = new Dictionary<int, KeyCode>();
            private static Dictionary<KeyCode, int> KeyBindingsByKey = new Dictionary<KeyCode, int>();

            public static class Static_Actions
            {
                public class KeyAction
                {
                    public readonly int code;

                    public KeyAction(int value)
                    {
                        this.code = value;
                    }
                }

                public static class Act_Movement
                {
                    // Code 0–30
                    public static readonly KeyAction FORWARD = new(0);
                    public static readonly KeyAction BACKWARD = new(1);
                    public static readonly KeyAction LEFT = new(2);
                    public static readonly KeyAction RIGHT = new(3);
                    public static readonly KeyAction JUMP = new(4);
                    public static readonly KeyAction SPRINT = new(5);
                }

                public static class Act_General
                {
                    // Code 31–60
                    public static readonly KeyAction INTERACT = new(31);
                    public static readonly KeyAction USE = new(32);
                    public static readonly KeyAction DROP = new(33);
                }

                public static class Act_Combat
                {
                    // Code 61–90
                    public static readonly KeyAction PRIMARY = new(61);
                    public static readonly KeyAction SECONDARY = new(62);
                    public static readonly KeyAction SWITCH = new(63);
                }

                public static class Act_Utility
                {
                    // Code 91–120
                    public static readonly KeyAction PAUSE = new(91);
                }
            }

            public static Dictionary<int, KeyCode> DefaultKeyBindings = new()
            {
                { Static_Actions.Act_Movement.FORWARD.code, KeyCode.W },
                { Static_Actions.Act_Movement.BACKWARD.code, KeyCode.S },
                { Static_Actions.Act_Movement.LEFT.code, KeyCode.A },
                { Static_Actions.Act_Movement.RIGHT.code, KeyCode.D },
                { Static_Actions.Act_Movement.JUMP.code, KeyCode.Space },
                { Static_Actions.Act_Movement.SPRINT.code, KeyCode.LeftShift },

                { Static_Actions.Act_General.INTERACT.code, KeyCode.F },
                { Static_Actions.Act_General.USE.code, KeyCode.E },
                { Static_Actions.Act_General.DROP.code, KeyCode.G },

                { Static_Actions.Act_Combat.PRIMARY.code, KeyCode.Mouse0 },
                { Static_Actions.Act_Combat.SECONDARY.code, KeyCode.Mouse1 },
                { Static_Actions.Act_Combat.SWITCH.code, KeyCode.Tab },

                { Static_Actions.Act_Utility.PAUSE.code, KeyCode.Escape }


            };
        }
    }
}