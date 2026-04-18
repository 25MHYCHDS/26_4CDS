using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Slider MusicSlider;
    public Slider SfxSlider;
    public static bool IsGamePaused = false;
    public GameObject PauesUI;
    public GameObject PauesMain;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SoundManager.instance.PlayESfx("Interact");
            PlayerPlotsystem.instance.SetDielogeBox();
            if (IsGamePaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        SoundManager.instance.PlayESfx("Interact");
        PauesUI.SetActive(true); PauesMain.SetActive(false);
        Time.timeScale = 0f;
        IsGamePaused = true;

        Cursor.visible = true;
    }
    public void Resume()
    {
        PauesUI.SetActive(false); PauesMain.SetActive(true);
        Time.timeScale = 1;
        IsGamePaused = false;

        Cursor.visible = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void ToggleMusic()
    {
        SoundManager.instance.ToggleMusic();
        SoundManager.instance.PlayESfx("Interact");
    }
    public void ToggleSfx()
    {
        SoundManager.instance.ToggleSfx();
        SoundManager.instance.PlayESfx("Interact");
    }
    public void MusicVolume()
    {
        SoundManager.instance.MusicVolume(MusicSlider.value);
    }
    public void SfxVolume()
    {
        SoundManager.instance.SfxVolume(SfxSlider.value);
    }

    public void UIEffect()
    {
        SoundManager.instance.PlaySfx("UI");
    }
}
