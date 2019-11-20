using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_NoteRandom : MonoBehaviour
{

    public Transform L_RandomRot1;
    public Transform L_RandomRot2;
    public Transform L_RandomRot3;

    public Transform R_RandomRot1;
    public Transform R_RandomRot2;
    public Transform R_RandomRot3;

    public float moveSpeed = 15f;
    public float rotspeed = 5f;

    private int r;
    void Start()
    {
        // r은 0~5 중 하나의 값을 가진다.
        r = Random.Range(0, 5);
    }

    
    void Update()
    {
        
        if (r == 0)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, L_RandomRot1.transform.position, Time.deltaTime * moveSpeed);
            Vector3 dir = L_RandomRot1.position - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir.normalized), rotspeed * Time.deltaTime);
        }

        if (r == 1)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, L_RandomRot2.transform.position, Time.deltaTime * moveSpeed);
            Vector3 dir = L_RandomRot2.position - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir.normalized), rotspeed * Time.deltaTime);
        }

        if (r == 2)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, L_RandomRot3.transform.position, Time.deltaTime * moveSpeed);
            Vector3 dir = L_RandomRot3.position - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir.normalized), rotspeed * Time.deltaTime);
        }

        if (r == 3)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, R_RandomRot1.transform.position, Time.deltaTime * moveSpeed);
            Vector3 dir = R_RandomRot1.position - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir.normalized), rotspeed * Time.deltaTime);
        }

        if (r == 4)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, R_RandomRot2.transform.position, Time.deltaTime * moveSpeed);
            Vector3 dir = R_RandomRot2.position - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir.normalized), rotspeed * Time.deltaTime);
        }

        if (r == 5)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, R_RandomRot3.transform.position, Time.deltaTime * moveSpeed);
            Vector3 dir = R_RandomRot3.position - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir.normalized), rotspeed * Time.deltaTime);
        }
    }
}
