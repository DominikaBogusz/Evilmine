using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Loot
{
    [SerializeField] private Text lootText;

    [SerializeField] public int MaxValue;

    private int collected;
    public int Collected
    {
        get { return collected; }
        set
        {
            collected = Mathf.Clamp(value, 0, MaxValue);
            lootText.text = collected.ToString();
        }
    }

    public void InitGem()
    {
        Collected = 0;
        lootText.transform.parent.gameObject.SetActive(true);
    }
}

public class ScoreManager : MonoBehaviour {

    private static ScoreManager instance;
    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScoreManager>();
            }
            return instance;
        }
    }

    public Loot Gold, Blue, Green, Red, Yellow;

    void Start()
    {
        Gold.Collected = 0;

        CheckIfInit(Blue);
        CheckIfInit(Green);
        CheckIfInit(Red);
        CheckIfInit(Yellow);
    }

    private void CheckIfInit(Loot gem)
    {
        if(gem.MaxValue > 0)
        {
            gem.InitGem();
        }
    }

	public void AddGems(string name)
    {
        switch (name)
        {
            case "BlueGemRift(Clone)":
                Blue.Collected++;
                break;
            case "GreenGemRift(Clone)":
                Green.Collected++;
                break;
            case "RedGemRift(Clone)":
                Red.Collected++;
                break;
            case "YellowGemRift(Clone)":
                Yellow.Collected++;
                break;
        }
    }           
}
