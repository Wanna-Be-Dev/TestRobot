using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pause;
    [SerializeField]
    private GameObject options;
    [SerializeField]
    private GameObject help;
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void Esc(bool state)
    {
        pause.SetActive(state);
    }
    public void Pause(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
    }
    public void optionsmenu(bool state)
    {
        options.SetActive(state);
    }
    public void helpmenu(bool state)
    {
        help.SetActive(state);
    }

}
