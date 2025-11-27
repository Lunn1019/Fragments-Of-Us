using InTerra.HUB;
using System.Collections.Generic;
using UnityEngine;

public class ClickInfoController : MonoBehaviour
{
    public static ClickInfoController Instance;

    [SerializeField] private float animationTime = 2;
    [SerializeField] private List<ClickInfo> missObjects;
    [SerializeField] private List<ClickInfo> okayObjects;
    [SerializeField] private List<ClickInfo> greatObjects;
    [SerializeField] private List<ClickInfo> perfectObjects;

    void Awake()
    {
        InterraHUB.Func_Singleton<ClickInfoController>(ref Instance, this);
    }

    public void EnableClickInfo(int laneIndex, int points)
    {
        ClickInfo infoObject;

        if(points == Static_Tile.Attributes.MISS_POINTS)
        {
            infoObject = missObjects[laneIndex];
            okayObjects[laneIndex].gameObject.SetActive(false); 
            greatObjects[laneIndex].gameObject.SetActive(false); 
            perfectObjects[laneIndex].gameObject.SetActive(false); 
        }
        else if (points == Static_Tile.Attributes.OKAY_POINTS)
        {
            infoObject = okayObjects[laneIndex];
            missObjects[laneIndex].gameObject.SetActive(false);
            greatObjects[laneIndex].gameObject.SetActive(false);
            perfectObjects[laneIndex].gameObject.SetActive(false);
        }
        else if (points == Static_Tile.Attributes.GREAT_POINTS)
        {
            infoObject = greatObjects[laneIndex];
            missObjects[laneIndex].gameObject.SetActive(false);
            okayObjects[laneIndex].gameObject.SetActive(false);
            perfectObjects[laneIndex].gameObject.SetActive(false);
        }
        else
        {
            infoObject = perfectObjects[laneIndex];
            missObjects[laneIndex].gameObject.SetActive(false);
            okayObjects[laneIndex].gameObject.SetActive(false);
            greatObjects[laneIndex].gameObject.SetActive(false);
        }

        infoObject.transform.localScale = Vector3.zero;
        infoObject.animationTime = animationTime;
        infoObject.gameObject.SetActive(true);
    }
}
