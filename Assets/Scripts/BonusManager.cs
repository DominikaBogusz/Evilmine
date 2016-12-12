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

    [SerializeField] private BonusesUI bonusesUI;

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

    public void IncreaseDamage(float value, float time)
    {
        bonusesUI.SetDamageBonus(value, time);
        StartCoroutine(IncreaseDamageCounting(value, time));
    }

    private IEnumerator IncreaseDamageCounting(float value, float time)
    {
        float store = Player.Instance.Attributes.Damage;
        Player.Instance.Attributes.Damage += value;
        yield return new WaitForSeconds(time);
        Player.Instance.Attributes.Damage = store;
    }

    public void IncreaseAttackSpeed(float value, float time)
    {
        bonusesUI.SetAttackSpeedBonus(value, time);
        StartCoroutine(IncreaseAttackSpeedCounting(value, time));
    }

    private IEnumerator IncreaseAttackSpeedCounting(float value, float time)
    {
        float store = Player.Instance.Attributes.AttackSpeed;
        Player.Instance.Attributes.AttackSpeed += value;
        yield return new WaitForSeconds(time);
        Player.Instance.Attributes.AttackSpeed = store;
    }

    public void IncreaseShieldProtection(float value, float time)
    {
        bonusesUI.SetShieldProtectionBonus(value, time);
        StartCoroutine(IncreaseShieldProtectionCounting(value, time));
    }

    private IEnumerator IncreaseShieldProtectionCounting(float value, float time)
    {
        float store = Player.Instance.Attributes.ShieldProtectionPercent;
        Player.Instance.Attributes.ShieldProtectionPercent += value;
        yield return new WaitForSeconds(time);
        Player.Instance.Attributes.ShieldProtectionPercent = store;
    }
}
