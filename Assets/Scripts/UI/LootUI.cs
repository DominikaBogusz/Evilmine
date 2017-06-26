using UnityEngine;
using UnityEngine.UI;

public class LootUI : MonoBehaviour {

	[SerializeField] private Text goldValue;
	
	public void SetGoldValue(int value)
    {
        goldValue.text = value.ToString();
    }

    [SerializeField] private Text blueValue;
	
	public void SetBlueValue(int value)
    {
        blueValue.text = value.ToString();
    }

    [SerializeField] private Text greenValue;
	
	public void SetGreenValue(int value)
    {
        greenValue.text = value.ToString();
    }

    [SerializeField] private Text redValue;
	
	public void SetRedValue(int value)
    {
        redValue.text = value.ToString();
    }

    [SerializeField] private Text yellowValue;
	
	public void SetYellowValue(int value)
    {
        yellowValue.text = value.ToString();
    }
}
