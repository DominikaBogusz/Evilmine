using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIOverlay : MonoBehaviour {

    [SerializeField] private Text goldText, blueText, greenText, redText, yellowText;

    void Start()
    {
        LevelManager.Instance.GameplayPersistentObjects.Add(gameObject);

        ScoreManager.Instance.Gold.SetLootText(goldText);
        ScoreManager.Instance.Blue.SetLootText(blueText);
        ScoreManager.Instance.Green.SetLootText(greenText);
        ScoreManager.Instance.Red.SetLootText(redText);
        ScoreManager.Instance.Yellow.SetLootText(yellowText);

        ScoreManager.Instance.Init();

        SceneManager.sceneLoaded += SetUISettings;
    }

    void SetUISettings(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex > 2)
        {
            ScoreManager.Instance.Gold.SetLootText(goldText);
            ScoreManager.Instance.Blue.SetLootText(blueText);
            ScoreManager.Instance.Green.SetLootText(greenText);
            ScoreManager.Instance.Red.SetLootText(redText);
            ScoreManager.Instance.Yellow.SetLootText(yellowText);

            ScoreManager.Instance.Init();
        }
    }

}
