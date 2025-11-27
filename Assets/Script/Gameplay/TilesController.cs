
using InTerra.EntitySDK;
using InTerra.SceneAndUiSDK;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TilesController : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private GameObject pressedPrefab;
    [SerializeField] private GameObject heldPrefab;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform pressedPool;
    [SerializeField] private Transform heldPool;
    [SerializeField] private Transform arrowPool;
    [SerializeField] private float timeNeeded;
    [SerializeField] private float arrowSpeed;
    private List<float> speedNeeded = new List<float>();
    [SerializeField] private List<Transform> startList;
    [SerializeField] private List<Transform> endList;
    [SerializeField] private List<Transform> clickList;
    public List<EventData> eventDatas = new List<EventData>();
    [SerializeField] private float delayTime;
    private bool isStillDelayed;
    [SerializeField] TextMeshProUGUI intScoreText;
    [SerializeField] TextMeshProUGUI intHighScoreText;
    [SerializeField] ComboAnimation comboObject;
    [SerializeField] TextMeshProUGUI comboText;

    [SerializeField] private float audioDelayTime;
    [SerializeField] private VideoPlayer video;

    List<float> checkPoints = new List<float>();
    List<int> checkScores = new List<int>();

    private SongTitle songTitle;

    public bool isLane1Available = false;
    public bool isLane2Available = false;
    public bool isLane3Available = false;
    public bool isLane4Available = false;
    public bool isLane5Available = false;
    public bool isLane6Available = false;
    public float lane1SpawnDownTime = 0f;
    public float lane2SpawnDownTime = 0f;
    public float lane3SpawnDownTime = 0f;
    public float lane4SpawnDownTime = 0f;
    public float lane5SpawnDownTime = 0f;
    public float lane6SpawnDownTime = 0f;
    public float lane1DownTime = 0;
    public float lane2DownTime = 0;
    public float lane3DownTime = 0;
    public float lane4DownTime = 0;
    public float lane5DownTime = 0;
    public float lane6DownTime = 0;

    public float spawnDownTime = 1.5f;

    private int currentIndex;
    private int checkPointIndex = 0;
    private int checkScoreIndex = 0;
    [SerializeField] private List<GameObject> jellyFish;
    [SerializeField] private List<Image> jellyImgs;

    private void Awake()
    {
        checkPointIndex = 0;
        checkScoreIndex = 0;
        Static_Tile.intHighScoreText = intHighScoreText;
        Static_Tile.intScoreText = intScoreText;
        Static_Tile.comboObject = comboObject.gameObject;
        Static_Tile.intComboText = comboText;
        comboObject.InitScale();
        MapEventDatasAndCheckpoints();
        video.Stop();
        isStillDelayed = true;
        StartCoroutine(CreateAndInstantiatePools());
        currentIndex = 0;
        time = 0;
        Static_Tile.ResetScore();
        Static_Tile.ChangeCombo(false);
        isLane1Available = true;
        isLane2Available = true;
        isLane3Available = true;
        isLane4Available = true;
        isLane5Available = true;
        isLane6Available = true;
        jellyFish[0].SetActive(false);
        jellyFish[1].SetActive(false);
        jellyFish[2].SetActive(false);
        endPanel.SetActive(false);
        Static_Tile.Attributes.timeNeeded = timeNeeded;

        for(int i = 0; i < 6; i++)
        {
            float distance = Vector3.Distance(startList[i].position, clickList[i].position);
            speedNeeded.Add(distance / timeNeeded);
        }

        Static_Tile.Attributes.arrowSpeed = arrowSpeed;

        StartCoroutine(DelayStage());
        StartCoroutine(DelayAudio());

    }

    private void Update()
    {
        if (!Static_Tile.isTilesLoaded || isStillDelayed)
            return;

        time += Time.deltaTime;
        SpawnTiles();
        RunLaneDownTime();
        CheckPointEvents();
        CheckScores();
    }

    private void CheckScores()
    {
        int score = Static_Tile.GetScore();

        if(checkScoreIndex >= checkScores.Count)
        {
            return;
        }

        if(score > checkScores[checkScoreIndex])
        {
            checkScoreIndex++;
        }
    }

    public void BackToHome()
    {
        THISHUB.Instance.DestroyHub();
        Comp_BaseCanvas.Mod_Loading.Func_LoadLoadingScene("HomeScene", "LoadingScreen");
    }

    private void CheckPointEvents()
    {
        if(checkPointIndex >= checkPoints.Count)
        {
            endPanel.SetActive(true);
            Static_Tile.EndPanelAtt.checkScoreIndex = checkScoreIndex;
            THISHUB.Instance.UpdateSaveData(songTitle, true, checkScoreIndex, Static_Tile.GetHighScore());
            THISHUB.Instance.SaveData();
            return;
        }

        if (time >= checkPoints[checkPointIndex])
        {
            int index = Math.Min(checkScoreIndex, 3);
            StartCoroutine(ShowJellyFish(jellyFish[index], jellyImgs[index]));
            checkPointIndex++;
        } 
    }

    private IEnumerator ShowJellyFish(GameObject jelly, Image img)
    {
        float jellyTime = 0;

        jelly.SetActive(true);
        while (jellyTime < 2f)
        {
            jellyTime += Time.deltaTime;

            Color c = img.color;
            c.a = jellyTime / 2f;
            img.color = c;

            yield return null;
        }

        while (jellyTime > 0f)
        {
            jellyTime -= Time.deltaTime;

            Color c = img.color;
            c.a = jellyTime / 2f;
            img.color = c;

            yield return null;
        }

        isStillDelayed = false;
    }

    private IEnumerator DelayStage()
    {
        while (delayTime > 0)
        {
            delayTime -= Time.deltaTime;
            yield return null;
        }

        isStillDelayed = false;
    }

    private IEnumerator DelayAudio()
    {
        while (audioDelayTime > 0)
        {
            audioDelayTime -= Time.deltaTime;
            yield return null;
        }

        video.Play();
    }

    public IEnumerator CreateAndInstantiatePools()
    {
        Comp_Object.Mod_PoolingLIFO.Func_CreatePool<Tile>(TileType.Pressed, pressedPrefab, true, pressedPool);
        Comp_Object.Mod_PoolingLIFO.Func_CreatePool<Tile>(TileType.Held, heldPrefab, true, heldPool);
        Comp_Object.Mod_PoolingLIFO.Func_CreatePool<Arrow>(TileType.Arrow, arrowPrefab, true, arrowPool);

        yield return Comp_Object.Mod_PoolingLIFO.Func_InstantiateObjects<Tile>(TileType.Pressed, 15);
        yield return Comp_Object.Mod_PoolingLIFO.Func_InstantiateObjects<Tile>(TileType.Held, 10);
        yield return Comp_Object.Mod_PoolingLIFO.Func_InstantiateObjects<Arrow>(TileType.Arrow, 40);

        Comp_Object.Mod_PoolingFIFO.Func_CreateEmptyPool<Tile>(LaneIndex.Lane1);
        Comp_Object.Mod_PoolingFIFO.Func_CreateEmptyPool<Tile>(LaneIndex.Lane2);
        Comp_Object.Mod_PoolingFIFO.Func_CreateEmptyPool<Tile>(LaneIndex.Lane3);
        Comp_Object.Mod_PoolingFIFO.Func_CreateEmptyPool<Tile>(LaneIndex.Lane4);
        Comp_Object.Mod_PoolingFIFO.Func_CreateEmptyPool<Tile>(LaneIndex.Lane5);
        Comp_Object.Mod_PoolingFIFO.Func_CreateEmptyPool<Tile>(LaneIndex.Lane6);

        Static_Tile.isTilesLoaded = true;
    }

    public IEnumerator RunArrows(Transform startTransform, Transform endTransform, float arrowTime, float spawnTime, Tile tile, int laneIndex, LaneIndex laneEnum)
    {
        float spawnTimeCopy = spawnTime;
        arrowTime = Mathf.Max(arrowTime, timeNeeded);

        while(arrowTime > 0)
        {
            if (!tile.gameObject.activeSelf)
            {
                startTransform = clickList[laneIndex];
            }

            arrowTime -= Time.deltaTime;
            spawnTimeCopy -= Time.deltaTime;

            if(spawnTimeCopy <= 0)
            {
                spawnTimeCopy = spawnTime;

                if (Comp_Object.Mod_PoolingLIFO.Func_TryGetObject<Arrow>(TileType.Arrow, startTransform.position, startTransform.rotation, out Arrow arrow))
                {
                    arrow.startTransform = startTransform;
                    arrow.targetTransform = endTransform;
                    arrow.laneIndex = laneIndex;
                    arrow.laneEnum = laneEnum;
                    arrow.RotateArrow();

                    arrow.gameObject.SetActive(true);
                }
            }

            yield return null;
        }
    }

    public void RunLaneDownTime()
    {
        if (!isLane1Available)
        {
            if(lane1DownTime < (spawnDownTime + lane1SpawnDownTime))
            {
                lane1DownTime += Time.deltaTime;
            }
            else
            {
                lane1DownTime = 0;
                lane1SpawnDownTime = 0;
                isLane1Available = true;
            }
        }

        if (!isLane2Available)
        {
            if (lane2DownTime < (spawnDownTime + lane2SpawnDownTime))
            {
                lane2DownTime += Time.deltaTime;
            }
            else
            {
                lane2DownTime = 0;
                lane2SpawnDownTime = 0;
                isLane2Available = true;
            }
        }

        if (!isLane3Available)
        {
            if (lane3DownTime < (spawnDownTime + lane3SpawnDownTime))
            {
                lane3DownTime += Time.deltaTime;
            }
            else
            {
                lane3DownTime = 0;
                lane3SpawnDownTime = 0;
                isLane3Available = true;
            }
        }

        if (!isLane4Available)
        {
            if (lane4DownTime < (spawnDownTime + lane4SpawnDownTime))
            {
                lane4DownTime += Time.deltaTime;
            }
            else
            {
                lane4DownTime = 0;
                lane4SpawnDownTime = 0;
                isLane4Available = true;
            }
        }

        if (!isLane5Available)
        {
            if (lane5DownTime < (spawnDownTime + lane5SpawnDownTime))
            {
                lane5DownTime += Time.deltaTime;
            }
            else
            {
                lane5DownTime = 0;
                lane5SpawnDownTime = 0;
                isLane5Available = true;
            }
        }

        if (!isLane6Available)
        {
            if (lane6DownTime < (spawnDownTime + lane6SpawnDownTime))
            {
                lane6DownTime += Time.deltaTime;
            }
            else
            {
                lane6DownTime = 0;
                lane6SpawnDownTime = 0;
                isLane6Available = true;
            }
        }
    }

    public void SpawnTiles()
    {
        if (currentIndex >= eventDatas.Count || eventDatas[currentIndex].timetoSpawn > time)
            return;

        int laneIndex = Comp_Object.Mod_PoolingRandom.Func_RandomizePool<int>(MapAvailableLanes(), 1, true)[0];

        Comp_Object.Mod_PoolingLIFO.Func_TryGetObject<Tile>(eventDatas[currentIndex].tileType, startList[laneIndex].position, startList[laneIndex].rotation, out Tile tile);

        tile.startTransform = startList[laneIndex];
        tile.targetTransform = endList[laneIndex];
        tile.clickTransform = clickList[laneIndex];
        tile.laneIndex = laneIndex;
        tile.speed = speedNeeded[laneIndex];

        Static_Tile.MapLane(laneIndex, out LaneIndex laneIndexEnum);
        MapLaneDownTime(laneIndexEnum, eventDatas[currentIndex].holdDuration);

        if (eventDatas[currentIndex].tileType == TileType.Pressed)
        {
            tile.holdIndex = 0;
            tile.gameObject.SetActive(true);
        }
        else
        {
            tile.holdIndex = 1;
            tile.holdDuration = eventDatas[currentIndex].holdDuration;
            tile.gameObject.SetActive(true);
            StartCoroutine(Static_Tile.SpawnSecondHoldTile(startList[laneIndex], eventDatas[currentIndex].holdDuration, laneIndex, endList[laneIndex], clickList[laneIndex], speedNeeded[laneIndex]));
            StartCoroutine(RunArrows(tile.transform, startList[laneIndex], eventDatas[currentIndex].holdDuration, .2f, tile, laneIndex, laneIndexEnum));
        }

        Comp_Object.Mod_PoolingFIFO.Func_ReturnObject<Tile>(laneIndexEnum, tile);

        currentIndex++;
    }

    public void MapLaneDownTime(LaneIndex laneIndex, float downTime)
    {
        if(laneIndex == LaneIndex.Lane1)
        {
            lane1SpawnDownTime = downTime;
            isLane1Available = false;
        }
        else if (laneIndex == LaneIndex.Lane2)
        {
            lane2SpawnDownTime = downTime;
            isLane2Available = false;
        }
        else if (laneIndex == LaneIndex.Lane3)
        {
            lane3SpawnDownTime = downTime;
            isLane3Available = false;
        }
        else if (laneIndex == LaneIndex.Lane4)
        {
            lane4SpawnDownTime = downTime;
            isLane4Available = false;
        }
        else if (laneIndex == LaneIndex.Lane5)
        {
            lane5SpawnDownTime = downTime;
            isLane5Available = false;
        }
        else
        {
            lane6SpawnDownTime = downTime;
            isLane6Available = false;
        }
    }

    public void CalculateScore(int points, Tile tile)
    {
        Static_Tile.ChangeScore(points);

        if(points == Static_Tile.Attributes.MISS_POINTS)
        {
            Static_Tile.ChangeCombo(false);
            return;
        }

        Comp_Object.Mod_PoolingLIFO.Func_ReturnObject<Tile>(tile.tileType, tile);
        tile.gameObject.SetActive(false);
        Static_Tile.ChangeCombo(true);
    }

    public void CalculateHoldScore()
    {
        Static_Tile.ChangeScore(Static_Tile.Attributes.OKAY_POINTS);
    }

    public List<int> MapAvailableLanes()
    {
        List<int> availableLanes = new List<int>();

        if (isLane1Available)
            availableLanes.Add(0);

        if (isLane2Available)
            availableLanes.Add(1);

        if (isLane3Available)
            availableLanes.Add(2);

        if (isLane4Available)
            availableLanes.Add(3);

        if (isLane5Available)
            availableLanes.Add(4);

        if (isLane6Available)
            availableLanes.Add(5);

        return availableLanes;
    }

    public void MapEventDatasAndCheckpoints()
    {
        if(!TryGetComponent<IEventsMapper>(out IEventsMapper eventsMapper))
        {
            return;
        }

        eventDatas = eventsMapper.GetEventDatas();
        checkPoints = eventsMapper.GetCheckPoints();
        checkScores = eventsMapper.GetCheckScores();
        songTitle = eventsMapper.GetSongTitle();

        StartCoroutine(SetScoreSystem(eventsMapper));
    }

    public IEnumerator SetScoreSystem(IEventsMapper eventsMapper)
    {
        yield return new WaitUntil(isHubInstantiated);

        eventsMapper.SetScoreSystem();
    }

    private bool isHubInstantiated()
    {
        return THISHUB.isInstantiated;
    }
}

public enum TileType
{
    Held,
    Pressed,
    Arrow
}

public enum LaneIndex
{
    Lane1,
    Lane2,
    Lane3,
    Lane4,
    Lane5,
    Lane6
}

public class EventData
{
    public float timetoSpawn;
    public TileType tileType;
    public float holdDuration;
}