using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
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

    public void QuitGame ()
    {
        Application.Quit();
    }

    public void StartGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
