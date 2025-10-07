using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject howToPlayPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private AudioClip[] songs;
    [SerializeField] private AudioSource audioS;
    [SerializeField] private Slider volume;
    private int currentSong = 0;

    private void Update()
    {
        if(audioS.time >= songs[currentSong].length)
        {
            currentSong++;
            if(currentSong >= songs.Length) currentSong = 0;
            audioS.clip = songs[currentSong];
            audioS.Play();
        }
    }

    public void StartGame()
    {
        PlayerPrefs.SetFloat("volume", volume.value);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);

    }

    public void HowToPlay()
    {
        howToPlayPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Credits()
    {
        creditsPanel.SetActive(true);
        menuPanel.SetActive(false);

    }

    public void BackToMenu()
    {
        creditsPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
        menuPanel.SetActive(true);

    }

    public void ChangeVolume(float vol)
    {
        audioS.volume = vol;
    }

}
