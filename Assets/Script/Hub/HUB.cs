using InTerra.FilesSDK;
using InTerra.HUB;
using System;
using System.Collections.Generic;
using UnityEngine;

public class THISHUB : MonoBehaviour
{
    public static THISHUB Instance;
    public static bool isInstantiated = false;

    public static InterraHUB.Base_SaveData<SaveDataClass> saveData = new InterraHUB.Base_SaveData<SaveDataClass>();

    private void Awake()
    {
        Instance = this;
        saveData = Comp_IO.Mod_SaveData.Func_Load<SaveDataClass>();
        
        if(saveData == null)
        {
            saveData = new InterraHUB.Base_SaveData<SaveDataClass>();
        }

        if(saveData.Data_Game == null || saveData.Data_Game.isNewData)
        {
            saveData.Data_Game = new SaveDataClass();
            saveData.Data_Game = MapBaseSaveData();
        }

        Comp_IO.Mod_SaveData.Func_Save<SaveDataClass>(saveData);

        isInstantiated = true;
    }

    public void DestroyHub()
    {
        Destroy(this.gameObject);
    }

    public void SaveData()
    {
        saveData.Data_Game.isNewData = false;
        Comp_IO.Mod_SaveData.Func_Save<SaveDataClass>(saveData);
    }

    public SaveDataClass MapBaseSaveData()
    {
        SaveDataClass saveDataClass = new SaveDataClass();

        saveDataClass.fragments.Add(MapBaseFragment(FragmentType.Dreams, 100));
        saveDataClass.fragments[0].stageDatas.Add(MapBaseStageData(SongTitle.IDreamOfYou, "I Dream Of You", 100));

        saveDataClass.fragments.Add(MapBaseFragment(FragmentType.Love, 100));
        saveDataClass.fragments[1].stageDatas.Add(MapBaseStageData(SongTitle.OnlyUs, "Only Us", 100));

        return saveDataClass;
    }

    public void UpdateSaveData(SongTitle song, bool completed, int jellyIndex, int highScore)
    {
        int index = MapSongTitleToIndex(song);

        saveData.Data_Game.fragments[index].stageDatas[0].completed = completed;
        saveData.Data_Game.fragments[index].stageDatas[0].jellyIndex = Math.Max(jellyIndex, saveData.Data_Game.fragments[index].stageDatas[0].jellyIndex);
        saveData.Data_Game.fragments[index].stageDatas[0].highScore = Math.Max(highScore, saveData.Data_Game.fragments[index].stageDatas[0].highScore);
    }

    private Fragment MapBaseFragment(FragmentType type, int coinsRequired)
    {
        Fragment fragment = new Fragment();
        fragment.fragmentType = type;
        fragment.unlocked = false;
        fragment.coinsRequired = coinsRequired;
        fragment.stageDatas = new List<StageData>();

        return fragment;
    }

    private StageData MapBaseStageData(SongTitle song, string songName, int coins)
    {
        StageData stageData = new StageData();
        stageData.songTitle = song;
        stageData.songName = songName;
        stageData.coinsRequired = coins;
        stageData.unlocked = false;
        stageData.highScore = 0;

        return stageData;
    }

    public int MapSongTitleToIndex(SongTitle song)
    {
        if(song == SongTitle.IDreamOfYou)
        {
            return 0;
        }

        if(song == SongTitle.OnlyUs)
        {
            return 1;
        }

        return 0;
    }

    public InterraHUB.Base_SaveData<SaveDataClass> ReadSaveData()
    {
        return saveData;
    }

    private void OnApplicationQuit()
    {
        Comp_IO.Mod_SaveData.Func_Save<SaveDataClass>(saveData);
    }

    public class SaveDataClass
    {
        public List<Fragment> fragments = new List<Fragment>();
        public int currentCoins = 0;
        public bool isNewData = true;
    }

    public class StageData
    {
        public SongTitle songTitle = SongTitle.IDreamOfYou;
        public string songName = "";
        public int highScore = 0;
        public int coinsRequired = 0;
        public bool unlocked = false;
        public bool completed = false;
        public int jellyIndex = 0;
    }

    public class Fragment
    {
        public FragmentType fragmentType = FragmentType.Joy;
        public List<StageData> stageDatas = new List<StageData>();
        public int coinsRequired = 0;
        public bool unlocked = false;
    }

    public enum FragmentType
    {
        Joy,
        Love,
        Dreams,
        Determination,
        Uncertainty,
        Pain
    }

}
public enum SongTitle
{
    IDreamOfYou,
    OnlyUs
}
