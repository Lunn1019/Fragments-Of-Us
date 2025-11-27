using System;

namespace InTerra.TimeSDK
{
    [Serializable]
    public class Base_TimeAttributes
    {
        public float dayDuration = 120f;
        public float startTimeOfDay = 6f;
    }
    
    public static class Static_TimeAttributes
    {
        public const float fullDay = 24f;
    }
}