using UnityEngine;
using MarUtility.ExecutionManagement;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : Manager
{
    public bool IsGamePaused => Time.timeScale == 0;

    [SerializeField] private GameObject pauseMenu;
    private InputAction pause;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Initialize()
    {
        pause = InputSystem.actions.FindAction("Pause");
        //pause.started += Pause_started;
        base.Initialize();

        pauseMenu.SetActive(false);
    }

    private void Pause_started(InputAction.CallbackContext obj)
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            return;
        }

        if (IsGamePaused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            return;
        }

        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    private void OnDestroy()
    {
        pause.started -= Pause_started;
    }
}
