using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISettings : MonoBehaviour {

    [SerializeField] private Text goldText, blueText, greenText, redText, yellowText;
    [SerializeField] private BonusesUI bonusesUI;
    
    void Start()
    {
        SetUISettings(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        SceneManager.sceneLoaded += SetUISettings;
    }

    void SetUISettings(Scene scene, LoadSceneMode mode)
    {
        ScoreManager.Instance.Gold.SetLootText(goldText);
        ScoreManager.Instance.Blue.SetLootText(blueText);
        ScoreManager.Instance.Green.SetLootText(greenText);
        ScoreManager.Instance.Red.SetLootText(redText);
        ScoreManager.Instance.Yellow.SetLootText(yellowText);

        ScoreManager.Instance.Init();

        BonusManager.Instance.SetBonusesUI(bonusesUI);
    }
}
