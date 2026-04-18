using UnityEngine;

public class GridCell : MonoBehaviour
{
    public bool isRevealed = false;
    private GameObject fk;

    void Awake()
    {
        fk = transform.Find("fk").gameObject; fk.SetActive(false);
    }

    public void Reveal()
    {
        if (isRevealed) return;
        isRevealed = true;
        fk.SetActive(true);
    }
}