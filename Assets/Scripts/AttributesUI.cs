using UnityEngine;
using UnityEngine.UI;

public class AttributesUI : MonoBehaviour {

	[SerializeField] private Text healthValue;
    [SerializeField] private Text damageValue;
    [SerializeField] private Text attackSpeedValue;

    private bool isActive;

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
            isActive = !isActive;
            ActiveUI();
        }
    }

    private void ActiveUI()
    {
        transform.GetChild(0).gameObject.SetActive(isActive);
    }

    public void UpdateHealth(float value)
    {
        healthValue.text = value.ToString();
    }

    public void UpdateDamage(float value)
    {
        damageValue.text = value.ToString();
    }

    public void UpdateAttackSpeed(float value)
    {
        attackSpeedValue.text = value.ToString();
    }
}
