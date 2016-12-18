using UnityEngine;
using UnityEngine.UI;

public delegate void ChangeEventHandler();

[System.Serializable]
public class Attribute {

    private int current;
    [SerializeField] public int min;
    [SerializeField] public int max;
    [SerializeField] private Text uiText;
    [Header("Optional: ")]
    [SerializeField] private StatusIndicatorUI statusIndicator;

    public int Get()
    {
        return current;
    }

    public void Set(int value)
    {
        current = Mathf.Clamp(value, min, max);
        uiText.text = current.ToString();
        if (statusIndicator != null)
        {
            statusIndicator.SetStatusBar(current, max);
        }
        OnChangeEvent();
    }

    public event ChangeEventHandler ChangeEvent;

    public void OnChangeEvent()
    {
        if (ChangeEvent != null)
        {
            ChangeEvent();
        }
    }

    public static Attribute operator +(Attribute instance, int value)
    {
        instance.Set(instance.current + value);
        return instance;
    }
    public static Attribute operator +(int value, Attribute instance)
    {
        instance.Set(instance.current + value);
        return instance;
    }

    public static Attribute operator -(Attribute instance, int value)
    {
        instance.Set(instance.current - value);
        return instance;
    }
    public static Attribute operator -(int value, Attribute instance)
    {
        instance.Set(instance.current - value);
        return instance;
    }
}
