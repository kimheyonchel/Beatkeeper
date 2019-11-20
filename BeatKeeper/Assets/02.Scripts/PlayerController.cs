using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    #region [컨트롤러 설정]
    // 아무 컨트롤러
    public static SteamVR_Input_Sources any = SteamVR_Input_Sources.Any;

    // 왼손 컨트롤러
    public static SteamVR_Input_Sources leftHand = SteamVR_Input_Sources.LeftHand;
    //오른손 컨트롤러
    public static SteamVR_Input_Sources rightHand = SteamVR_Input_Sources.RightHand;

    // 액션 트리거 버튼
    public static SteamVR_Action_Boolean trigger;
    // 액션 - 트랙패드 클릭
    public SteamVR_Action_Boolean trackPad;
    //액션 - 트랙패드 터치 여부
    public SteamVR_Action_Boolean trackPadTouch;

    //액션 - 컨트롤러 햅틱 진동
    public static SteamVR_Action_Vibration haptic;
    // 액션 - 컨트롤러 그랩
    //public SteamVR_Action_Boolean grip = SteamVR_Actions.default_GrabGrip;

    // 액션 - 어플리케이션 메뉴 버튼
    public SteamVR_Action_Boolean pause;


    public GameObject MainMenu;

    #endregion

    #region [왼손 컨트롤러 관련]
    // 파란색 음파
    public GameObject B_bullet;
    public GameObject B_Laser;
    public GameObject B_LaserEff;
    // 파란색 음파 생성위치
    public GameObject B_bulletPos;
    // 파란색 실드
    public GameObject Bshield;
    // 파란색 실드 생성위치
    public GameObject BshieldPos;

    // B실드 애니메이터
    Animator B_anim;

    #endregion

    #region [오른손 컨트롤러 관련]
    // 빨간색 음파
    public GameObject R_bullet;
    public GameObject R_Laser;
    public GameObject R_LaserEff;
    // 빨간색 음파 생성위치
    public GameObject R_bulletPos;
    // 빨간색 실드
    public GameObject Rshield;
    // 빨간색 실드 생성위치
    public GameObject RshieldPos;

    // R실드 애니메이터
    Animator R_anim;

    #endregion

    // 일시정지 상태 파악용
    public static bool IsPause;

    public GameObject PauseScreen;

    public float fadeOutTime = 1f;

    public GameObject B_gun;
    public GameObject R_gun;

    void Start()
    {
        trigger = SteamVR_Actions.default_InteractUI;
        trackPad = SteamVR_Actions.default_Teleport;
        trackPadTouch = SteamVR_Actions.default_TrackPadTouch;
        haptic = SteamVR_Actions.default_Haptic;
        pause = SteamVR_Actions.default_Pause;
        // Rshield에 있는 Animator 정보를 가져온다.
        R_anim = Rshield.GetComponent<Animator>();
        // Bshield에 있는 Animator 정보를 가져온다.
        B_anim = Bshield.GetComponent<Animator>();

        IsPause = true;
        SteamVR_Fade.View(Color.clear, 2.5f);

    }


    void Update()
    {

        #region [왼손 컨트롤러]
        //만약 왼손 컨트롤러의 trigger 버튼을 누르면
        if (trigger.GetStateDown(leftHand))
        {
            // 파란 음파를 활성화한다.
            //GameObject Bul = Instantiate(B_bullet, B_bulletPos.transform.position, B_bulletPos.transform.rotation);
            B_Laser.SetActive(true);
            B_LaserEff.SetActive(true);

        }
        if (trigger.GetStateUp(leftHand))
        {
            B_Laser.SetActive(false);
            B_LaserEff.SetActive(false);
        }


        // 만약 왼쪽 컨트롤러의 TrackPad 버튼을 누를때
        if (trackPadTouch.GetStateDown(leftHand))
        {
            // 라인렌더러 비활성화
            B_gun.GetComponent<LineRenderer>().enabled = false;


            B_anim.SetTrigger("ShCreate");

            // Bshield를 활성화한다.
            Bshield.SetActive(true);

            //Debug.Log("B실드 생성");

        }

        // 만약 왼쪽 컨트롤러의 TrackPad 버튼을 누르지 않으면
        if (trackPadTouch.GetStateUp(leftHand))
        {
            B_anim.SetTrigger("ShDeath");
            B_gun.GetComponent<LineRenderer>().enabled = true;
            Invoke("ShieldClose1", 0.2f);

            //Debug.Log("B실드 제거");
        }

        #endregion [왼손 컨트롤러]

        #region [오른손 컨트롤러]
        //만약 오른손 컨트롤러의 trigger 버튼을 누르면
        if (trigger.GetStateDown(rightHand))
        {
            // 빨간 음파를 활성화한다.
            //GameObject Rul = Instantiate(R_bullet, R_bulletPos.transform.position, R_bulletPos.transform.rotation);
            R_Laser.SetActive(true);
            R_LaserEff.SetActive(true);
            //Debug.Log("빨간 음파 발사");
        }
        if (trigger.GetStateUp(rightHand))
        {
            R_Laser.SetActive(false);
            R_LaserEff.SetActive(false);
        }

        // 만약 오른쪽 컨트롤러의 TrackPad 버튼을 누를때
        if (trackPadTouch.GetStateDown(rightHand))
        {
            // 라인렌더러 비활성화
            R_gun.GetComponent<LineRenderer>().enabled = false;

            R_anim.SetTrigger("ShCreate");

            // Rshield를 활성화한다.
            Rshield.SetActive(true);

            //Debug.Log("R실드 생성");
        }

        // 만약 오른쪽 컨트롤러의 TrackPad 버튼을 누르지 않으면
        if (trackPadTouch.GetStateUp(rightHand))
        {
            R_anim.SetTrigger("ShDeath");

            R_gun.GetComponent<LineRenderer>().enabled = true;

            Invoke("ShieldClose2", 0.2f);

            //Debug.Log("R실드 제거");
        }
        #endregion [오른손 컨트롤러]

        Pause();

    }

    void ShieldClose1()
    {
        Bshield.SetActive(false);
        // 라인렌더러1 활성화
        B_gun.GetComponent<LaserPointer2>().enabled = true;
    }

    void ShieldClose2()
    {
        Rshield.SetActive(false);
        // 라인렌더러2 활성화
        R_gun.GetComponent<LaserPointer>().enabled = true;
    }





    void Pause()
    {
        // 만약 아무 컨트롤러의 어플리케이션버튼을 눌렀을 떄
        if(pause.GetStateDown(any))
        {
            // 일시정지 상태가 아니라면
             if (IsPause == false)
            {
                // 타임스케일을 0으로 한다.
                Time.timeScale = 0;
                // IsPause 값을 true로 변경한다.
                IsPause = true;
                this.GetComponent<AudioSource>().Pause();

                // 팝업창 활성화 (UI로)
                PauseScreen.SetActive(true);
                GetComponent<PlayerController>().enabled = false;
                return;
            }


        }

    }

    // 애니메이션 종료 후 발생하는 이벤트 함수
    public void MenuOpen()
    {
        MainMenu.SetActive(true);
        IsPause = false;
        // 게임 재시작 등을 했을 때 애니메이션 반복 막기용
        this.GetComponent<Animator>().enabled = false;
    }

}
