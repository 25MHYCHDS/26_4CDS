using UnityEngine;
using UnityEngine.UI;

public class PipGameT : MonoBehaviour
{
    public static PipGameT instance;
    public GameObject InteractionE;
    public GameObject InteractionQ;
    public GameObject PCamera;
    public GameObject PipGCamera;
    public GameObject DragGCButton;

    private Animator AnimatorP;
    public bool IsInteract;

    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AnimatorP = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInteract)
        {
            Cursor.visible = true;
            InteractionE.GetComponent<Image>().color = Color.white;
            InteractionQ.GetComponent<Image>().color = Color.white;
            if (Input.GetKeyDown(KeyCode.E))
            {
                SoundManager.instance.PlayESfx("Interact");
                AnimatorP.SetBool("IsForword", true);
                AnimatorP.SetBool("IsBackword", false);

                ScreenChange();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SoundManager.instance.PlayESfx("Interact");
                AnimatorP.SetBool("IsForword", false);
                AnimatorP.SetBool("IsBackword", true);

                ScreenChange();
            }
        }
    }

    private void ScreenChange()
    {
        PCamera.SetActive(false);
        PipGCamera.SetActive(true);

        DragGCButton.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (pnpc1.instance.IsStudy)
            {
                IsInteract = true;
                InteractionE.SetActive(true);
                InteractionQ.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsInteract = false;
            InteractionE.SetActive(false);
            InteractionQ.SetActive(false);
        }
    }

    public void ClosePipGCamera()
    {
        PipGCamera.SetActive(false);
    }
}
