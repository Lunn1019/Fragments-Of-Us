using UnityEngine;

public class ComboAnimation : MonoBehaviour
{
    public Vector3 scale;
    private bool isInitialized = false;
    public float growSpeed = 20f;

    public void InitScale()
    {
        scale = transform.localScale;
        isInitialized = true;
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isInitialized)
        {
            return;
        }

        Static_Tile.DecreaseAnimationCd();
        
        transform.localScale = Vector3.MoveTowards(
            transform.localScale,
            scale,
            growSpeed * Time.deltaTime
        );
    }
}
