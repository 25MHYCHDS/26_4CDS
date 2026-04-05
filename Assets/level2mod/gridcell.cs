using UnityEngine;

public class GridCell : MonoBehaviour
{
    public bool isRevealed = false;
    private MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    public void Reveal()
    {
        if (isRevealed) return;
        isRevealed = true;
        meshRenderer.enabled = true;
    }
}