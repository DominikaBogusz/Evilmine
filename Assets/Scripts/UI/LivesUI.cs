using UnityEngine;

public class LivesUI : MonoBehaviour {

	[SerializeField] private GameObject[] hearts;
    private int activeLivesCount;

    void Start()
    {
        activeLivesCount = hearts.Length;
        Player.Instance.DeadEvent += new DeadEventHandler(LostLife);   
    }

    public void LostLife()
    {
        if(activeLivesCount == 0)
        {
            UIManager.Instance.ShowGameOverUI();
            return;
        }
        Destroy(hearts[activeLivesCount - 1]);
        activeLivesCount--;
    }
}
