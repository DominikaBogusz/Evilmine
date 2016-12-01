using UnityEngine;

public class PlayerAttributes : Attributes {

    private PlayerAttributesUI playerAttributesUI;

    void Awake()
    {
        playerAttributesUI = attributesUI as PlayerAttributesUI;
    }

    [SerializeField] private float minShieldProtectionPercent;
    [SerializeField] private float maxShieldProtectionPercent;

    private float shieldProtectionPercent;
    public float ShieldProtectionPercent
    {
        get { return shieldProtectionPercent; }
        set
        {
            shieldProtectionPercent = Mathf.Clamp(value, minShieldProtectionPercent, maxShieldProtectionPercent);
            playerAttributesUI.UpdateShieldProtection(shieldProtectionPercent);
        }
    }

    public override void Init()
    {
        base.Init();
        ShieldProtectionPercent = (minShieldProtectionPercent + maxShieldProtectionPercent) / 2;
    }
}
