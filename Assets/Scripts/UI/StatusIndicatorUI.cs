using UnityEngine;

public class StatusIndicatorUI : MonoBehaviour {

    [SerializeField] private RectTransform statusBar;

    public void SetStatusBar(int current, int max)
    {
        float value = (float)current / max;
        statusBar.localScale = new Vector3(value, statusBar.localScale.y, statusBar.localScale.z);
    }
}
