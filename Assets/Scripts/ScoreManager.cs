using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public static int blue;
    public static int green;
    public static int red;
    public static int yellow;

	public static void AddGems(string name)
    {
        switch (name)
        {
            case "BlueGemRift(Clone)":
                blue++;
                break;
            case "GreenGemRift(Clone)":
                green++;
                break;
            case "RedGemRift(Clone)":
                red++;
                break;
            case "YellowGemRift(Clone)":
                yellow++;
                break;
        }
    }

    public static int Gold { get; set; }
}
