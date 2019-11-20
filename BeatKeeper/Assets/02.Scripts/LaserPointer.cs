using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class LaserPointer : MonoBehaviour
{
    private SteamVR_Behaviour_Pose pose;
    private SteamVR_Input_Sources hand;
    private LineRenderer line;

    // 클릭 이벤트에 반응할 액션
    public SteamVR_Action_Boolean trigger;

    public static float maxDistance = 150f;

    // 라인 색상
    public Color color = Color.red;
    public Color clickedColor = Color.blue;

    // 레이캐스트 변수 선언
    private RaycastHit hit;
    private Transform tr;

    // 이벤트 전달할 객체의 저장변수
    private GameObject prevObject;
    private GameObject currObject;

    void Start()
    {
        trigger = SteamVR_Actions.default_InteractUI;

        tr = GetComponent<Transform>();

        // 컨트롤러 정보 검출
        pose = GetComponentInParent<SteamVR_Behaviour_Pose>();

        hand = pose.inputSource;

        CreateLineRenderer();
    }

    void CreateLineRenderer()
    {
        line = this.gameObject.AddComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.receiveShadows = false;

        line.positionCount = 2;
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, new Vector3(this.transform.position.x, this.transform.position.y, maxDistance));

        // 라인 시작점 너비
        line.startWidth = 0.03f;
        // 라인 끝점 너비
        line.endWidth = 0.005f;

        // 라인 머티리얼 및 색상 설정
        line.material = new Material(Shader.Find("Unlit/Color"));
        line.material.color = this.color;
    }
    
    void Update()
    {


        if (Physics.Raycast(tr.position, tr.forward, out hit, maxDistance))
        {
            // 라인의 끝점 위치를 캐스팅한 지점의 좌표로 변경
            //line.SetPosition(1, new Vector3(0, 0, hit.distance));

            currObject = hit.collider.gameObject;

            if(currObject != prevObject)
            {
                // 현재 객체에 pointenter 이벤트 전달
                ExecuteEvents.Execute(currObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
                // 이전 객체에 pointexit 이벤트 전달
                ExecuteEvents.Execute(prevObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                prevObject = currObject;
            }

            if(trigger.GetStateDown(hand))
            {
                ExecuteEvents.Execute(currObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
            }
        }
        else
        {
            if (prevObject != null)
            {
                // 이전 객체에 pointerexit 이벤트 전달
                ExecuteEvents.Execute(prevObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);

                prevObject = null;
            }
        }

        if (trigger.GetStateUp(hand))
        {
            line.material.color = this.color;
        }
        if (trigger.GetStateDown(hand))
        {
            line.material.color = clickedColor;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "R_Monster" && this.transform.tag == "R_Controller")
        {
            if (other.transform.name == "N_RNote3(Clone)")
            {
                other.GetComponent<N_NoteRoot>().enabled = false;
                other.GetComponent<N_NoteRandom>().enabled = true;
            }
            if (other.transform.name == "N_RNote4(Clone)")
            {
                other.GetComponent<N_NoteRoot2>().enabled = false;
                other.GetComponent<N_NoteRandom>().enabled = true;
            }
            Destroy(other.gameObject, 1.5f);
            ScoreManager.TotalScore += 100 * ScoreManager.x;
            ScoreManager.combo += 1;
            ScoreManager.shotNote += 1;
        }

    }

}
