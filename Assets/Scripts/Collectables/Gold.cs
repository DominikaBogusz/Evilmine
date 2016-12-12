using UnityEngine;

public class Gold : MonoBehaviour, ICollectable {

    [SerializeField] private int value;

    public void Collect()
    {
        ScoreManager.Instance.Gold += value;
        Destroy(gameObject);
    }
}
