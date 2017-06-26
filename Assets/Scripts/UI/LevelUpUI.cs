using UnityEngine;

public class LevelUpUI : MonoBehaviour {

	[SerializeField] private GameObject arrowSignUI;

    void Update()
    {
        if(Player.Instance.Attributes.LearningPoints > 0)
        {
            arrowSignUI.SetActive(true);
        }
        else
        {
            arrowSignUI.SetActive(false);
        }
    }

}
