using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    [SerializeField]
    WaveSpawner spawner;

    [SerializeField]
    Animator waveAnimator;

    [SerializeField]
    Text waveCountdownText;

    [SerializeField]
    Text waveCountText;

    [SerializeField]
    string hoverOverSound = "ButtonHover";

    [SerializeField]
    string pressedButtonSound = "ButtonClick";

    AudioManager audioManager;

    private WaveSpawner.SpawnState previousState;


    // Start is called before the first frame update
    void Start()
    {
        if (spawner == null)
        {
            this.enabled = false;
        }

        if (waveAnimator == null)
        {
            this.enabled = false;
        }

        if (waveCountText == null)
        {
            this.enabled = false;
        }

        if (waveCountText == null)
        {
            this.enabled = false;
        }

        audioManager = AudioManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        switch (spawner.State)
        {
            case WaveSpawner.SpawnState.COUNTING:
                UpdateCountingUI();
                break;

            case WaveSpawner.SpawnState.SPAWNING:
                UpdateSpawningUI();
                break;


        }

        previousState = spawner.State;
    }

    void UpdateCountingUI()
    {
        if (previousState != WaveSpawner.SpawnState.COUNTING)
        {
            waveAnimator.SetBool("WaveCountdown", true);
            waveAnimator.SetBool("WaveIncoming", false);
        }

        waveCountdownText.text = ((int)spawner.WaveCountdown).ToString();
    }

    void UpdateSpawningUI()
    {
        if (previousState != WaveSpawner.SpawnState.SPAWNING)
        {
            waveAnimator.SetBool("WaveCountdown", false);
            waveAnimator.SetBool("WaveIncoming", true);

            waveCountText.text = spawner.NextWave.ToString();
        }
    }

    public void OnMouseOver()
    {
        audioManager.PlaySound(hoverOverSound);
    }

    public void OnMouseClick()
    {
        audioManager.PlaySound(pressedButtonSound);
    }
}
