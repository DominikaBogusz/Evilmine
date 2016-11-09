using UnityEngine;

public abstract class AnimationManager : MonoBehaviour {

    protected Animator animator;

    public virtual void Start()
    {
        animator = GetComponent<Animator>();
    }
}
