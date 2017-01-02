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
    public bool ActiveUI { get; set; }
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject summaryUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && !ActiveUI)
        {
            ActiveAttributesUI = !ActiveAttributesUI;
        }
        if (Input.GetKeyDown(KeyCode.C) && !ActiveUI)
        {
            ActivateUI(upgradeUI);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !ActiveUI)
        {
            ActivateUI(pauseUI);
        }
    }

    public void ShowGameOverUI()
    {
        StartCoroutine(WaitForPlayerFall(2.0f));
    }

    private IEnumerator WaitForPlayerFall(float time)
    {
        yield return new WaitForSeconds(time);
        ActivateUI(gameOverUI);
    }

    public void ShowSummaryUI()
    {
        ActivateUI(summaryUI);
    }

    public void ActivateUI(GameObject ui)
    {
        ActiveUI = true;
        Time.timeScale = 0.0f;
        ui.SetActive(true);
    }
}
