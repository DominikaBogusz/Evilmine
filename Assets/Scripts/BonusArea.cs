using UnityEngine;
using System.Collections;

public class BonusArea : MonoBehaviour {

    private BonusManager bonusManager;

    private BoxCollider2D collider;
    private Bonus generatedBonus;
    private float timeBetweenEnable = 5f;
    private float timeBetweenTrigger = 2.5f;
    private bool triggered;

    void Start()
    {
        bonusManager = BonusManager.Instance;
        collider = GetComponent<BoxCollider2D>();
    }

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" &&  !triggered && !bonusManager.generated)
        {
            CountDownToTrigger();

            if (GenerateHealthBonus() || GenerateTimeBonus())
            {
                bonusManager.CountDownToGenerate();
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
            generatedBonus = Instantiate(bonusManager.healthBonus, randomPosition, Quaternion.identity) as Bonus;
            generatedBonus.SetBonusArea(this);

            return true;
        }

        return false;
    }

    private bool GenerateTimeBonus()
    {
        int chance = 100 - DifficultyManager.Instance.PlayerLevel;
        int draw = Random.Range(0, 100);

        if (draw < chance)
        {
            int random = Random.Range(0, bonusManager.timeBonuses.Count);
            Vector2 randomPosition = new Vector2(Random.Range(collider.bounds.min.x, collider.bounds.max.x), Random.Range(collider.bounds.min.y, collider.bounds.max.y));
            generatedBonus = Instantiate(bonusManager.timeBonuses[random], randomPosition, Quaternion.identity) as Bonus;
            generatedBonus.SetBonusArea(this);

            return true;
        }

        return false;
    }

    public void CountDownToEnable()
    {
        StartCoroutine(EnableCounting());
    }

    private IEnumerator EnableCounting()
    {
        yield return new WaitForSeconds(timeBetweenEnable);
        collider.enabled = true;
    }

    private void CountDownToTrigger()
    {
        triggered = true;
        StartCoroutine(TriggerCounting());
    }

    private IEnumerator TriggerCounting()
    {
        yield return new WaitForSeconds(timeBetweenTrigger);
        triggered = false;
    }
}
