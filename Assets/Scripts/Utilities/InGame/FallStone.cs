using UnityEngine;

public class FallStone : MonoBehaviour
{
    public float FallTimeOffset;
    private Vector3 FirstPos;
    public GameObject Stone;
    private float Timer = 0f;
    private Animator FA;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FirstPos = transform.position;
        FA = GetComponent<Animator>();
        FA.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Stone.activeSelf)
        {
            return;
        }

        Timer += Time.deltaTime;
        if (Timer > FallTimeOffset)
        {
            Stone.SetActive(true);
            FA.enabled = true;

        }
    }

    public void BackP()
    {
        transform.position = FirstPos;
    }
}
