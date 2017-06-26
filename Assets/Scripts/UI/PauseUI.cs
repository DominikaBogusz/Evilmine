using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour {

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        UIManager.Instance.ActiveUI = false;
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
}
