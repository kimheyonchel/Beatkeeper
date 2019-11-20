using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Valve.VR.Extras;
using Valve.VR;

public class BtnMgr : MonoBehaviour
{
    public GameObject KeyScreen;
    public GameObject SettingScreen;
    public GameObject PauseScreen;
    public GameObject camerarig;
    SteamVR_LaserPointer laserPointer;

    public float fadeOutTime = 1f;
    public GameObject button;

    public GameObject Score1;
    public GameObject Score2;
    public GameObject NoteMgr;

    public GameObject result;
    GameObject pressButton;

    GameObject Camrig;
    public GameObject Menu;

    void Start()
    {
        SteamVR_Fade.View(Color.clear, 0.5f);
        pressButton = GameObject.Find("Press any button");
        Camrig = GameObject.Find("[CameraRig]");
    }

    void Update()
    {
        if (PlayerController.trigger.GetStateDown(PlayerController.any) && pressButton.activeSelf == true)
        {
            Invoke("SceneMove", 0.2f);
        }
    }

    public void SceneMove()
    {
        StartCoroutine(Fade());
        Invoke("Load", 0.8f);
    }


    // 게임시작
    public void GameStart()
    {
        Invoke("musicStart", 5.4f);
        //button.GetComponent<Animator>().enabled = true;
        NoteMgr.SetActive(true);
        Menu.SetActive(false);
        Score1.SetActive(true);
        Score2.SetActive(true);
        ScoreManager.combo = 0;
        ScoreManager.TotalScore = 0;
        ScoreManager.miss = 0;
        ScoreManager.maxcombo = 0;
        ScoreManager.allNote = 0;
        ScoreManager.shotNote = 0;
    }

    // 음악 시작용
    void musicStart()
    {
        Camrig.GetComponent<AudioSource>().enabled = true;
    }

    // 게임씬 이동용 >> 지연시켜야해서 Invoke로
    public void Load()
    {
        
        if (pressButton.activeSelf == true)
        {
            LoadingSceneManager.LoadScene("GameStage_1115");
        }
        if (this.transform.tag == "MainMenu")
        {
            SceneManager.LoadScene("Lobby");
        }
    }
    public void Load2()
    {
        LoadingSceneManager.LoadScene("GameStage_1115");
    }

    // 조작법 열기
    public void Key()
    {
        if (SettingScreen.activeSelf == false)
        {
            KeyScreen.SetActive(true);
            button.GetComponent<Animator>().enabled = true;
            Invoke("AnimReturn", 0.25f);
        }
    }

    // 환경설정 열기
    public void Setting()
    {
        if (KeyScreen.activeSelf == false)
        {
            SettingScreen.SetActive(true);
            button.GetComponent<Animator>().enabled = true;
            Invoke("AnimReturn", 0.25f);
        }
    }

    public void PopupExit()
    {
        Invoke("popupDown", 0.3f);
    }

    public void popupDown()
    {
        if (this.transform.tag == "Key")
        {
            KeyScreen.SetActive(false);   
        }
        if (this.transform.tag == "Setting")
        {
            SettingScreen.SetActive(false);
        }        
    }

    public void AnimReturn()
    {
        button.GetComponent<Animator>().enabled = false;
    }

    // 게임종료
    public void GameExit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // play모드를 false로 전환
        #elif UNITY_WEBPLAYER
        Application.OpenURL("http://google.com"); // 구글웹으로 전환
        #else
        Application.Quit(); // 어플리케이션 종료
        #endif
    }


    // 게임정지 팝업창 - 메인메뉴가기
    public void MainMenu()
    {
        Invoke("Load", 0.8f);
        StartCoroutine(Fade());
        Time.timeScale = 1;



    }
    // 게임정지 팝업창 - 게임재시작
    public void Restart()
    {
        Invoke("Load2", 0.8f);
        StartCoroutine(Fade());
        Time.timeScale = 1;
        ScoreManager.combo = 0;
        ScoreManager.TotalScore = 0;
        ScoreManager.miss = 0;
        ScoreManager.maxcombo = 0;
        ScoreManager.allNote = 0;
        ScoreManager.shotNote = 0;

    }
    // 게임중지 팝업창 - 정지해제
    public void UnPause()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            PlayerController.IsPause = false;

            // 팝업창 비활성화
            PauseScreen.SetActive(false);
            camerarig.GetComponent<PlayerController>().enabled = true;
            camerarig.GetComponent<AudioSource>().Play();
            return;
        }
    }
    IEnumerator Fade()
    {
        SteamVR_Fade.View(Color.black, 0.5f);

        yield return new WaitForSeconds(fadeOutTime);

    }

}
