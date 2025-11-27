using InTerra.EntitySDK;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Transform startTransform;
    public Transform targetTransform;
    public int laneIndex;
    public LaneIndex laneEnum;
    private bool isSecondTileFound;

    private void OnEnable()
    {
    }

    private void Update()
    {
        CheckSecondTile();
        if (IsTargetReached())
        {
            this.gameObject.SetActive(false);
            return;
        }
        MoveToTarget();
    }

    public void RotateArrow()
    {
        Vector3 startPos = Camera.main.WorldToScreenPoint(this.transform.position);
        Vector3 targetPos = Camera.main.WorldToScreenPoint(this.targetTransform.position);

        Vector3 direction = (targetPos - startPos).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }


    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetTransform.position,
            10 * Time.deltaTime
        );
    }

    private void CheckSecondTile()
    {
        if (isSecondTileFound)
            return;

        Tile tile;

        if(!Comp_Object.Mod_PoolingFIFO.Func_TryGetObjectFromQueue<Tile>(laneEnum, out tile) || tile.holdIndex != 2)
        {
            if(!Comp_Object.Mod_PoolingFIFO.Func_TryGetLastObjectFromQueue<Tile>(laneEnum, out tile) || tile.holdIndex != 2)
            {
                return;
            }
        }

        targetTransform = tile.transform;
    }

    private bool IsTargetReached()
    {
        return Vector3.Distance(this.transform.position, targetTransform.position) <= 5f || this.transform.position.y >= targetTransform.position.y;
    }
}
