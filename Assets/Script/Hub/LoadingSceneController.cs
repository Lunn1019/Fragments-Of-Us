using InTerra.EntitySDK;
using InTerra.SceneAndUiSDK;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] private List<GameObject> loadScreens = new List<GameObject>();
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI progressText;

    private void Awake()
    {
        int chosenIndex = Comp_Object.Mod_PoolingRandom.Func_RandomizePool<int>(MapIndexes(), 1, true)[0];
        EnableScreen(chosenIndex);
        Comp_BaseCanvas.Mod_Loading.Func_SetDefaultScene("HomeScene");
        StartCoroutine(Comp_BaseCanvas.Mod_Loading.Func_AsyncLoadTargetScene(1f));
    }

    private void Update()
    {
        ShowProgress(Comp_BaseCanvas.Mod_Loading.Func_ReadAsyncLoadProgress());
    }

    private void ShowProgress(float progress)
    {
        float progressPercentage = progress * 100;

        slider.value = progress;
        progressText.text = progressPercentage.ToString() + " %";
    }

    private List<int> MapIndexes()
    {
        List<int> list = new List<int>();
        list.Add(0);
        list.Add(1);
        list.Add(2);

        return list;
    }

    private void EnableScreen(int chosenIndex)
    {
        if (chosenIndex == 0)
        {
            loadScreens[0].SetActive(true);
            loadScreens[1].SetActive(false);
            loadScreens[2].SetActive(false);
        }
        else if (chosenIndex == 1)
        {
            loadScreens[0].SetActive(false);
            loadScreens[1].SetActive(true);
            loadScreens[2].SetActive(false);
        }
        else
        {
            loadScreens[0].SetActive(false);
            loadScreens[1].SetActive(false);
            loadScreens[2].SetActive(true);
        }
    }
}
