using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BonusAreasManager : MonoBehaviour {

    private static BonusAreasManager instance;
    public static BonusAreasManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BonusAreasManager>();
            }
            return instance;
        }
    }

    [SerializeField] public Bonus healthBonus;
    [SerializeField] public List<TimeBonus> timeBonuses;

    private float timeBetweenGenerations = 10f;
    public bool generated;

    public void CountDownToGenerate()
    {
        generated = true;
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        yield return new WaitForSeconds(timeBetweenGenerations);
        generated = false;
    }
}
