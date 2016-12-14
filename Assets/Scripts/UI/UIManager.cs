using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    public bool ActiveAttributesUI { private set; get; }
    [SerializeField] private PauseUI pauseUI;
    [SerializeField] private GameOverUI gameOverUI;
    private bool activeGameOverUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ActiveAttributesUI = !ActiveAttributesUI;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !activeGameOverUI)
        {
            if (pauseUI.gameObject.activeSelf)
            {
                pauseUI.Resume();
            }
            else
            {
                pauseUI.Activate();
            }
        }
    }

    public void ShowGameOverUI()
    {
        StartCoroutine(WaitForPlayerFall(2.0f));
        activeGameOverUI = true;
    }

    private IEnumerator WaitForPlayerFall(float time)
    {
        yield return new WaitForSeconds(time);
        gameOverUI.Activate();
    }
}
