using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;
    public GameObject morteMenuUI;

    public GameObject winMenuUI;

    public AudioMixer audioMixer;
    public AudioSource source;
    public AudioClip winSom;
    public AudioClip deathSom;
    void Start()
    {
        morteMenuUI.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !HandlePlayerMorte.current.morreu && !ChaoBoss.endGame)
        {
            if (Controller.current.m_IsPaused)
            {
                Resume();
            }
            else
            {

                Pause();
            }
        }

        if (HandlePlayerMorte.current.morreu)
            MorteScreen();

        if (ChaoBoss.endGame)
            WinScreen();

    }

    public void Resume()
    {
        audioMixer.SetFloat("Volume", 0);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Controller.current.m_IsPaused = !Controller.current.m_IsPaused;

    }

    void Pause()
    {
        audioMixer.SetFloat("Volume", -10);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Controller.current.m_IsPaused = !Controller.current.m_IsPaused;

    }

    void MorteScreen()
    {
        morteMenuUI.SetActive(HandlePlayerMorte.current.morreu);
        source.Stop();
        source.PlayOneShot(deathSom);
        Time.timeScale = 0f;
    }

    void WinScreen()
    {
        winMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Controller.current.LockControl=true;
        Controller.current.m_IsPaused =true;
        source.Stop();
        source.loop= false;
        source.PlayOneShot(winSom);
    }
    public void Sair()
    {
        Application.Quit();
    }

    public void Delete()
    {
        PlayerPrefs.DeleteAll();
    }
    public void LoadMenu(string cena)
    {
        morteMenuUI.SetActive(false);
        HandlePlayerMorte.current.morreu = false;
        Controller.current.m_IsPaused = false;
        Controller.current.LockControl = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(cena);
    }




}
