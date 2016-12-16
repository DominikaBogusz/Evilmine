using UnityEngine;
using UnityEngine.UI;

public abstract class Attribute<T> {

    protected string name;
    protected T min;
    protected T max;
    protected T value;
    protected Text uiText;
    protected StatusIndicatorUI statusIndicator;

    public Attribute(string name, T min, T max, Text uiText)
    {
        this.name = name;
        this.min = min;
        this.max = max;
        this.uiText = uiText;
    }

    public Attribute(string name, T min, T max, Text uiText, StatusIndicatorUI statusIndicator)
    {
        this.name = name;
        this.min = min;
        this.max = max;
        this.uiText = uiText;
        this.statusIndicator = statusIndicator;
    }

    public T Get()
    {
        return value;
    }

    public abstract void Set(T value);

    public T Max()
    {
        return max;
    }
}

public class AttributeInt : Attribute<int> {

    public AttributeInt(string name, int min, int max, Text uiText) : base(name, min, max, uiText) { }
    public AttributeInt(string name, int min, int max, Text uiText, StatusIndicatorUI statusIndicator) : base(name, min, max, uiText, statusIndicator) { }

    public override void Set(int value)
    {
        this.value = Mathf.Clamp(value, min, max);
        uiText.text = value.ToString();
        if (statusIndicator != null)
        {
            statusIndicator.SetStatusBar(value, max);
        }
    }

    public static implicit operator int(AttributeInt instance)
    {
        return instance.value;
    }
}

public class AttributeFloat : Attribute<float> {

    public AttributeFloat(string name, float min, float max, Text uiText) : base(name, min, max, uiText) { }

    public override void Set(float value)
    {
        this.value = Mathf.Clamp(value, min, max);
        uiText.text = value.ToString();
    }

    public static implicit operator float(AttributeFloat instance)
    {
        return instance.value;
    }
}
