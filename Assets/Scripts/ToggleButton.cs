using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    public GameObject inActiveSprite;
    private void Start()
    {
        if(AudioListener.volume == 0)
        {
            inActiveSprite.SetActive(true);
            GetComponent<Button>().interactable = false;
        }
        else
        {
            inActiveSprite.SetActive(false);
            GetComponent<Button>().interactable = true;

        }

    }
    public void Toggle(string _name)
    {
        inActiveSprite.SetActive(SoundManager.Instance.ToggleMusic(_name));
    }
}
