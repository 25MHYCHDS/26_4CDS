using UnityEngine;

public class TriggerReveal : MonoBehaviour
{
    public GameObject target;   // 拖拽要显现的物体

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target.SetActive(true);
        }
    }
}