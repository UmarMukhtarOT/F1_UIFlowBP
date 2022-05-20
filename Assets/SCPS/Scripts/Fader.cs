using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Fader : MonoBehaviour
{
    public Image FadingCanvas;
    private bool fading;
    // Start is called before the first frame update
    void Start()
    {
        FadingCanvas.enabled = true;
        fading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (fading && FadingCanvas.color.a < 1)
        {
            FadingCanvas.color = new Color(FadingCanvas.color.r, FadingCanvas.color.g, FadingCanvas.color.b, FadingCanvas.color.a + 0.02f);
        }
        else if (!fading && FadingCanvas.color.a > 0)
        {
            FadingCanvas.color = new Color(FadingCanvas.color.r, FadingCanvas.color.g, FadingCanvas.color.b, FadingCanvas.color.a - 0.02f);
        }
        if (FadingCanvas.color.a <= 0 || FadingCanvas.color.a > 1)
        {
            FadingCanvas.gameObject.SetActive(false);
        }
        else
        {
            FadingCanvas.gameObject.SetActive(true);
        }
    }
    public void LoadLevel(string lvl)
    {
        StartCoroutine(LoadLevelAsync(lvl));
    }
    IEnumerator LoadLevelAsync(string lvl)
    {
        fading = true; 
        yield return new WaitForSecondsRealtime(1f);
        AsyncOperation async=  SceneManager.LoadSceneAsync(lvl);
        
        while (!async.isDone)
        {
            yield return null;
        }
    }
    public static void FadeToLevel(string lvl)
    {
        FindObjectOfType<Fader>().LoadLevel(lvl);
    }
}
