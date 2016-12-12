using UnityEngine;

public class LivesUI : MonoBehaviour {

	[SerializeField] private GameObject[] hearts;
    private int activeLife;

    void Start()
    {
        activeLife = hearts.Length - 1;
        Player.Instance.DeadEvent += new DeadEventHandler(LostLife);   
    }

    public void LostLife()
    {
        if(activeLife < 0)
        {
            //TODO: nie dopuścić do tej sytuacji, ma się pokazać GameOver
            return;
        }
        Destroy(hearts[activeLife]);
        activeLife--;
    }
}
