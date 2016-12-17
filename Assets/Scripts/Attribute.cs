using UnityEngine;
using UnityEngine.UI;

public abstract class Attribute<T> {

    protected string name;
    public T min { get; set; }
    public T max { get; set; }
    protected T current;
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
        return current;
    }

    public abstract void Set(T value);
}

public class AttributeInt : Attribute<int> {

    public AttributeInt(string name, int min, int max, Text uiText) : base(name, min, max, uiText) { }
    public AttributeInt(string name, int min, int max, Text uiText, StatusIndicatorUI statusIndicator) : base(name, min, max, uiText, statusIndicator) { }

    public override void Set(int value)
    {
        current = Mathf.Clamp(value, min, max);
        uiText.text = current.ToString();
        if (statusIndicator != null)
        {
            statusIndicator.SetStatusBar(current, max);
        }
    }

    public static implicit operator int(AttributeInt instance)
    {
        return instance.current;
    }

    public static AttributeInt operator +(AttributeInt instance, int value)
    {
        instance.Set(instance.current + value);
        return instance;
    }

    public static AttributeInt operator +(int value, AttributeInt instance)
    {
        instance.Set(instance.current + value);
        return instance;
    }

    public static AttributeInt operator -(AttributeInt instance, int value)
    {
        instance.Set(instance.current - value);
        return instance;
    }

    public static AttributeInt operator -(int value, AttributeInt instance)
    {
        instance.Set(instance.current - value);
        return instance;
    }
}

public class AttributeFloat : Attribute<float> {

    public AttributeFloat(string name, float min, float max, Text uiText) : base(name, min, max, uiText) { }

    public override void Set(float value)
    {
        current = Mathf.Clamp(value, min, max);
        uiText.text = current.ToString();
    }

    public static implicit operator float(AttributeFloat instance)
    {
        return instance.current;
    }

    public static AttributeFloat operator +(AttributeFloat instance, float value)
    {
        instance.Set(instance.current + value);
        return instance;
    }

    public static AttributeFloat operator +(float value, AttributeFloat instance)
    {
        instance.Set(instance.current + value);
        return instance;
    }

    public static AttributeFloat operator -(AttributeFloat instance, float value)
    {
        instance.Set(instance.current - value);
        return instance;
    }

    public static AttributeFloat operator -(float value, AttributeFloat instance)
    {
        instance.Set(instance.current - value);
        return instance;
    }
}
