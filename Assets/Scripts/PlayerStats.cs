using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    public int money;
    public int totalMoney;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        if(PlayerPrefs.HasKey("playerMoney"))
        {
            totalMoney = PlayerPrefs.GetInt("playerMoney");
        }
        else
        {
            totalMoney = 0;
        }
    }
    public void SavePlayerMoney()
    {

        totalMoney += money;
        PlayerPrefs.SetInt("playerMoney", totalMoney);
    }


}
