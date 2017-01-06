using UnityEngine;

public class LivesUI : MonoBehaviour {

    void Start()
    {
        Player.Instance.MaxLivesCount = Player.Instance.ActiveLivesCount = transform.childCount;
        Player.Instance.DeadEvent += new DeadEventHandler(LostLife);   
    }

    public void LostLife()
    {
        if(Player.Instance.ActiveLivesCount == 0)
        {
            UIManager.Instance.ShowGameOverUI();
            return;
        }
        Player.Instance.ActiveLivesCount--;
        transform.GetChild(Player.Instance.ActiveLivesCount).gameObject.SetActive(false);
    }

    public void AddLife()
    {
        transform.GetChild(Player.Instance.ActiveLivesCount).gameObject.SetActive(true);
        Player.Instance.ActiveLivesCount++;
    }
}
