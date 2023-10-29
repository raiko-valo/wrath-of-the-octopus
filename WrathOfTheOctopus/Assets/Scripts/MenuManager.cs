using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Menu();
    }

    public void Menu()
    {
        SceneManager.LoadScene("DemoMainMenu");
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("DemoScene");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
