using InTerra.EntitySDK;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private int laneIndex;
    [SerializeField] private LaneIndex laneEnum;
    [SerializeField] private TilesController tilesController;
    private bool isHolding;
    public int holdIndex;
    private float holdTime;
    private bool isClicking;

    private void Awake()
    {
        isHolding = false;
        holdTime = 0;
    }

    private void Update()
    {
        if (isHolding)
        {
            holdTime += Time.deltaTime;
            TileOnHold();
        }
    }

    public void TileEventCheck()
    {
        if (!Comp_Object.Mod_PoolingFIFO.Func_TryGetObjectFromQueue<Tile>(laneEnum, out Tile tile))
        {
            Debug.Log("Pool Not found in " + laneEnum + " and " + laneIndex);
            tilesController.CalculateScore(Static_Tile.Attributes.MISS_POINTS, tile);
            ClickInfoController.Instance.EnableClickInfo(laneIndex, Static_Tile.Attributes.MISS_POINTS);
            return;
        }

        if(tile.tileType == TileType.Held && tile.holdIndex == 2)
        {
            tilesController.CalculateScore(Static_Tile.Attributes.MISS_POINTS, tile);
            ClickInfoController.Instance.EnableClickInfo(laneIndex, Static_Tile.Attributes.MISS_POINTS);
            return;
        }

        if(tile.tileType == TileType.Pressed)
        {
            TileOnClick(tile, laneEnum);
        }
        else
        {
            isHolding = TileOnClick(tile, laneEnum);
        }
    }

    public void SecondTileEventCheck()
    {
        if (!Comp_Object.Mod_PoolingFIFO.Func_TryGetObjectFromQueue<Tile>(laneEnum, out Tile tile))
        {
            Debug.Log("Pool Not found in " + laneEnum + " and " + laneIndex);
            isHolding = false;
            holdTime = 0;
            return;
        }

        if (isHolding)
        {
            if(!TileOnClick(tile, laneEnum))
            {
                tilesController.CalculateHoldScore();
            }

            isHolding = false;
            holdTime = 0;
        }
    }

    public void TileOnHold()
    {
        if (!Comp_Object.Mod_PoolingFIFO.Func_TryGetObjectFromQueue<Tile>(laneEnum, out Tile tile))
        {
            Debug.Log("Pool Not found in " + laneEnum + " and " + laneIndex);

            if (holdTime >= 1)
            {
                tilesController.CalculateHoldScore();

                holdTime = 0;
            }

            return;
        }

        float distance = Vector3.Distance(this.transform.position, tile.transform.position);

        if (distance > Static_Tile.Attributes.OKAY_DISTANCE && holdTime >= 1)
        {
            tilesController.CalculateHoldScore();

            holdTime = 0;

            return;
        }
    }

    public bool TileOnClick(Tile tile, LaneIndex laneEnum)
    {
        float distance = Vector3.Distance(this.transform.position, tile.transform.position);

        if (distance > Static_Tile.Attributes.OKAY_DISTANCE)
        {
            tilesController.CalculateScore(Static_Tile.Attributes.MISS_POINTS, tile);
            ClickInfoController.Instance.EnableClickInfo(laneIndex, Static_Tile.Attributes.MISS_POINTS);
            return false;
        }

        Comp_Object.Mod_PoolingFIFO.Func_TryRemoveObjectFromQueue<Tile>(laneEnum);

        if (distance < Static_Tile.Attributes.PERFECT_DISTANCE)
        {
            tilesController.CalculateScore(Static_Tile.Attributes.PERFECT_POINTS, tile);
            ClickInfoController.Instance.EnableClickInfo(laneIndex, Static_Tile.Attributes.PERFECT_POINTS);
        }
        else if (distance < Static_Tile.Attributes.GREAT_DISTANCE)
        {
            tilesController.CalculateScore(Static_Tile.Attributes.GREAT_POINTS, tile);
            ClickInfoController.Instance.EnableClickInfo(laneIndex, Static_Tile.Attributes.GREAT_POINTS);
        }
        else
        {
            tilesController.CalculateScore(Static_Tile.Attributes.OKAY_POINTS, tile);
            ClickInfoController.Instance.EnableClickInfo(laneIndex, Static_Tile.Attributes.OKAY_POINTS);
        }

        return true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isClicking)
        {
            return;
        }

        isClicking = true;
        TileEventCheck();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SecondTileEventCheck();

        isClicking = false;
    }
}
