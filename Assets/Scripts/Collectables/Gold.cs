using UnityEngine;

public class Gold : MonoBehaviour, ICollectable {

    [SerializeField] private int value;

    public void Collect()
    {
        ScoreManager.Instance.Gold.Collected += value;
        Destroy(gameObject);
    }
}
