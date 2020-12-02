using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    Scene current;

    public void Start()
    {
        AudioManager.instance.Play("Music");
    }

    public void StartLevel()
    {
        AudioManager.instance.Play("Ui");
        SceneManager.LoadScene("Level1");
    }

    public void Quit()
    {
        AudioManager.instance.Play("Ui");
        Application.Quit();
    }

    public void Restart()
    {
        AudioManager.instance.Play("Ui");
        current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }

    public void MainMenu()
    {
        AudioManager.instance.Play("Ui");
        Player.enemyDead = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        AudioManager.instance.Play("Ui");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadMenu(GameObject panel)
    {
        AudioManager.instance.Play("Ui");
        Player.enemyDead = false;
        panel.SetActive(true);    
    }
}
