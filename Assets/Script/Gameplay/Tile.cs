using InTerra.EntitySDK;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Transform startTransform;
    public Transform targetTransform;
    public Transform clickTransform;
    public TileType tileType;
    public LaneIndex laneIndexEnum;
    public int laneIndex;
    public float holdDuration;
    public int holdIndex;
    public float speed;

    private void OnEnable()
    {
        if (!Static_Tile.isTilesLoaded)
        {
            return;
        }

        Static_Tile.MapLane(laneIndex, out laneIndexEnum);
    }

    private void Update()
    {
        if (!Static_Tile.isTilesLoaded)
        {
            return;
        }

        MoveToTarget();
        CheckPosition();
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetTransform.position,
            speed * Time.deltaTime
        );
    }

    public void CheckPosition()
    {
        if(CheckDistanceWithTarget())
        {
            Comp_Object.Mod_PoolingLIFO.Func_ReturnObject<Tile>(tileType, this);
            this.holdDuration = 0;
            this.gameObject.SetActive(false);

            Comp_Object.Mod_PoolingFIFO.Func_TryRemoveObjectFromQueue<Tile>(laneIndexEnum);
            if(!(tileType == TileType.Held && holdIndex == 2))
            {
                Static_Tile.ChangeScore(Static_Tile.Attributes.MISS_POINTS);
            }

            Static_Tile.ChangeCombo(false);
        }
    }

    public bool CheckDistanceWithTarget()
    {
        return Vector3.Distance(transform.position, targetTransform.position) < .5f;
    }
}
