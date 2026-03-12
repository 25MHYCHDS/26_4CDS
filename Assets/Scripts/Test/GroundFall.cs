using UnityEngine;

public class GroundFall : MonoBehaviour
{
    private GameObject ground;
    private void Awake()
    {
        ground = GetComponent<GameObject>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetP = transform.position;
        targetP.y += Time.deltaTime * 10f;
        transform.position = targetP;
    }
}
