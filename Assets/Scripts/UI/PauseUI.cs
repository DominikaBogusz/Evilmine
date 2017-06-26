using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour {

    public void Activate()
    {
        Time.timeScale = 0.0f;
        gameObject.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
}
