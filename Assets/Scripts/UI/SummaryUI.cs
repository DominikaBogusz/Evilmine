using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class GemUI
{
    [SerializeField] public Text text;
    [SerializeField] public Text addedExperience;
}

public class SummaryUI : MonoBehaviour
{
    [SerializeField] private Text goldText;
    [SerializeField] private GemUI blue, green, red, yellow;

    [SerializeField] private LivesUI livesUI;
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private Button restoreLifeButton;

    void Start()
    {
        goldText.text = ScoreManager.Instance.Gold.Collected.ToString();

        ShowGemLoot(blue, ScoreManager.Instance.Blue.Collected, ScoreManager.Instance.Blue.MaxValue);
        ShowGemLoot(green, ScoreManager.Instance.Green.Collected, ScoreManager.Instance.Green.MaxValue);
        ShowGemLoot(red, ScoreManager.Instance.Red.Collected, ScoreManager.Instance.Red.MaxValue);
        ShowGemLoot(yellow, ScoreManager.Instance.Yellow.Collected, ScoreManager.Instance.Yellow.MaxValue);

        for (int i = 0; i < livesUI.ActiveLivesCount; i++)
        {
            hearts[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }

        if (ScoreManager.Instance.Gold.Collected >= 100 && livesUI.ActiveLivesCount < livesUI.MaxLivesCount)
        {
            restoreLifeButton.GetComponent<Button>().interactable = true;
        }
    }

    void ShowGemLoot(GemUI gemUI, int collected, int max)
    {
        gemUI.text.text = collected.ToString() + "/" + max.ToString();

        float fraction = collected / (float)max;
        float experienceToGain;
        if (fraction < 1)
        {
            experienceToGain = fraction * 100;
        }
        else
        {
            experienceToGain = fraction * 200;
            gemUI.addedExperience.GetComponent<Text>().color = Color.green;
        }

        int exp = (int)experienceToGain;
        gemUI.addedExperience.text = "+" + exp.ToString() + "exp!";
        Player.Instance.Attributes.AddExperience(exp);
    }

    public void RestoreLife()
    {
        ScoreManager.Instance.Gold.Collected -= 100;
        goldText.text = ScoreManager.Instance.Gold.Collected.ToString();
        if (ScoreManager.Instance.Gold.Collected <= 100 || livesUI.ActiveLivesCount >= livesUI.MaxLivesCount)
        {
            restoreLifeButton.GetComponent<Button>().interactable = false;
        }

        hearts[livesUI.ActiveLivesCount].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        livesUI.AddLife();
    }

    public void Retry()
    {
        UIManager.Instance.ActiveUI = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        UIManager.Instance.ActiveUI = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}