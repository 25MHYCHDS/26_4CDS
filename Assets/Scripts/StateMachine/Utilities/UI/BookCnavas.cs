using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BookCnavas : MonoBehaviour
{
    public static BookCnavas instance;
    public GameObject Book1;
    public GameObject ExitButton;
    [field: Header("Setting")]
    public float PopGropTime = 0.14f;
    public float SwichTime = 0.5f;
    public Text DialougueText;
    [field: Header("Story")]
    public PlotClip[] plotClip;

    private PlotClip PlayingClip;
    private char[] TemporaryTextChars;
    private int TemporaryIndex = 0;
    private int StoryTextLenth;
    private int CompleteStoryTextLenth;


    private void Awake()
    {
        instance = this;
        Cursor.visible = false;
    }

    public void DisBook()
    {
        Book1.SetActive(false);
        ExitButton.SetActive(false);
    }

    public void SExitBotton()
    {
        ExitButton.SetActive(true);
    }

    public void PSFX()
    {
        SoundManager.instance.PlayESfx("Interact");
    }

    public void OpenBook(string name)
    {
        if (Book1.activeSelf == false)
        {
            Book1.SetActive(true);
            PlayingClip = Array.Find(plotClip, x => x.Name == name);
            if (PlayingClip == null)
            {
                Debug.Log("无法找到剧情");
            }
            else
            {
                SoundManager.instance.PlayESfx("Interact");
                DialougueText.text = null;
                StoryTextLenth = PlayingClip.StoryText.Length;
                TemporaryTextChars = PlayingClip.StoryText[CompleteStoryTextLenth].ToCharArray();

                InvokeRepeating("TextShow", 0, PopGropTime);
            }
        }
        else
        {
            return;
        }
    }
    public void TextShow()
    {
        if (TemporaryIndex >= TemporaryTextChars.Length)
        {
            SwichTime = 0.5f;
            TemporaryIndex = 0;
            CancelInvoke();
            Invoke("DecidePopNextText", SwichTime);
        }
        else
        {
            DialougueText.text += TemporaryTextChars[TemporaryIndex];
            TemporaryIndex++;
        }
    }
    public void DecidePopNextText()
    {
        if (StoryTextLenth - 1 > CompleteStoryTextLenth)
        {
            CompleteStoryTextLenth++;
            DialougueText.text = null;
            TemporaryTextChars = PlayingClip.StoryText[CompleteStoryTextLenth].ToCharArray();
            InvokeRepeating("TextShow", 0, PopGropTime);
        }
        else
        {
            StartCoroutine("EndPlot");
        }
    }
    IEnumerator EndPlot()
    {
        Cursor.visible = false;
        CompleteStoryTextLenth = 0;
        TemporaryIndex = 0;
        yield return new WaitForSeconds(SwichTime);
        Book1.SetActive(false);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
