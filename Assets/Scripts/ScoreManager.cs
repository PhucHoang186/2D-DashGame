using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    [SerializeField] TMP_Text scoreText; // display score text
    [SerializeField] TMP_Text bestScoreText;// display best score text
    [SerializeField] TMP_Text coinText;// display coin test
    [SerializeField] TMP_Text finalScoreText;// display final score test

    [SerializeField] float scoreDisplaySpeed=.3f;

    // score
    [HideInInspector] public int score = 0;
    [HideInInspector] public int bestScore;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
   
    void Start()
    {
        bestScore = PlayerPrefs.GetInt("bestScore", 0);
        scoreText.text = "0";
        bestScoreText.text = "Best score: " + bestScore.ToString(); 
    }
    public void UpdateScoreUI()
    {
        score += 1;        
        scoreText.text = score.ToString();
    }
    public void UpdateCoinUI()
    {
        coinText.text = PlayerStats.Instance.money.ToString();
    }
    public void SaveBestScore()
    {
        if (bestScore < score)
        {
            bestScore = score;
            PlayerPrefs.SetInt("bestScore", bestScore);
        }
    }
    public IEnumerator DisplayScore()
    {
        yield return new WaitForSecondsRealtime(.2f);// wait for menu animation finish
        int i = 0;
        while (i <= score)
        {
            finalScoreText.text = i.ToString();
            yield return new WaitForSecondsRealtime(scoreDisplaySpeed);
            i++;
        }
    }
}
