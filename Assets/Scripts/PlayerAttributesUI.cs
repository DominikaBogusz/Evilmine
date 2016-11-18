using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributesUI : MonoBehaviour {

    [SerializeField] private Text healthValue;
    [SerializeField] private Text damageValue;
    [SerializeField] private Text attackSpeedValue;
    [SerializeField] private Text shieldProtectionValue;

	void Update ()
    {
        if(transform.parent.localScale.x == -1)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        else
        {
            transform.localScale = new Vector2(1f, 1f);
        }

        healthValue.text = Player.Instance.attributes.Health.ToString();
        damageValue.text = Player.Instance.attributes.Damage.ToString();
        attackSpeedValue.text = Player.Instance.attributes.AttackSpeed.ToString();
        shieldProtectionValue.text = Player.Instance.attributes.ShieldProtectionPercent.ToString() + "%";

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (GetComponent<Canvas>().enabled)
            {
                GetComponent<Canvas>().enabled = false;
            }
            else
            {
                GetComponent<Canvas>().enabled = true;
            }
        }
    }
}
