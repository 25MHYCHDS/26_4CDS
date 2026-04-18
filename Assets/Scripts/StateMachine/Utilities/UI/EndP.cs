using UnityEngine;

public class EndP : MonoBehaviour
{
    public void EndPolt()
    {
        gameObject.GetComponent<Animator>().ResetTrigger("End");
        gameObject.SetActive (false);
    }
}
