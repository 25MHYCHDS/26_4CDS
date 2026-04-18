using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AIchat : MonoBehaviour
{
    public static AIchat instance;
    [field: Header("Setting")]
    public float PopGropTime = 0.14f;
    public float SwichTime = 0.5f;
    public Text DialougueText;
    public GameObject AIS;
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
    }

    private void Start()
    {
        PlayPlot("Open");
        StartCoroutine("Tutor");
    }

    public void PlayPlot(string name)
    {
        AIS.GetComponent<Animator>().ResetTrigger("End");
        if (AIS.activeSelf == false)
        {
            AIS.SetActive(true);

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

    public void ShowAllText()
    {
        DialougueText.text = PlayingClip.StoryText[CompleteStoryTextLenth];
        TemporaryIndex = TemporaryTextChars.Length;
        SwichTime = 1;
    }
    public void ImageShow(char Name1, Image image)
    {
        if (TemporaryTextChars[2] == Name1)
        {
            image.enabled = true;
        }
        else
        {
            image.enabled = false;
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
        CompleteStoryTextLenth = 0;
        TemporaryIndex = 0;
        yield return new WaitForSeconds(SwichTime);
        AIS.GetComponent<Animator>().SetTrigger("End");
        AIS.SetActive(false);
    }

    IEnumerator Tutor()
    {
        yield return new WaitForSeconds(8);
        PlayerPlotsystem.instance.PlayPlot("Open");

        yield return new WaitForSeconds(5);
        PlayPlot("Tutorials");
    }
}
