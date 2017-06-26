using UnityEngine;
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

    public void Activate()
    {
        goldText.text = ScoreManager.Instance.Gold.Collected.ToString();
        
        if (ScoreManager.Instance.Blue.Active)
        {
            ShowGemLoot(blue, ScoreManager.Instance.Blue.Collected, ScoreManager.Instance.Blue.MaxValue);
        }
        if (ScoreManager.Instance.Green.Active)
        {
            ShowGemLoot(green, ScoreManager.Instance.Green.Collected, ScoreManager.Instance.Green.MaxValue);
        }
        if (ScoreManager.Instance.Red.Active)
        {
            ShowGemLoot(red, ScoreManager.Instance.Red.Collected, ScoreManager.Instance.Red.MaxValue);
        }
        if (ScoreManager.Instance.Yellow.Active)
        {
            ShowGemLoot(yellow, ScoreManager.Instance.Yellow.Collected, ScoreManager.Instance.Yellow.MaxValue);
        }

        for (int i = 0; i < Player.Instance.ActiveLivesCount; i++)
        {
            hearts[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }

        if (ScoreManager.Instance.Gold.Collected >= 100 && Player.Instance.ActiveLivesCount < Player.Instance.MaxLivesCount)
        {
            restoreLifeButton.GetComponent<Button>().interactable = true;
        }
    }

    void ShowGemLoot(GemUI gemUI, int collected, int max)
    {
        gemUI.text.transform.parent.gameObject.SetActive(true);
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
        if (ScoreManager.Instance.Gold.Collected <= 100 || Player.Instance.ActiveLivesCount >= Player.Instance.MaxLivesCount)
        {
            restoreLifeButton.GetComponent<Button>().interactable = false;
        }

        hearts[Player.Instance.ActiveLivesCount].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        livesUI.AddLife();
    }

    public void Retry()
    {
        UIManager.Instance.ActiveUI = false;
        Time.timeScale = 1.0f;
        LevelManager.Instance.LoadLevel(2);
        gameObject.SetActive(false);
    }

    public void LoadNextLevel()
    {
        UIManager.Instance.ActiveUI = false;
        Time.timeScale = 1.0f;
        if (!(LevelManager.Instance.ActiveScene == 4))
        {
            LevelManager.Instance.LoadLevel(LevelManager.Instance.ActiveScene + 1);
        }
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        blue.text.transform.parent.gameObject.SetActive(false);
        green.text.transform.parent.gameObject.SetActive(false);
        red.text.transform.parent.gameObject.SetActive(false);
        yellow.text.transform.parent.gameObject.SetActive(false);
    }
}