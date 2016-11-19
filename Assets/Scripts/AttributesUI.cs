using UnityEngine;
using UnityEngine.UI;

public class AttributesUI : MonoBehaviour {

	[SerializeField] private Text healthValue;
    [SerializeField] private Text damageValue;
    [SerializeField] private Text attackSpeedValue;

    void Update()
    {
        if (transform.parent.localScale.x == -1)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        else
        {
            transform.localScale = new Vector2(1f, 1f);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (transform.GetChild(0).gameObject.activeSelf)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    public void UpdateHealth(int value)
    {
        healthValue.text = value.ToString();
    }

    public void UpdateDamage(int value)
    {
        damageValue.text = value.ToString();
    }

    public void UpdateAttackSpeed(float value)
    {
        attackSpeedValue.text = value.ToString();
    }
}
