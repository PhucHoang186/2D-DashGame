using System.Collections;
using System;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] SpriteRenderer backgroundImage;
    //Game Menu
    [SerializeField] GameObject pauseMenu; //Pause Menu
    [SerializeField] GameObject gameOverMenu;//Game Lost Menu
    [SerializeField] GameObject optionMenu;//option Menu
    [SerializeField] GameObject playOneMoreButton;
    [SerializeField] GameObject startScreen; // display start text
    [SerializeField] TMP_Text countdownText;
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
        Time.timeScale = 1;
        Color newColor = new Color(UnityEngine.Random.Range(10, 250f) / 255, UnityEngine.Random.Range(10, 250f) / 255, UnityEngine.Random.Range(100, 250f) / 255);// pick random color for background

        // game manager in the menu scene dont need these game object
        if (backgroundImage == null || startScreen == null || countdownText == null)
        {
            return;
        }
        backgroundImage.material.DOColor(newColor, 0.5f);
        countdownText.gameObject.SetActive(false);
        startScreen.SetActive(true);
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
        if(Input.GetKeyDown(KeyCode.A))
        {
            if(startScreen!=null)
            {
                startScreen.SetActive(false);
            }
            else
            {
                return;
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
        isPause = false;
        pauseMenu.SetActive(isPause);
        StartCoroutine(CountdownCo());
    }
    IEnumerator CountdownCo()
    {

        countdownText.gameObject.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }
        countdownText.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    // Lost Game
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