using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BonusesUI : MonoBehaviour {

	[SerializeField] private Text damageBonusValue;
    [SerializeField] private Text damageBonusTimeText;
	
	public void SetDamageBonus(float value, float time)
    {
        damageBonusValue.text = "+" + value.ToString();
        StartCoroutine(DamageBonusCounting(value, time));
    }

    private IEnumerator DamageBonusCounting(float value, float time)
    {
        damageBonusValue.transform.parent.gameObject.SetActive(true);
        while(time > 0f)
        { 
            damageBonusTimeText.text = (time / 60 - time / 60 % 1).ToString() + " : " + (time * 10 - time * 10 % 1).ToString();
            time -= Time.deltaTime;
            yield return null;
        }
        damageBonusValue.transform.parent.gameObject.SetActive(false);
    }

    [SerializeField] private Text attackSpeedBonusValue;
    [SerializeField] private Text attackSpeedBonusTimeText;
	
	public void SetAttackSpeedBonus(float value, float time)
    {
        attackSpeedBonusValue.text = "+" + value.ToString();
        StartCoroutine(AttackSpeedBonusCounting(value, time));
    }

    private IEnumerator AttackSpeedBonusCounting(float value, float time)
    {
        attackSpeedBonusValue.transform.parent.gameObject.SetActive(true);
        while(time > 0f)
        {
            attackSpeedBonusTimeText.text = (time / 60 - time / 60 % 1).ToString() + " : " + (time * 10 - time * 10 % 1).ToString();
            time -= Time.deltaTime;
            yield return null;
        }
        attackSpeedBonusValue.transform.parent.gameObject.SetActive(false);
    }

    [SerializeField] private Text shieldProtectionBonusValue;
    [SerializeField] private Text shieldProtectionBonusTimeText;
	
	public void SetShieldProtectionBonus(float value, float time)
    {
        shieldProtectionBonusValue.text = "+" + value.ToString() + "%";
        StartCoroutine(ShieldProtectionBonusCounting(value, time));
    }

    private IEnumerator ShieldProtectionBonusCounting(float value, float time)
    {
        shieldProtectionBonusValue.transform.parent.gameObject.SetActive(true);
        while(time > 0f)
        { 
            shieldProtectionBonusTimeText.text = (time / 60 - time / 60 % 1).ToString() + " : " + (time * 10 - time * 10 % 1).ToString();
            time -= Time.deltaTime;
            yield return null;
        }
        shieldProtectionBonusValue.transform.parent.gameObject.SetActive(false);
    }
}
