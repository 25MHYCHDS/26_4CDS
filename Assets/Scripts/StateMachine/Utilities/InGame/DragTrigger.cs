using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DragTrigger : MonoBehaviour
{
    public GameObject InteractionUI;
    public GameObject DragOGame;
    public GameObject PCamera;
    public GameObject DragGCamera;
    public GameObject DragGCButton;
    public string Shu;
    private bool IsInteract;

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (pnpc1.instance.IsGetBook1)
            {
                InteractionUI.SetActive(true);
                IsInteract = true;
                StartCoroutine(TS6());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractionUI.GetComponent<Image>().color = Color.white;
            InteractionUI.SetActive(false);
            IsInteract =false;
        }
    }
    private void Update()
    {
        RotateSkybox();
        if (IsInteract)
        {
            Cursor.visible = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                SoundManager.instance.PlayESfx("Interact");
                Cursor.visible = true;
                DragOGame.SetActive(true);
                InteractionUI.GetComponent<Image>().color = Color.gray;
                PCamera.SetActive(false);
                DragGCamera.SetActive(true);
                DragGCButton.SetActive(true);
                InteractionUI.SetActive(false);
                Player.instance.moveStateMachine.Player.Input.gamePlayActions.Disable();
            }
        }
    }

    private void RotateSkybox()
    {
        float num = RenderSettings.skybox.GetFloat("_Rotation");
        RenderSettings.skybox.SetFloat("_Rotation", num + 0.01f);
    }


    public void CloseDragG()
    {
        PCamera.SetActive(true);
        DragGCamera.SetActive(false);
        DragOGame.SetActive(false);
        Cursor.visible = false;
        DragGCButton.SetActive(true);
        InteractionUI.GetComponent<Image>().color = Color.white;
        Player.instance.moveStateMachine.Player.Input.gamePlayActions.Enable();

        pnpc1.instance.IsStudy = true;

        AIchatManager.Instance.SetAIP(Shu);
    }

    IEnumerator TS6()
    {
        yield return new WaitForSeconds(10);
        AIchat.instance.PlayPlot("TS6");
    }
}
