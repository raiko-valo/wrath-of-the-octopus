using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SlotMenu;
    public GameObject DifficultyMenu;
    public TextMeshProUGUI DifficultyText;
    public TextMeshProUGUI DifficultyExplanation;

    private string[] difficulties = {"JellyfiSh", "OctoPus", "SharK", "KrakEn"};
    private string[] explanations = {"For inexperienced jellyfishes new to the game.", "Normal difficulty for the average octopus.", "For those who are not afraid to get bitten.", "Only those who face the kraken can truly call themselves masters of the ocean."};
    private int difficulty = 0;

    private void Awake()
    {
        SlotMenu.SetActive(false);
        DifficultyMenu.SetActive(false);
    }
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
        SceneManager.LoadScene("Milestone2Map");
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void ActivateSlots()
    {
        MainMenu.SetActive(false);
        SlotMenu.SetActive(true);
    }

    public void ChooseDifficulty()
    {
        SlotMenu.SetActive(false);
        DifficultyMenu.SetActive(true);
    }

    public void BackToMain()
    {
        MainMenu.SetActive(true);
        SlotMenu.SetActive(false);
    }

    public void BackToSlot()
    {
        SlotMenu.SetActive(true);
        DifficultyMenu.SetActive(false);
    }

    public void NextDifficulty()
    {
        difficulty++;
        difficulty = Mathf.Clamp(difficulty, 0, 3);
        DifficultyText.text = difficulties[difficulty].ToString();
        DifficultyExplanation.text = explanations[difficulty].ToString();
    }

    public void PreviousDifficulty()
    {
        difficulty--;
        difficulty = Mathf.Clamp(difficulty, 0, 3);
        DifficultyText.text = difficulties[difficulty].ToString();
        DifficultyExplanation.text = explanations[difficulty].ToString();
    }
}
