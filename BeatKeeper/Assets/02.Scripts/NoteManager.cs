using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 생성에 대한 처리
// - XmlPaser.attacksList 를 돌면서 생성 시간
public class NoteManager : MonoBehaviour
{
    #region [노트 오브젝트, 생성위치]
    //숏 오브젝트
    public GameObject S_RNote;
    public GameObject S_RNote2;
    public GameObject S_RNote3;
    public GameObject S_BNote;
    public GameObject S_BNote2;
    public GameObject S_BNote3;
    //니들 오브젝트
    public GameObject N_RNote;
    public GameObject N_RNote2;
    public GameObject N_BNote;
    public GameObject N_BNote2;
    //롱 오브젝트
    public GameObject L_RNote;
    public GameObject L_RNote2;
    public GameObject L_RNote3;
    public GameObject L_BNote;
    public GameObject L_BNote2;
    public GameObject L_BNote3;

    //숏 생성위치
    public Transform S_NotePotal1;
    public Transform S_NotePotal2;
    public Transform S_NotePotal3;
    //니들 생성위치
    public Transform N_NotePotal1;
    public Transform N_NotePotal2;
    //롱 생성위치
    public Transform L_NotePotal1;
    public Transform L_NotePotal2;
    public Transform L_NotePotal3;

    #endregion

    #region [파싱한 xml 자료 코루틴으로 부르기]
    void Start()
    {
        // XmlParser의 attacksList가 값이 있는지 확인
        //print("11111 "+ XmlParser.attacksList.Count);
        foreach (Attack attack in XmlParser.attacksList)
        {
            // attack의 string값을 출력
            //print(attack.ToString());
            StartCoroutine(CreateNote(attack));
        }

    }
    #endregion

    #region [노트 생성 코루틴]
    IEnumerator CreateNote(Attack attack)
    {
        // attack에 있는 Create값을 float화하여 가져오기
        yield return new WaitForSeconds(Attack.ConvertFloat(attack.Create));

        // Create값 불러올 때 해당 값의 Note 확인하여 정해진 위치에 노트 생성
        switch(attack.Note)
        {
            case "ShortNote":
                if (attack.Position == "1")
                {
                    Instantiate(S_RNote, S_NotePotal1.transform.position, S_NotePotal1.transform.rotation);
                    ScoreManager.allNote += 1;
                }
                if (attack.Position == "2")
                {
                    Instantiate(S_RNote2, S_NotePotal2.transform.position, S_NotePotal2.transform.rotation);
                    ScoreManager.allNote += 1;
                }
                if (attack.Position == "3")
                {
                    Instantiate(S_RNote3, S_NotePotal3.transform.position, S_NotePotal3.transform.rotation);
                    ScoreManager.allNote += 1;
                }
                if (attack.Position == "4")
                {
                    Instantiate(S_BNote, S_NotePotal1.transform.position, S_NotePotal1.transform.rotation);
                    ScoreManager.allNote += 1;
                }
                if (attack.Position == "5")
                {
                    Instantiate(S_BNote2, S_NotePotal2.transform.position, S_NotePotal2.transform.rotation);
                    ScoreManager.allNote += 1;
                }
                if (attack.Position == "6")
                {
                    Instantiate(S_BNote3, S_NotePotal3.transform.position, S_NotePotal3.transform.rotation);
                    ScoreManager.allNote += 1;
                }
                break;

            case "NeedleNote":
                if (attack.Position == "1")
                {
                    Instantiate(N_RNote, N_NotePotal1.transform.position, N_NotePotal1.transform.rotation);
                    ScoreManager.allNote += 1;
                }
                if (attack.Position == "2")
                {
                    Instantiate(N_RNote2, N_NotePotal2.transform.position, N_NotePotal2.transform.rotation);
                    ScoreManager.allNote += 1;
                }
                if (attack.Position == "3")
                {
                    Instantiate(N_BNote, N_NotePotal1.transform.position, N_NotePotal1.transform.rotation);
                    ScoreManager.allNote += 1;
                }
                if (attack.Position == "4")
                {
                    Instantiate(N_BNote2, N_NotePotal2.transform.position, N_NotePotal2.transform.rotation);
                    ScoreManager.allNote += 1;
                }
                break;

                case "LongNote":
                if (attack.Position == "1")
                {
                    Instantiate(L_RNote, L_NotePotal1.transform.position, L_NotePotal1.transform.rotation);
                    ScoreManager.allNote += 1;
                }
                if (attack.Position == "2")
                {
                    Instantiate(L_RNote2, L_NotePotal2.transform.position, L_NotePotal2.transform.rotation);
                    ScoreManager.allNote += 1;
                }
                if (attack.Position == "3")
                {
                    Instantiate(L_RNote3, L_NotePotal3.transform.position, L_NotePotal3.transform.rotation);
                    ScoreManager.allNote += 1;
                }
                break;
        }
        
        // note 생성 로그 확인
        //print("노트생성 "+ attack.Note + ", 생성시간 : " + attack.Create);

    }
    #endregion
}
