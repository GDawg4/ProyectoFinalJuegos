using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    AudioManager audioManager;

    [SerializeField]
    string hoverOverSound = "ButtonHover";

    [SerializeField]
    string pressedButtonSound = "ButtonClick";

    private void Start()
    {
        audioManager = AudioManager.instance;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
