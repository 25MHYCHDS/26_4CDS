using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public static bool IsOpen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsOpen)
        {
            if (other.CompareTag("Player"))
            {
                gameObject.GetComponent<Animator>().SetTrigger("Open");
            }
        }
        else
        {
            PlayerPlotsystem.instance.PlayPlot("Door");
            StartCoroutine("Tutor");
        }
    }
    IEnumerator Tutor()
    {
        yield return new WaitForSeconds(5);
        AIchat.instance.PlayPlot("TS2");
    }
}
