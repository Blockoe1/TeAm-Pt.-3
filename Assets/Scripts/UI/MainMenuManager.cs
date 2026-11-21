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
