using UnityEngine;

public class HealthIndicatorUI : MonoBehaviour {

    [SerializeField] private RectTransform helthBar;

    public void SetHealth(float current, float max)
    {
        float value = current / max;
        helthBar.localScale = new Vector3(value, helthBar.localScale.y, helthBar.localScale.z);
    }
}
