using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUIScript : MonoBehaviour
{
    public void SwitchScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
