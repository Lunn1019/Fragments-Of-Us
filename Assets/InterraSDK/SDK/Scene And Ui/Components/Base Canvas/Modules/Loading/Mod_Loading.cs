using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using InTerra.HUB;

namespace InTerra.SceneAndUiSDK
{
    public static partial class Comp_BaseCanvas
    {
        public static partial class Mod_Loading
        {
            #region APIs
            public static void Func_LoadLoadingScene(string targetScene, string loadingScene)
            {
                Static_LoadingAttributes.targetScene = targetScene;
                SceneManager.LoadScene(loadingScene);
            }

            public static void Func_SetDefaultScene(string targetScene)
            {
                if (string.IsNullOrEmpty(Static_LoadingAttributes.targetScene))
                {
                    Static_LoadingAttributes.targetScene = targetScene;
                }
            }

            public static IEnumerator Func_AsyncLoadTargetScene(float delayTime)
            {
                Static_LoadingAttributes.isCleaningGC = true;
                Static_LoadingAttributes.loadingProgress = 0;
                yield return new WaitForSeconds(delayTime);


                _ = InterraHUB.Func_ClearGarbage();
                AsyncOperation operation = SceneManager.LoadSceneAsync(Static_LoadingAttributes.targetScene);
                operation.allowSceneActivation = false;

                yield return new WaitForSeconds(0.5f);
                while (!operation.isDone || Static_LoadingAttributes.isCleaningGC)
                {
                    float progress = Mathf.Clamp01(operation.progress / 0.9f);
                    Static_LoadingAttributes.loadingProgress = progress;

                    if (operation.progress >= 0.9f && !Static_LoadingAttributes.isCleaningGC)
                    {
                        yield return new WaitForSeconds(delayTime);
                        operation.allowSceneActivation = true;
                    }

                    yield return null;
                }
            }

            public static float Func_ReadAsyncLoadProgress()
            {
                return Static_LoadingAttributes.loadingProgress;
            }

            public static void FinishCleaningGC()
            {
                Static_LoadingAttributes.isCleaningGC = false;
            }
            #endregion
        }
    }
}