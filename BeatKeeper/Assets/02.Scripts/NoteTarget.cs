using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxJump : MonoBehaviour
{
    //protected float Animation;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Animation += Time.deltaTime;

        Animation = Animation % 5f;

        transform.position = MathParabola.Parabola(Vector3.zero, Vector3.forward * 10f, 5f, Animation / 5f);
        */
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "R_Controller" && this.transform.tag == "R_Monster")
        {
            //Debug.Log("신호 오니?");
            Destroy(gameObject);
        }
        if (other.transform.tag == "L_Controller" && this.transform.tag == "B_Monster")
        {
            Destroy(gameObject);
        }
    }
}
