/*****************************************************************************
// File Name : MoneyManager.cs
// Author : Alex Lindsey
// Creation Date : 11/21/2026
// Last Modified : 11/21/2026
//
// Brief Description : Implements the main menu functionality, including starting the game, toggling credits, and quitting the application.
*****************************************************************************/
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private int mainGameSceneIndex = 1;
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGameScene");
    }
    public void ToggleCredits()
    {
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(!creditsPanel.activeSelf);
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
}
