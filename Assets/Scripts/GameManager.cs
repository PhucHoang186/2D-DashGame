using System;
using UnityEngine;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] SpriteRenderer backgroundImage;
    //Game Menu
    [SerializeField] GameObject pauseMenu; //Pause Menu
    [SerializeField] GameObject gameOverMenu;//Game Lost Menu
    [SerializeField] GameObject optionMenu;//option Menu
    [SerializeField] GameObject playOneMoreButton; 
    public event Action UpdateBlockspawner;
    SceneFader sceneFader;
    //Game State
    bool canPlayOneMore =true;
    //bool isLost;
    bool isPause;
    bool isChange = false;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    private void Start()
    {
        sceneFader = FindObjectOfType<SceneFader>();
        //isLost = false;
        Time.timeScale = 1;
        Color newColor = new Color(UnityEngine.Random.Range(10, 250f) / 255, UnityEngine.Random.Range(10, 250f) / 255, UnityEngine.Random.Range(100, 250f) / 255);// pick random color for background
        if(backgroundImage==null)
        {
            return;
        }
        backgroundImage.material.DOColor(newColor, 0.5f);
    }
    void Update()
    {
        if (backgroundImage == null)
            return;
        UpdateGameState();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPause)
            {
                Pausegame();
            }
            else
            {
                ContinueGame();
            }
        }
    }
    // Pause Game
    public void Pausegame()
    {
        Time.timeScale = 0;
        isPause = true;
        pauseMenu.SetActive(isPause);
    }
    //Continue Game
    public void ContinueGame()
    {
        Time.timeScale = 1;
        isPause = false;
        pauseMenu.SetActive(isPause);

    }// Lost Game
    public void LostGame()
    {
        gameOverMenu.SetActive(true);
        if(canPlayOneMore)
        {
            playOneMoreButton.SetActive(true);
        }
        else
        {
            playOneMoreButton.SetActive(false);
        }
        //isLost = true;
        StartCoroutine(ScoreManager.Instance.DisplayScore());
        Time.timeScale = 0;
    }
    //quit game
    public void QuitGame()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
    // open Option Menu
    public void OpenMenu()
    {
        optionMenu.gameObject.SetActive(true);
    }
    public void CloseMenu()
    {
        optionMenu.gameObject.SetActive(false);

    }
    private void UpdateGameState()
    {
        if ( ScoreManager.Instance.score!=0 && ScoreManager.Instance.score % 5 == 0 && !isChange)// change color every 5 points
        {
            UpdateBlockspawner?.Invoke();
            isChange = true;
            Color newColor = new Color(UnityEngine.Random.Range(100, 250f) / 255, UnityEngine.Random.Range(100, 250f) / 255, UnityEngine.Random.Range(100, 250f) / 255);// pick random color for background
            backgroundImage.material.DOColor(newColor,0.5f);
        }
        else if (ScoreManager.Instance.score % 5 != 0)
        {
            isChange = false;
        }
    }
    public void ReplayGame()
    {
        //sceneFader.FadeToScene(SceneManager.GetActiveScene().name);
        sceneFader.FadeToScene("Level");

    }
    public void ContinueAfterLost() //watch ad to continue play one more live;
    {
        canPlayOneMore = false;
        ContinueGame();
        gameOverMenu.SetActive(false);
    }
    public void LoadScene(string _sceneName)
    {
        sceneFader.FadeToScene(_sceneName);
    }

}