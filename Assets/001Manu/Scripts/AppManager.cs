using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class AppManager : MonoBehaviour
{
    AsyncOperation loader;
    public Slider slider;
    AppManager totalGameManager;
    void Awake()
    {
        if (totalGameManager == null)
        {
            this.gameObject.name = "TotalManager";
            totalGameManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void LoadThisScene(string sceneName)
    {
        SceneManager.LoadScene("Load");
        StartCoroutine(WaitAndLoadFinalScene(sceneName));
    }
    public void QuitarLaApp()
    {
        Application.Quit();
    }
    IEnumerator WaitAndLoadFinalScene(string scene)
    {
        yield return new WaitForSeconds(0.1f);
        loader = SceneManager.LoadSceneAsync(scene);
        loader.allowSceneActivation = false;
        slider = FindObjectOfType<Slider>();
        while (!loader.isDone)
        {
            slider.value = Mathf.Clamp01(loader.progress);
            if (loader.progress >= 0.89f)
                loader.allowSceneActivation = true;
            yield return null;

        }

    }

}
