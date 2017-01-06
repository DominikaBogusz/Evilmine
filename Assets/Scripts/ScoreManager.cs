using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Loot
{
    private Text lootText;
    public void SetLootText(Text text)
    {
        lootText = text;
    }

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

    private bool active;
    public bool Active
    {
        get { return active; }
        set
        {
            active = value;
            if (active)
            {
                Collected = 0;
                lootText.transform.parent.gameObject.SetActive(true);
            }
            else
            {
                Collected = 0;
                lootText.transform.parent.gameObject.SetActive(false);
            }
        }
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

    public void Init()
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
            gem.Active = true;
        }
        else
        {
            gem.Active = false;
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
