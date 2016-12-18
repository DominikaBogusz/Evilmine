using UnityEngine;
using UnityEngine.UI;

public class AttributeButtonUI : MonoBehaviour {

	void Update()
    {
        if(Player.Instance.Attributes.LearningPoints > 0)
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
