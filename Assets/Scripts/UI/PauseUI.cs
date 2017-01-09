using UnityEngine;

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
        LevelManager.Instance.LoadLevel(0);
    }

    public void Resume()
    {
        UIManager.Instance.ActiveUI = false;
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
}
