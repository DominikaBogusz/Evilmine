using UnityEngine;
using UnityEngine.UI;

public class AttributesUI : MonoBehaviour {

    private bool activated;

    void Update()
    {
        if (activated != UIManager.Instance.ActiveAttributesUI)
        {
            ToggleUIActive();
        }
    }

    public void ToggleUIActive()
    {
        activated = !activated;
        transform.GetChild(0).gameObject.SetActive(activated);
        GetComponent<Image>().color = new Color(0f, 0f, 0f, activated ? 1f : 0f);
    }
}
