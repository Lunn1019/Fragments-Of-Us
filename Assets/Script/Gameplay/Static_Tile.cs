using InTerra.EntitySDK;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public static class Static_Tile
{
    public static bool isTilesLoaded = false;
    public static TextMeshProUGUI intScoreText;
    public static TextMeshProUGUI intHighScoreText;
    public static GameObject comboObject;
    public static TextMeshProUGUI intComboText;
    private static float animationCd = 3;
    
    public static class Attributes
    {
        public static float timeNeeded = 2f;
        public static float arrowSpeed = 150;

        public const float PERFECT_DISTANCE = 10;
        public const float GREAT_DISTANCE = 30;
        public const float OKAY_DISTANCE = 80;

        public const int PERFECT_POINTS = 5;
        public const int GREAT_POINTS = 3;
        public const int OKAY_POINTS = 1;
        public const int MISS_POINTS = -1;
    }

    public static class EndPanelAtt
    {
        public static int checkScoreIndex;
    }

    private static class ScoreSystem
    {
        public static int score = 0;
        public static int highScore = 0;
        public static int tempHighScore = 0;
        public static int comboStreak = 0;
    }

    public static int GetScore()
    {
        return ScoreSystem.score;
    }

    public static int GetHighScore()
    {
        return ScoreSystem.highScore;
    }

    public static void DecreaseAnimationCd()
    {
        if(animationCd <= 0)
        {
            comboObject.gameObject.SetActive(false);
            return;
        }

        animationCd -= Time.deltaTime;
    }

    public static IEnumerator SpawnSecondHoldTile(Transform startTransform, float delayTime, int laneIndex, Transform endTransform, Transform clickTransform, float speed)
    {
        while(delayTime > 0)
        {
            delayTime -= Time.deltaTime;
            yield return null;
        }

        Comp_Object.Mod_PoolingLIFO.Func_TryGetObject<Tile>(TileType.Held, startTransform.position, startTransform.rotation, out Tile tile);
        tile.holdIndex = 2;
        tile.startTransform = startTransform;
        tile.targetTransform = endTransform;
        tile.clickTransform = clickTransform;
        tile.laneIndex = laneIndex;
        tile.speed = speed;
        tile.gameObject.SetActive(true);

        MapLane(laneIndex, out LaneIndex laneIndexEnum);

        Comp_Object.Mod_PoolingFIFO.Func_ReturnObject<Tile>(laneIndexEnum, tile);
    }

    public static void DisplayScore()
    {
        intScoreText.text = ScoreSystem.score.ToString();
    }

    public static void DisplayHighScore()
    {
        intHighScoreText.text = ScoreSystem.highScore.ToString();
    }

    public static void ResetScore()
    {
        ScoreSystem.score = 0;

        DisplayScore();
    }

    public static void ChangeScore(int points)
    {
        ScoreSystem.score += points;
        ScoreSystem.score += ScoreSystem.comboStreak;
        ScoreSystem.score = Mathf.Max(ScoreSystem.score, 0);

        DisplayScore();
        SyncScoreWithHighScore();
    }

    public static void SyncScoreWithHighScore()
    {
        ChangeHighScore(Math.Max(ScoreSystem.tempHighScore, ScoreSystem.score));
    }

    public static void SetInitialHighScore(int score)
    {
        ScoreSystem.tempHighScore = score;
        
        ChangeHighScore(score);
    }

    public static void ChangeHighScore(int score)
    {
        ScoreSystem.highScore = score;
        DisplayHighScore();
    }

    public static void ChangeCombo(bool isCombo)
    {
        ScoreSystem.comboStreak = isCombo ? ScoreSystem.comboStreak + 1 : 0;

        if (isCombo)
        {
            comboObject.transform.localScale = Vector3.zero;
            intComboText.text = ScoreSystem.comboStreak.ToString();
            animationCd = 3;
            comboObject.SetActive(true);
        }
    }

    public static void MapLane(int lane, out LaneIndex laneIndex)
    {
        if (lane == 0)
        {
            laneIndex = LaneIndex.Lane1;
        }
        else if (lane == 1)
        {
            laneIndex = LaneIndex.Lane2;
        }
        else if (lane == 2)
        {
            laneIndex = LaneIndex.Lane3;
        }
        else if (lane == 3)
        {
            laneIndex = LaneIndex.Lane4;
        }
        else if (lane == 4)
        {
            laneIndex = LaneIndex.Lane5;
        }
        else
        {
            laneIndex = LaneIndex.Lane6;
        }
    }
}
