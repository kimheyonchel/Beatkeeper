using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;

    public float currTime = 10.0f;
    public float changeTime = 12.0f;

    public Text Persent;

    [SerializeField]

    Image progressBar;

    private void Start()

    {
        SteamVR_Fade.View(Color.clear, 0.5f);

        Invoke("Load", 1.0f);
    }

    void Update()
    {
        Persent.text = progressBar.fillAmount * 100 + "%";
        
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    void Load()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);

        op.allowSceneActivation = false;

        float timer = 0.0f;

        while (!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            //프로그래스바는 currTime(10초)에 걸쳐 100%를 채운다.
            progressBar.fillAmount = timer / currTime;

            // currTime보다 timer가 커지면 >> 프로그래스바가 100퍼를 채웠다!
            if (timer >= currTime)
                {
                    SteamVR_Fade.View(Color.black, 0.5f);
                }

                if(timer >= changeTime)
                {
                // 씬을 넘어갈지 말지 결정하는 놈이 true가 된다.
                op.allowSceneActivation = true;
                yield break;
            }

        }

    }

}
