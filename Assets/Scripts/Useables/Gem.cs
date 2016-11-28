﻿using UnityEngine;

public class Gem : MonoBehaviour, IUseable {

    private Player player;

    [SerializeField] private BoxCollider2D leftTrigger;
    [SerializeField] private BoxCollider2D rightTrigger;

    void Start()
    {
        player = Player.Instance;
    }

    public void Use()
    {
        if (player.MyAnimator.GetFloat("speed") == 0f)
        {
            if (leftTrigger.IsTouching(player.GetComponent<Collider2D>()) && !player.FacingRight)
            {
                player.Flip();
            }
            else if (rightTrigger.IsTouching(player.GetComponent<Collider2D>()) && player.FacingRight)
            {
                player.Flip();
            }
            player.Digging = true;
            player.UseManager.Useable = null;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
