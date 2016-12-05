using UnityEngine;
using System.Collections;

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

    public void IncreaseDamage(float value, float time)
    {
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
