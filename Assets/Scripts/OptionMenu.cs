using UnityEngine;
using UnityEngine.UI;
public class OptionMenu : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    private void Start()
    {
        if(!PlayerPrefs.HasKey("volumeValue"))
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = PlayerPrefs.GetFloat("volumeValue");
        }
        volumeSlider.value = AudioListener.volume;
    }
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        SaveVolumeSetting();
    }
    void SaveVolumeSetting()
    {
        PlayerPrefs.SetFloat("volumeValue", AudioListener.volume);
    }
}
