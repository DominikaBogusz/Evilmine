using UnityEngine;

public class Gold : MonoBehaviour, ICollectable {

    [SerializeField] private int value;

    public void Collect()
    {
        ScoreManager.Gold += value;
        Destroy(gameObject);
    }
}
