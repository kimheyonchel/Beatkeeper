using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTime : MonoBehaviour
{
    private int r;

    public float moveSpeed = 5f;
    public float rotspeed = 5f;

    public Transform RanPos1;
    public Transform RanPos2;
    public Transform RanPos3;

    void Start()
    {
        // r은 0~2 중 하나의 값을 가진다.
        r = Random.Range(0, 2);
    }

    
    void Update()
    {
        // 랜덤으로 숫자 정해지면 생성되는 텍스트는 지정된 랜덤 포인트를 바라보며 이동 >> 잠시 후 파괴

        if (r == 0)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, RanPos1.transform.position, Time.deltaTime * moveSpeed);
            Vector3 dir = RanPos1.position - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir.normalized), rotspeed * Time.deltaTime);
        }
        if (r == 1)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, RanPos2.transform.position, Time.deltaTime * moveSpeed);
            Vector3 dir = RanPos2.position - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir.normalized), rotspeed * Time.deltaTime);
        }
        if (r == 2)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, RanPos3.transform.position, Time.deltaTime * moveSpeed);
            Vector3 dir = RanPos3.position - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir.normalized), rotspeed * Time.deltaTime);
        }
        Destroy(this.gameObject, 1f);
    }
}
