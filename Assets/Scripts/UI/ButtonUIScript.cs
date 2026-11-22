using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUIScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public void SwitchScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        ResumeGame();
        SwitchScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
