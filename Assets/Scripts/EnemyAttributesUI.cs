using UnityEngine;
using UnityEngine.UI;

public class EnemyAttributesUI : MonoBehaviour {

    private Enemy enemy;

	[SerializeField] private Text healthValue;
    [SerializeField] private Text damageValue;
    [SerializeField] private Text attackSpeedValue;
    [SerializeField] private Text attackIntervalValue;

    void Start()
    {
        enemy = transform.parent.GetComponent<Enemy>();
    }

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

        healthValue.text = enemy.attributes.Health.ToString();
        damageValue.text = enemy.attributes.Damage.ToString();
        attackSpeedValue.text = enemy.attributes.AttackSpeed.ToString();
        attackIntervalValue.text = enemy.attributes.AttackInterval.ToString();

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
