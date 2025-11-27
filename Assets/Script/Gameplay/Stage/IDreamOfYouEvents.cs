using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDreamOfYouEvents : MonoBehaviour, IEventsMapper
{
    public const float firstCheckPointFinished = 77f;
    public const float secondCheckPointStart = 78.9f;
    public const float secondCheckPointFinished = 151.2f;
    public const float thirdCheckPointStart = 153f;
    public const float thirdCheckPointFinished = 218f;

    List<float> IEventsMapper.GetCheckPoints()
    {
        List<float> checkPoints = new List<float>();

        checkPoints.Add(firstCheckPointFinished);
        checkPoints.Add(secondCheckPointFinished);
        checkPoints.Add(thirdCheckPointFinished);

        return checkPoints;
    }

    List<int> IEventsMapper.GetCheckScores()
    {
        List<int> scores = new List<int>();
        scores.Add(600);
        scores.Add(1200);
        scores.Add(1800);

        return scores;
    }

    void IEventsMapper.SetScoreSystem()
    {
        if(THISHUB.saveData.Data_Game == null || THISHUB.saveData.Data_Game.fragments.Count == 0)
        {
            THISHUB.saveData.Data_Game = THISHUB.Instance.MapBaseSaveData();
        }

        int highScore = THISHUB.saveData.Data_Game.fragments[THISHUB.Instance.MapSongTitleToIndex(SongTitle.IDreamOfYou)].stageDatas[0].highScore;
        Static_Tile.SetInitialHighScore(highScore);
    }

    SongTitle IEventsMapper.GetSongTitle()
    {
        return SongTitle.IDreamOfYou;
    }

    List<EventData> IEventsMapper.GetEventDatas()
    {
        List<EventData> eventDatas = new List<EventData>();

        eventDatas.Add(MapEventData(TileType.Pressed, 1.1f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 2f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 2.9f, 0));
        eventDatas.Add(MapEventData(TileType.Held, 3.3f, .5f));
        eventDatas.Add(MapEventData(TileType.Pressed, 4.2f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 4.5f, 0));
        eventDatas.Add(MapEventData(TileType.Held, 4.9f, 1));
        eventDatas.Add(MapEventData(TileType.Pressed, 5.6f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 6.5f, 0));
        eventDatas.Add(MapEventData(TileType.Held, 6.7f, 1));
        eventDatas.Add(MapEventData(TileType.Pressed, 7.5f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 8.3f, 0));
        eventDatas.Add(MapEventData(TileType.Held, 8.7f, 1f));
        eventDatas.Add(MapEventData(TileType.Pressed, 9.5f, 0));
        eventDatas.Add(MapEventData(TileType.Held, 10.5f, .5f));
        eventDatas.Add(MapEventData(TileType.Pressed, 11f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 12f, 0));
        eventDatas.Add(MapEventData(TileType.Held, 12.5f, .5f));
        eventDatas.Add(MapEventData(TileType.Pressed, 13.5f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 13.9f, 0));
        eventDatas.Add(MapEventData(TileType.Held, 14.3f, 1f));
        eventDatas.Add(MapEventData(TileType.Pressed, 15.1f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 16.2f, 0));
        eventDatas.Add(MapEventData(TileType.Held, 16.5f, 1f));
        eventDatas.Add(MapEventData(TileType.Pressed, 17.6f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 18.2f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 18.8f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 19.4f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 20f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 20.6f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 21.2f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 21.8f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 22.4f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 23f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 23.6f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 24.2f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 24.8f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 25.4f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 26f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 26.6f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 27.2f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 27.8f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 28.4f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 29f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 29.6f, 0));
        eventDatas.Add(MapEventData(TileType.Held, 30.2f, 1f));
        eventDatas.Add(MapEventData(TileType.Pressed, 31.6f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 32.2f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 33.2f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 33.8f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 34.4f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 35f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 36f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 36.6f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 37.2f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 37.8f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 38.4f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 39f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 40f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 40.6f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 41.2f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 41.8f, 0));
        eventDatas.Add(MapEventData(TileType.Held, 42.2f, 2f));
        eventDatas.Add(MapEventData(TileType.Pressed, 44f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 45f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 45.6f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 46.2f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 46.8f, 0));
        eventDatas.Add(MapEventData(TileType.Held, 47.3f, 2f));
        eventDatas.Add(MapEventData(TileType.Pressed, 50f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 51f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 51.6f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 52.2f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 52.6f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 53f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 53.4f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 54f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 54.5f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 55f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 55.6f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 56.2f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 56.8f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 57.4f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 58f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 58.5f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 59f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 59.6f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 60.2f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 60.8f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 61.4f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 62f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 62.5f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 63f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 63.6f, 0));
        eventDatas.Add(MapEventData(TileType.Held, 64.3f, 2f));
        eventDatas.Add(MapEventData(TileType.Pressed, 66.6f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 67.2f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 67.8f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 68.4f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 69f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 69.5f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 70f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 70.6f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 71.2f, 0));
        eventDatas.Add(MapEventData(TileType.Pressed, 71.8f, 0));
        eventDatas.Add(MapEventData(TileType.Held, 72.3f, 3.5f));

        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 1.1f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 2.9f), 0));
        eventDatas.Add(MapEventData(TileType.Held, (secondCheckPointStart + 3.3f), 1f));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 4.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 4.5f), 0));
        eventDatas.Add(MapEventData(TileType.Held, (secondCheckPointStart + 4.9f), 1));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 5.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 6.5f), 0));
        eventDatas.Add(MapEventData(TileType.Held, (secondCheckPointStart + 6.7f), 1));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 7.5f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 8.3f), 0));
        eventDatas.Add(MapEventData(TileType.Held, (secondCheckPointStart + 8.7f), 1f));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 9.5f), 0));
        eventDatas.Add(MapEventData(TileType.Held, (secondCheckPointStart + 10.5f), .5f));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 11f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 12f), 0));
        eventDatas.Add(MapEventData(TileType.Held, (secondCheckPointStart + 12.5f), .5f));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 13.5f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 13.9f), 0));
        eventDatas.Add(MapEventData(TileType.Held, (secondCheckPointStart + 14.3f), .5f));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 15.1f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 16.2f), 0));
        eventDatas.Add(MapEventData(TileType.Held, (secondCheckPointStart + 16.5f), .5f));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 17.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 18.1f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 18.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 19.1f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 19.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 20f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 20.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 21f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 21.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 21.9f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 22.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 23f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 23.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 23.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 24.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 24.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 25.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 25.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 26f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 26.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 26.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 27.3f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 27.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 28.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 29f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 29.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 29.8f), 0));
        eventDatas.Add(MapEventData(TileType.Held, (secondCheckPointStart + 30.2f), 1f));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 31.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 32.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 33.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 33.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 34.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 35f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 35.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 35.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 36.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 36.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 37.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 37.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 38.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 39f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 39.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 39.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 40.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 40.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 41.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 41.8f), 0));
        eventDatas.Add(MapEventData(TileType.Held, (secondCheckPointStart + 42.2f), 2f));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 44f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 45f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 45.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 46.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 46.8f), 0));
        eventDatas.Add(MapEventData(TileType.Held, (secondCheckPointStart + 47.3f), 2f));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 50f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 50.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 50.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 51.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 51.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 52.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 52.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 53.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 54f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 54.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 54.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 55.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 55.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 56.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 56.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 57.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 58f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 58.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 58.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 59.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 59.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 60.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 60.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 61.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 62f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 62.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 62.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 63.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 63.6f), 0));
        eventDatas.Add(MapEventData(TileType.Held, (secondCheckPointStart + 64.3f), 2f));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 66.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 67.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 67.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 68.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 68.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 69.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 69.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 70f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 70.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 71.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (secondCheckPointStart + 71.8f), 0));
        eventDatas.Add(MapEventData(TileType.Held, (secondCheckPointStart + 72.3f), 3.5f));

        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 1.1f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 2.9f), 0));
        eventDatas.Add(MapEventData(TileType.Held,    (thirdCheckPointStart + 3.3f), 1f));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 4.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 4.5f), 0));
        eventDatas.Add(MapEventData(TileType.Held,    (thirdCheckPointStart + 4.9f), 1));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 5.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 6.5f), 0));
        eventDatas.Add(MapEventData(TileType.Held,    (thirdCheckPointStart + 6.7f), 1));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 7.5f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 8.3f), 0));
        eventDatas.Add(MapEventData(TileType.Held,    (thirdCheckPointStart + 8.7f), 1f));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 9.5f), 0));
        eventDatas.Add(MapEventData(TileType.Held,    (thirdCheckPointStart + 10.5f), .5f));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 11f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 12f), 0));
        eventDatas.Add(MapEventData(TileType.Held,    (thirdCheckPointStart + 12.5f), .5f));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 13.5f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 13.9f), 0));
        eventDatas.Add(MapEventData(TileType.Held,    (thirdCheckPointStart + 14.3f), .5f));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 15.1f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 16.2f), 0));
        eventDatas.Add(MapEventData(TileType.Held,    (thirdCheckPointStart + 16.5f), .5f));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 17.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 18.1f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 18.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 19.1f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 19.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 20f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 20.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 21f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 21.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 21.9f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 22.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 23f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 23.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 23.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 24.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 24.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 25.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 25.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 26f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 26.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 26.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 27.3f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 27.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 28.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 29f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 29.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 29.8f), 0));
        eventDatas.Add(MapEventData(TileType.Held,    (thirdCheckPointStart + 30.2f), 1f));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 31.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 32.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 33.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 33.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 34.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 35f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 35.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 35.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 36.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 36.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 37.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 37.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 38.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 39f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 39.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 39.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 40.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 40.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 41.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 41.8f), 0));
        eventDatas.Add(MapEventData(TileType.Held,    (thirdCheckPointStart + 42.2f), 2f));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 44f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 45f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 45.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 46.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 46.8f), 0));
        eventDatas.Add(MapEventData(TileType.Held,    (thirdCheckPointStart + 47.3f), 2f));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 50f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 50.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 50.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 51.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 51.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 52.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 52.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 53.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 54f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 54.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 54.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 55.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 55.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 56.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 56.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 57.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 58f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 58.4f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 58.8f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 59.2f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 59.6f), 0));
        eventDatas.Add(MapEventData(TileType.Pressed, (thirdCheckPointStart + 60.2f), 0));
        eventDatas.Add(MapEventData(TileType.Held, (thirdCheckPointStart + 60.8f), 1));

        return eventDatas;
    }

    public EventData MapEventData(TileType tileType, float timetoSpawn, float holdDuration)
    {
        EventData eventData = new EventData();
        eventData.tileType = tileType;
        eventData.timetoSpawn = timetoSpawn;
        eventData.holdDuration = holdDuration;

        return eventData;
    }
}
