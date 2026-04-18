using System.Collections;
using UnityEngine;

public class pnpc2 : MonoBehaviour
{
    public static pnpc2 instance;
    public bool IsGetBook2 = false;
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
                PlayerPlotsystem.instance.PlayPlot("S4");
                StartCoroutine(TS9());
            }
            else
            {
                PlayerPlotsystem.instance.PlayPlot("S3");

                IsGetBook2 = true;

                StartCoroutine(OpenBook());
            }

            IEnumerator OpenBook()
            {
                yield return new WaitForSeconds(3);
                Cursor.visible = true;
                BookCnavas.instance.SExitBotton();
                BookCnavas.instance.OpenBook("B2");

                yield return new WaitForSeconds(5);
                AIchat.instance.PlayPlot("TS8");
            }

            IEnumerator TS9()
            {
                yield return new WaitForSeconds(4);
                AIchat.instance.PlayPlot("TS9");
            }
        }
    }
}
