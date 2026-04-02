using UnityEngine;

public class CellTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GridCell cell = GetComponent<GridCell>();
            if (cell != null) cell.Reveal();
        }
    }
}