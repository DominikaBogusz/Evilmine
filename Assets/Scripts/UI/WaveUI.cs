using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour {

	public WaveSpawner waveSpawner { get; set; }
	[SerializeField] Animator waveAnimator;
	[SerializeField] Text waveCountdownText;
	[SerializeField] Text waveCountText;

	private WaveSpawner.SpawnState previousState;

	void Update ()
    {
        if (waveSpawner)
        {
            switch (waveSpawner.State)
            {
                case WaveSpawner.SpawnState.COUNTING:
                    UpdateCountingUI();
                    break;
                case WaveSpawner.SpawnState.SPAWNING:
                    UpdateSpawningUI();
                    break;
            }
            previousState = waveSpawner.State;
        }     
	}

	void UpdateCountingUI()
    {
		if(previousState != WaveSpawner.SpawnState.COUNTING)
        {
            waveAnimator.SetBool("countdown", true);
            waveAnimator.SetBool("incoming", false);  
        }
        waveCountdownText.text = ((int)waveSpawner.WaveCountdown + 1).ToString();
    }

	void UpdateSpawningUI()
    {
		if(previousState != WaveSpawner.SpawnState.SPAWNING)
        {
            waveAnimator.SetBool("incoming", true);
            waveAnimator.SetBool("countdown", false);        
            waveCountText.text = waveSpawner.NextWave.ToString();
		}
	}
}
