using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public void LoadPlayScene()
    {
        SceneManager.LoadScene("Play");
    }

    public void ApplicationExit()
    {
        Application.Quit();
    }
}
