using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenEvents : MonoBehaviour
{
    public void LoadCasino()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
