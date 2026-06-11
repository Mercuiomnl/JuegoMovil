using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject ExitButton;

    private PlayerMovement playerMovement;
    public void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
    }
       
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
    }
    public void Salir()
    {
        SceneManager.LoadScene("Menu");
    }
    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Beach");         
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
