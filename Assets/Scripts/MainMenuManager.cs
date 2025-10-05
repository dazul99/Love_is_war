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

    public void StartGame()
    {
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

}
