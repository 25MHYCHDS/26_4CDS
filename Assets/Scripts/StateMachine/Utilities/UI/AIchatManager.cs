using UnityEngine;

public class AIchatManager : MonoBehaviour
{
    public static AIchatManager Instance;
    public GameObject Panel;
    public char[] AIP2;
    public char[] AIP3;

    private void Awake()
    {
        Instance = this;
    }
    public void SetAIP2()
    {
        Panel.GetComponent<DeepSeekAPI>().npcCharacter.personalityPrompt = AIP2.ToString();
    }
    public void SetAIP3()
    {
        Panel.GetComponent<DeepSeekAPI>().npcCharacter.personalityPrompt = AIP3.ToString();
    }
}
