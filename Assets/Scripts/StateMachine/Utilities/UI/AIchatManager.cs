using UnityEngine;

public class AIchatManager : MonoBehaviour
{
    public static AIchatManager Instance;
    public GameObject Panel;
    public string AIP2;
    public string AIP3;

    private void Awake()
    {
        Instance = this;
    }
    public void SetAIP(string shu)
    {
        if(shu == "2")
        {
            Panel.GetComponent<DeepSeekAPI>().npcCharacter.personalityPrompt = AIP2;
            Panel.SetActive(true);
        }
        if (shu == "3")
        {
            Panel.GetComponent<DeepSeekAPI>().npcCharacter.personalityPrompt = AIP3;
            Panel.SetActive(true);
        }
    }

    public void closeAIC()
    {
       // Panel.SetActive(false);
    }
    public void openAIC()
    {
        Panel.SetActive(true);
    }
}
