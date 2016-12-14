using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject controlsUI;

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ShowControlsUI()
    {
        controlsUI.SetActive(true);
    }

    public void CloseControlsUI()
    {
        controlsUI.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }
}
