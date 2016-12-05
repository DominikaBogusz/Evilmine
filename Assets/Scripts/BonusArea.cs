using UnityEngine;
using System.Collections;

public class BonusArea : MonoBehaviour {

    private BonusAreasManager areasManager;

    private BoxCollider2D collider;
    private Bonus generatedBonus;
    private float timeBetweenEnable = 5f;
    private float timeBetweenTrigger = 2.5f;
    private bool triggered;

    void Start()
    {
        areasManager = BonusAreasManager.Instance;
        collider = GetComponent<BoxCollider2D>();
    }

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" &&  !triggered && !areasManager.generated)
        {
            CountDownToTrigger();

            if (GenerateHealthBonus() || GenerateTimeBonus())
            {
                areasManager.CountDownToGenerate();
                collider.enabled = false;
            }
        }
    }

    private bool GenerateHealthBonus()
    {
        float chance = 100f - Player.Instance.Attributes.Health;
        float draw = Random.Range(0f, 100f);

        if (draw < chance)
        {
            Vector2 randomPosition = new Vector2(Random.Range(collider.bounds.min.x, collider.bounds.max.x), Random.Range(collider.bounds.min.y, collider.bounds.max.y));
            generatedBonus = Instantiate(areasManager.healthBonus, randomPosition, Quaternion.identity) as Bonus;
            generatedBonus.SetBonusArea(this);

            return true;
        }

        return false;
    }

    private bool GenerateTimeBonus()
    {
        float chance = 50f; // TODO: function to estimate player skill and generating bonuses adequate
        float draw = Random.Range(0f, 100f);

        if (draw < chance)
        {
            int random = Random.Range(0, areasManager.timeBonuses.Count);
            Vector2 randomPosition = new Vector2(Random.Range(collider.bounds.min.x, collider.bounds.max.x), Random.Range(collider.bounds.min.y, collider.bounds.max.y));
            generatedBonus = Instantiate(areasManager.timeBonuses[random], randomPosition, Quaternion.identity) as Bonus;
            generatedBonus.SetBonusArea(this);

            return true;
        }

        return false;
    }

    public void CountDownToEnable()
    {
        StartCoroutine(Enable());
    }

    private IEnumerator Enable()
    {
        yield return new WaitForSeconds(timeBetweenEnable);
        collider.enabled = true;
    }

    private void CountDownToTrigger()
    {
        triggered = true;
        StartCoroutine(Trigger());
    }

    private IEnumerator Trigger()
    {
        yield return new WaitForSeconds(timeBetweenTrigger);
        triggered = false;
    }
}
