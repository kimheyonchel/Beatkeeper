using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_NoteMove : MonoBehaviour
{
    public float moveSpeed = 15f;
    public float rotSpeed = 5f;

    Transform S_MovePoint1;

    public static float s1;
    Transform DisTarget;

    void Start()
    {

        S_MovePoint1 = GameObject.Find("S_Note End1").transform;
        DisTarget = GameObject.Find("S_EffectOn").transform;

    }

    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, S_MovePoint1.transform.position, Time.deltaTime * moveSpeed);
        // 노트와 노트 충돌점의 거리 구하기
        s1 = Vector3.Distance(this.transform.position, DisTarget.transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        // 이펙트 켜기
        if (other.transform.name == "S_EffectOn" && this.transform.tag == "S_BNote")
        {
            transform.Find("SimplePortalBlue").gameObject.SetActive(true);
        }
        if (other.transform.name == "S_EffectOn" && this.transform.tag == "S_RNote")
        {
            transform.Find("SimplePortalGold").gameObject.SetActive(true);
        }

        // B숏노트 제거 
        if (this.transform.tag == "S_BNote" && other.transform.tag == "B_Bullet")
        {
            s1 = Vector3.Distance(this.transform.position, DisTarget.transform.position);

            this.gameObject.GetComponent<Animator>().enabled = true;

            if (s1 >= 20 && s1 <= 22)
            {
                ScoreManager.TotalScore += 50 * ScoreManager.best * ScoreManager.x;
                Debug.Log("Best");
            }
            if (s1 >= 22.1 && s1 <= 25)
            {
                ScoreManager.TotalScore += 50 * ScoreManager.good * ScoreManager.x;
                Debug.Log("Good");
            }
            if (s1 >= 25.1 && s1 <= 30)
            {
                ScoreManager.TotalScore += 50 * ScoreManager.bad * ScoreManager.x;
                Debug.Log("Bad");
            }
            Destroy(this.gameObject, 0.2f);
            ScoreManager.combo += 1;
            ScoreManager.shotNote += 1;

        }
        // R숏노트 제거
        if (this.transform.tag == "S_RNote" && other.transform.tag == "R_Bullet")
        {
            this.gameObject.GetComponent<Animator>().enabled = true;

            if (s1 >= 21 && s1 <= 23)
            {
                ScoreManager.TotalScore += 50 * ScoreManager.best * ScoreManager.x;
                Debug.Log("Best");
            }
            if (s1 >= 23.1 && s1 <= 26)
            {
                ScoreManager.TotalScore += 50 * ScoreManager.good * ScoreManager.x;
                Debug.Log("Good");
            }
            if (s1 >= 26.1 && s1 <= 30)
            {
                ScoreManager.TotalScore += 50 * ScoreManager.bad * ScoreManager.x;
                Debug.Log("Bad");
            }

            Destroy(this.gameObject, 0.2f);
            ScoreManager.combo += 1;
            ScoreManager.shotNote += 1;

        }
    }
}
