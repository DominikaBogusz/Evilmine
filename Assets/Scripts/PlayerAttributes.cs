using UnityEngine;

public class PlayerAttributes : Attributes {

    private PlayerAttributesUI playerAttributesUI;

    void Start()
    {
        playerAttributesUI = attributesUI as PlayerAttributesUI;
    }

    [SerializeField] private int minShieldProtectionPercent;
    [SerializeField] private int maxShieldProtectionPercent;

    private int shieldProtectionPercent;
    public int ShieldProtectionPercent
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
        shieldProtectionPercent = (minShieldProtectionPercent + maxShieldProtectionPercent) / 2;
    }
}
