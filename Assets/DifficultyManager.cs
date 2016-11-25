using UnityEngine;

public class DifficultyManager : MonoBehaviour {

    /*
	public float BattleTimer { get; set; }
    public bool IsBattling { get; set; }

    private float betweenActionsTimer = 2f;
    private float betweenActionsGap = 2f;

    void Update()
    {
        IsBattling = Player.Instance.IsBattling();
        if (IsBattling)
        {
            BattleTimer += Time.deltaTime;
            betweenActionsTimer = 0f;
        }
        else
        {
            betweenActionsTimer += Time.deltaTime;
            if(betweenActionsTimer < betweenActionsGap)
            {
                BattleTimer += Time.deltaTime;
            }
        }

        //Debug.Log("BattleTime:" + BattleTimer + ", betweenActionsTimer:" + betweenActionsTimer);
    }
    */

    private static DifficultyManager instance;
    public static DifficultyManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DifficultyManager>();
            }
            return instance;
        }
    }
}
