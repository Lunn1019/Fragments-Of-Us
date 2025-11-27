using InTerra.SceneAndUiSDK;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndPanel : MonoBehaviour
{
    private int checkScoreIndex = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject newHighScoreTxt;
    [SerializeField] private List<GameObject> jellyfish;

    private void OnEnable()
    {
        checkScoreIndex = Static_Tile.EndPanelAtt.checkScoreIndex;

        int score = Static_Tile.GetScore();

        if(!(score > Static_Tile.GetHighScore()))
        {
            highScoreText.text = "Score";
            highScoreText.color = Color.white;
            newHighScoreTxt.SetActive(true);
        }
        else
        {
            highScoreText.text = "New High Score";
            highScoreText.color = Color.yellow;
            newHighScoreTxt.SetActive(true);
        }

        scoreText.text = score.ToString();

        jellyfish[0].SetActive(false);
        jellyfish[1].SetActive(false);
        jellyfish[2].SetActive(false);

        if(checkScoreIndex <= 0)
        {
            return;
        }

        jellyfish[checkScoreIndex - 1].SetActive(true);
    }

    public void ReplayOnClick(string sceneName)
    {
        THISHUB.Instance.DestroyHub();
        Comp_BaseCanvas.Mod_Loading.Func_LoadLoadingScene(sceneName, "LoadingScreen");
    }

    public void BackOnClick()
    {
        THISHUB.Instance.DestroyHub();
        Comp_BaseCanvas.Mod_Loading.Func_LoadLoadingScene("HomeScene", "LoadingScreen");
    }
}
