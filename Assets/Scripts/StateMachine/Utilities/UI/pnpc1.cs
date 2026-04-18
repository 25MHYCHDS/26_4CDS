using System.Collections;
using UnityEngine;

public class pnpc1 : MonoBehaviour
{
    public static pnpc1 instance;
    public bool IsGetBook1 = false;
    public bool IsStudy = false;

    private void Awake()
    {
        instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (IsStudy)
            {
                PlayerPlotsystem.instance.PlayPlot("S2");
                StartCoroutine(TS7());
            }
            else
            {
                PlayerPlotsystem.instance.PlayPlot("S1");

                IsGetBook1 = true;

                StartCoroutine(OpenBook());
            }
        }


        IEnumerator OpenBook()
        {
            yield return new WaitForSeconds(3);
            Cursor.visible = true;
            BookCnavas.instance.SExitBotton();
            BookCnavas.instance.OpenBook("B1");

            yield return new WaitForSeconds(5);
            AIchat.instance.PlayPlot("TS5");
        }
        IEnumerator TS7()
        {
            yield return new WaitForSeconds(4);
            AIchat.instance.PlayPlot("TS7");
        }
    }
}
