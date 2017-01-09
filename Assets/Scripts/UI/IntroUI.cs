using UnityEngine;

public class IntroUI : MonoBehaviour {

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Play();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Return();
        }
    }

    public void Play()
    {
        LevelManager.Instance.LoadLevel(LevelManager.Instance.ActiveScene + 1);
    }

    public void Return()
    {
        LevelManager.Instance.LoadLevel(0);
    }
}
