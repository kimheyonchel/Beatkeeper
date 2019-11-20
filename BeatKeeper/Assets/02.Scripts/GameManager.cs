using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    AudioSource audio;
    GameObject obj;

    public float fadeOutTime = 1f;
    public static float hp = 10f;
    float maxhp = 10f;
    public Image hpBar;

    public GameObject OverText;
    public Transform resultPos;
    public GameObject OverScreen;





    void Start()
    {
        obj = GameObject.Find("[CameraRig]");
        audio = obj.GetComponent<AudioSource>();
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        //if(animator.GetCurrentAnimatorStateinfo(0).nameHash == Animator.StringToHash(""))
        // 노래가 재생되고있고
        if (obj.GetComponent<AudioSource>().enabled == true)
        {
            // 노래가 끝났으면
            if (!audio.isPlaying && PlayerController.IsPause == false)
            {
                //로비로 이동
                Invoke("Fade", 4f);
            }
        }

        // hp바는 hp에 비례하여 깎여나가게
        hpBar.fillAmount = hp / maxhp;
        // hp가 0이 되면 (hp는 각각의 DestroyPos에서 판단하여 전달될 예정)
        if (hp == 0)
        {
            // 타임스케일이 점차 0으로 변한다. >> 만약에 타임스케일이 0에서 멈추지않고 더내려간다면 0까지만 내려가도록 조치를 취해야함
            Time.timeScale -= 0.5f;
            // 타임스케일이 0이 되었을떄
            if(Time.timeScale == 0)
            {
                // 게임오버 텍스트 출력 후 몇초 뒤 로비로
                Invoke("GameOver", 0f);
            }

        }
    }

    // hp가 0이 되면 게임오버다.
    // 게임오버가 되면 타임스케일이 0으로 점차 줄어들며 게임오버 텍스트가 뜬다.
    // 몇 초 뒤에 로비로 이동한다. (이 때 오버 결과창이 뜬 상태로 같이 이동한다.)

    void GameOver()
    {
        // 게임오버 텍스트 출력
        OverText.SetActive(true);
        // 잠시 후 로비씬으로 돌아간다.
        Invoke("Fade", 4f);
        Invoke("OverResult", 4.4f);
    }

    void OverResult()
    {
        // 게임오버 결과창 생성
        Instantiate(OverScreen, resultPos.transform.position, resultPos.transform.rotation);
        DontDestroyOnLoad(OverScreen);
    }

    void BackLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    void Fade()
    {
        SteamVR_Fade.View(Color.black, 0.3f);
        Invoke("BackLobby", 1f);
        Time.timeScale = 1;
    }
    
}
