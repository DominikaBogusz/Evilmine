using UnityEngine;

public class PlayerUseManager : MonoBehaviour {

    public IUseable Useable { get; set; }

    public void Use()
    {
        if (Useable != null)
        {
            Useable.Use();
        }
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Useable")
        {
            Useable = other.GetComponent<IUseable>();
        }
    }
}
