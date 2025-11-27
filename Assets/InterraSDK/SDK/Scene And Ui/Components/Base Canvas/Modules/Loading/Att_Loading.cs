namespace InTerra.SceneAndUiSDK
{
    public static partial class Comp_BaseCanvas
    {
        public static partial class Mod_Loading
        {
            #region private
            private static class Static_LoadingAttributes
            {
                public static string targetScene = "";
                public static bool isCleaningGC = false;
                public static float loadingProgress = 0;
            }
            #endregion
        }
    }
}