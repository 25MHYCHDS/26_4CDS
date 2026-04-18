using UnityEngine;

public class P2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pnpc2.instance.IsStudy = true;
        }
    }
}
