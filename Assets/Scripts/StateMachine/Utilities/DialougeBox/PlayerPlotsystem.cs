using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class PlayerPlotsystem : MonoBehaviour
{
    public static PlayerPlotsystem instance;
    [field: Header("Setting")]
    public float PopGropTime = 0.14f;
    public float SwichTime = 0.5f;
    public Text DialougueText;
    public GameObject DialogueBoxBoutton;
    [field: Header("Story")]
    public PlotClip[] plotClip;

    private PlotClip PlayingClip;
    private char[] TemporaryTextChars;
    private int TemporaryIndex = 0;
    private int StoryTextLenth;
    private int CompleteStoryTextLenth;

    private void Awake()
    {
        CancelInvoke();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        CancelInvoke();
        CompleteStoryTextLenth = 0;

    }
    //播放剧情（单字）
    public void PlayPlot(string name)
    {
        if (DialogueBoxBoutton.activeSelf == false)
        {
            DialogueBoxBoutton.SetActive(true);
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
        Cursor.visible = false;
        CompleteStoryTextLenth = 0;
        TemporaryIndex = 0;
        yield return new WaitForSeconds(SwichTime);
        DialogueBoxBoutton.GetComponent<Animator>().SetTrigger("End");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetDielogeBox()
    {
        if (DialogueBoxBoutton.activeSelf)
        {
            DialogueBoxBoutton.SetActive(false);
        }
        else
        {
            DialogueBoxBoutton.SetActive(true);
        }
    }
}
