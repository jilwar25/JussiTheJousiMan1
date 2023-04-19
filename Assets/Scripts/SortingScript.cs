using UnityEngine;

public class SortingScript : MonoBehaviour
{
    [SerializeField] private int offset = 0;
    private SpriteRenderer spriteRenderer;
    private int sortingOrderBase = 5000;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        spriteRenderer.sortingOrder = (int)(sortingOrderBase - transform.position.y - offset);
    }
}
