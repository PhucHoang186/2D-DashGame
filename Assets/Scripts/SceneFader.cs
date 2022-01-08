using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneFader : MonoBehaviour
{
    Animator ani;
    [SerializeField] float waitTime = 1f;
    void Start()
    {
        ani = GetComponent<Animator>();
    }
    IEnumerator FadeOut(string _sceneName)
    {
        ani.SetTrigger("Fading");
        yield return new WaitForSecondsRealtime(waitTime);
        SceneManager.LoadScene(_sceneName);
    }
    public void FadeToScene(string _sceneName)
    {
        StartCoroutine(FadeOut(_sceneName));

    }
}
