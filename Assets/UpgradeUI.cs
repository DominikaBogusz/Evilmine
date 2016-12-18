using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour {

    [SerializeField] private Text levelText;
    [SerializeField] private Text learningPointsText;

    [SerializeField] private Text healthText;
    [SerializeField] private Text damageText;
    [SerializeField] private Text attackSpeedText;
    [SerializeField] private Text shieldProtectionText;

	void Start()
    {
        UpdateLevel();
        UpdateHealthPoints();
        UpdateAttributes();

        Player.Instance.Attributes.Level.ChangeEvent += new ChangeEventHandler(UpdateLevel);
        Player.Instance.Attributes.Damage.ChangeEvent += new ChangeEventHandler(UpdateAttributes);
        Player.Instance.Attributes.AttackSpeed.ChangeEvent += new ChangeEventHandler(UpdateAttributes);
        Player.Instance.Attributes.ShieldProtection.ChangeEvent += new ChangeEventHandler(UpdateAttributes);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    public void Close()
    {
        UIManager.Instance.ActiveUI = false;
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    void UpdateLevel()
    {
        levelText.text = Player.Instance.Attributes.Level.Get().ToString();
        UpdateLearningPoints();
    }

    void UpdateLearningPoints()
    {
        learningPointsText.text = Player.Instance.Attributes.LearningPoints.ToString();
        if(Player.Instance.Attributes.LearningPoints > 0)
        {
            learningPointsText.GetComponentInParent<Image>().color = Color.green;
        }
        else
        {
            learningPointsText.GetComponentInParent<Image>().color = Color.red;
        }
    }

    void UpdateHealthPoints()
    {
        healthText.text = Player.Instance.Attributes.Health.max.ToString();
    }

    void UpdateAttributes()
    {    
        damageText.text = Player.Instance.Attributes.Damage.Get().ToString();
        attackSpeedText.text = Player.Instance.Attributes.AttackSpeed.Get().ToString();
        shieldProtectionText.text = Player.Instance.Attributes.ShieldProtection.Get().ToString() + "%";
    }

    public void AddHealthPoints()
    {
        Player.Instance.Attributes.Health.max += 10;
        UpdateHealthPoints();
        Player.Instance.Attributes.LearningPoints--;
        UpdateLearningPoints(); 
    }

    public void AddDamage()
    {
        Player.Instance.Attributes.Damage += 5;
        Player.Instance.Attributes.LearningPoints--;
        UpdateLearningPoints();
    }

    public void AddAttackSpeed()
    {
        Player.Instance.Attributes.AttackSpeed += 2;
        Player.Instance.Attributes.LearningPoints--;
        UpdateLearningPoints();
    }

    public void AddShieldProtection()
    {
        Player.Instance.Attributes.ShieldProtection += 5;
        Player.Instance.Attributes.LearningPoints--;
        UpdateLearningPoints();
    }
}
