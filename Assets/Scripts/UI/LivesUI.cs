using UnityEngine;

public class LivesUI : MonoBehaviour {

	[SerializeField] private GameObject[] hearts;
    public int MaxLivesCount { get; set; }
    public int ActiveLivesCount { get; set; }

    void Start()
    {
        MaxLivesCount = ActiveLivesCount = hearts.Length;
        Player.Instance.DeadEvent += new DeadEventHandler(LostLife);   
    }

    public void LostLife()
    {
        if(ActiveLivesCount == 0)
        {
            UIManager.Instance.ShowGameOverUI();
            return;
        }
        hearts[ActiveLivesCount - 1].SetActive(false);
        ActiveLivesCount--;
    }

    public void AddLife()
    {
        hearts[ActiveLivesCount].SetActive(true);
        ActiveLivesCount++;
    }
}
