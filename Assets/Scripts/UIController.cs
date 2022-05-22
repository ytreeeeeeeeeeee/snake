using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject _gameUI;
    [SerializeField] GameObject _pauseUI;
    public void pause()
    {
        _gameUI.SetActive(false);
        _pauseUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void play_again()
    {
        SceneManager.LoadScene(1);
    }
    public void main_menu()
    {
        SceneManager.LoadScene(0);
    }
    public void vozob()
    {
        _gameUI.SetActive(true);
        _pauseUI.SetActive(false);
        Time.timeScale = 1;
    }
}
