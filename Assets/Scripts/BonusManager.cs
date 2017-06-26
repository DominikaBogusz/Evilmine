using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BonusManager : MonoBehaviour {

    private static BonusManager instance;
    public static BonusManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BonusManager>();
            }
            return instance;
        }
    }

    private BonusesUI bonusesUI;
    public void SetBonusesUI(BonusesUI bonusesUI)
    {
        this.bonusesUI = bonusesUI;
    }

    [SerializeField] public Bonus healthBonus;
    [SerializeField] public List<TimeBonus> timeBonuses;

    [SerializeField] private float minTimeBetweenGenerations;
    [SerializeField] private float maxTimeBetweenGenerations;
    private float timeBetweenGenerations;
    public float TimeBetweenGenerations
    {
        get { return timeBetweenGenerations; }
        set { timeBetweenGenerations = Mathf.Clamp(value, minTimeBetweenGenerations, maxTimeBetweenGenerations); }
    }

    public bool generated;

    void Start()
    {
        TimeBetweenGenerations = 10f;
    }

    public void CountDownToGenerate()
    {
        generated = true;
        StartCoroutine(GenerateCounting());
    }

    private IEnumerator GenerateCounting()
    {
        yield return new WaitForSeconds(timeBetweenGenerations);
        generated = false;
    }

    public void IncreaseDamage(int value, float time)
    {
        bonusesUI.SetDamageBonus(value, time);
        StartCoroutine(IncreaseDamageCounting(value, time));
    }

    private IEnumerator IncreaseDamageCounting(int value, float time)
    {
        int store = Player.Instance.Attributes.Damage.Get();
        Player.Instance.Attributes.Damage += value;
        yield return new WaitForSeconds(time);
        Player.Instance.Attributes.Damage.Set(store);
    }

    public void IncreaseAttackSpeed(int value, float time)
    {
        bonusesUI.SetAttackSpeedBonus(value, time);
        StartCoroutine(IncreaseAttackSpeedCounting(value, time));
    }

    private IEnumerator IncreaseAttackSpeedCounting(int value, float time)
    {
        int store = Player.Instance.Attributes.AttackSpeed.Get();
        Player.Instance.Attributes.AttackSpeed += value;
        yield return new WaitForSeconds(time);
        Player.Instance.Attributes.AttackSpeed.Set(store);
    }

    public void IncreaseShieldProtection(int value, float time)
    {
        bonusesUI.SetShieldProtectionBonus(value, time);
        StartCoroutine(IncreaseShieldProtectionCounting(value, time));
    }

    private IEnumerator IncreaseShieldProtectionCounting(int value, float time)
    {
        int store = Player.Instance.Attributes.ShieldProtection.Get();
        Player.Instance.Attributes.ShieldProtection += value;
        yield return new WaitForSeconds(time);
        Player.Instance.Attributes.ShieldProtection.Set(store);
    }
}
