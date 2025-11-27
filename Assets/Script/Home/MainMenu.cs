using InTerra.HUB;
using InTerra.SceneAndUiSDK;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;

    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject specialMenu;
    [SerializeField]
    private GameObject fragmentsMenu;

    [SerializeField] private GameObject onlyUsCompleted;
    [SerializeField] private GameObject iDreamOfYouCompleted;
    
    [SerializeField] private List<GameObject> onlyUsJelly;
    [SerializeField] private List<GameObject> iDreamOfYouJelly;

    [SerializeField]
    private Image loadPanel;

    private bool isSpecialLoaded;
    private bool isSpecialLoading;

    private bool isFragmentsLoaded;
    private bool isFragmentsLoading;

    public void Awake()
    {
        InterraHUB.Func_Singleton(ref Instance, this);

        mainMenu.SetActive(true);
        specialMenu.SetActive(false);
        fragmentsMenu.SetActive(false);
        isSpecialLoaded = false;
        isSpecialLoading = false;

        isFragmentsLoaded = false;
        isFragmentsLoading = false;

        StartCoroutine(RefreshJellyAndCompleted());

        Color tempColor = loadPanel.color;
        tempColor.a = 0f;
        loadPanel.color = tempColor;
    }

    public bool IsHubInstantiated()
    {
        return THISHUB.isInstantiated;
    }

    public IEnumerator RefreshJellyAndCompleted()
    {
        yield return new WaitUntil(IsHubInstantiated);

        RefreshIDreamOfYouJellyAndCompleted();
        RefreshOnlyUsJellyAndCompleted();
    }

    public void RefreshOnlyUsJellyAndCompleted()
    {
        onlyUsCompleted.SetActive(THISHUB.saveData.Data_Game.fragments[THISHUB.Instance.MapSongTitleToIndex(SongTitle.OnlyUs)].stageDatas[0].completed);

        onlyUsJelly[0].SetActive(false);
        onlyUsJelly[1].SetActive(false);
        onlyUsJelly[2].SetActive(false);

        int jellyIndex = THISHUB.saveData.Data_Game.fragments[THISHUB.Instance.MapSongTitleToIndex(SongTitle.OnlyUs)].stageDatas[0].jellyIndex;
        if (jellyIndex <= 0)
        {
            return;
        }

        onlyUsJelly[jellyIndex - 1].SetActive(true);
    }

    public void RefreshIDreamOfYouJellyAndCompleted()
    {
        iDreamOfYouCompleted.SetActive(THISHUB.saveData.Data_Game.fragments[THISHUB.Instance.MapSongTitleToIndex(SongTitle.IDreamOfYou)].stageDatas[0].completed);

        iDreamOfYouJelly[0].SetActive(false);
        iDreamOfYouJelly[1].SetActive(false);
        iDreamOfYouJelly[2].SetActive(false);

        int jellyIndex = THISHUB.saveData.Data_Game.fragments[THISHUB.Instance.MapSongTitleToIndex(SongTitle.IDreamOfYou)].stageDatas[0].jellyIndex;
        if (jellyIndex <= 0)
        {
            return;
        }

        iDreamOfYouJelly[jellyIndex - 1].SetActive(true);
    }

    public void OnClickPlay()
    {
        if(isFragmentsLoading)
        {
            return;
        }

        isFragmentsLoading = true;

        if(isFragmentsLoaded)
        {
            StartCoroutine(CloseFragments());
            return;
        }
        
        StartCoroutine(LoadFragments());
    }

    public void OnClickSpecial()
    {
        if (isSpecialLoading)
        {
            return;
        }

        isSpecialLoading = true;

        if (isSpecialLoaded)
        {
            StartCoroutine(CloseSpecial());
            return;
        }

        StartCoroutine(LoadSpecial());
    }

    public void LoadStage(string stageName)
    {
        THISHUB.Instance.DestroyHub();
        Comp_BaseCanvas.Mod_Loading.Func_LoadLoadingScene(stageName, "LoadingScreen");
    }

    private IEnumerator LoadFragments()
    {
        yield return LoadWithPanel(fragmentsMenu, mainMenu);

        isFragmentsLoaded = true;
        isFragmentsLoading = false;
    }

    private IEnumerator CloseFragments()
    {
        yield return LoadWithPanel(mainMenu, fragmentsMenu);

        isFragmentsLoaded = false;
        isFragmentsLoading = false;
    }

    private IEnumerator LoadSpecial()
    {
        yield return LoadWithPanel(specialMenu, mainMenu);

        isSpecialLoaded = true;
        isSpecialLoading = false;
    }

    private IEnumerator CloseSpecial()
    {
        yield return LoadWithPanel(mainMenu, specialMenu);

        isSpecialLoaded = false;
        isSpecialLoading = false;
    }

    private IEnumerator LoadWithPanel(GameObject loadedObject, GameObject unloadedObject)
    {
        float loadTime = 0f;
        while (loadTime <= 1f)
        {
            loadTime += Time.deltaTime;
            Color tempColor = loadPanel.color;
            tempColor.a = loadTime;
            loadPanel.color = tempColor;
            yield return null;
        }

        loadedObject.SetActive(true);
        unloadedObject.SetActive(false);

        while (loadTime >= 0f)
        {
            loadTime -= Time.deltaTime;
            Color tempColor = loadPanel.color;
            tempColor.a = loadTime;
            loadPanel.color = tempColor;
            yield return null;
        }
    }
}
