using UnityEngine;

public abstract class Bonus : MonoBehaviour, ICollectable {

    [SerializeField] protected float bonusValue;

    public abstract void Collect();

    protected BonusArea bonusArea;

    public void SetBonusArea(BonusArea bonusArea)
    {
        this.bonusArea = bonusArea;
    }

    void OnDestroy()
    {
        if(bonusArea != null)
        {
            bonusArea.CountDownToEnable();
        } 
    }
}
