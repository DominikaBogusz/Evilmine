using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject controlsUI;

    void Start()
    {
        Cursor.visible = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Play();
        }
    }

    public void Play()
    {
        LevelManager.Instance.LoadLevel(LevelManager.Instance.ActiveScene + 1);
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
        Application.Quit();
    }
}
