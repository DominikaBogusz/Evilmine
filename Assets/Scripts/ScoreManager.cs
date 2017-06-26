using UnityEngine;

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

    [SerializeField] private LootUI lootUI;

    private int gold;
    public int Gold
    {
        get { return gold; }
        set
        {
            gold = value;
            lootUI.SetGoldValue(gold);
        }
    }

    private int blue;
    public int Blue
    {
        get { return blue; }
        set
        {
            blue = value;
            lootUI.SetBlueValue(blue);
        }
    }

    private int green;
    public int Green
    {
        get { return green; }
        set
        {
            green = value;
            lootUI.SetGreenValue(green);
        }
    }

    private int red;
    public int Red
    {
        get { return red; }
        set
        {
            red = value;
            lootUI.SetRedValue(red);
        }
    }

    private int yellow;
    public int Yellow
    {
        get { return yellow; }
        set
        {
            yellow = value;
            lootUI.SetYellowValue(yellow);
        }
    }

    void Start()
    {
        Gold = 0;
        Blue = 0;
        Green = 0;
        Red = 0;
        Yellow = 0;
    }

	public void AddGems(string name)
    {
        switch (name)
        {
            case "BlueGemRift(Clone)":
                Blue++;
                break;
            case "GreenGemRift(Clone)":
                Green++;
                break;
            case "RedGemRift(Clone)":
                Red++;
                break;
            case "YellowGemRift(Clone)":
                Yellow++;
                break;
        }
    }           
}
