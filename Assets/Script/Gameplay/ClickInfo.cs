using UnityEngine;
using UnityEngine.UIElements;

public class ClickInfo : MonoBehaviour
{
    public Vector3 scale;
    public float growSpeed = 20f;
    public float animationTime = 0;

    private void Awake()
    {
        scale = transform.localScale;
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(animationTime <= 0)
        {
            this.gameObject.SetActive(false);
            return;
        }

        animationTime -= Time.deltaTime;
        transform.localScale = Vector3.MoveTowards(
            transform.localScale,
            scale,
            growSpeed * Time.deltaTime
        );
    }
}
