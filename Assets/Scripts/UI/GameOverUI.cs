using UnityEngine;

public class GameOverUI : MonoBehaviour {

    public void BackToMenu()
    {
        Time.timeScale = 1.0f;
        LevelManager.Instance.LoadLevel(0);
    }

    public void Retry()
    {
        UIManager.Instance.ActiveUI = false;
        Time.timeScale = 1.0f;
        LevelManager.Instance.LoadLevel(2);
    }
}
