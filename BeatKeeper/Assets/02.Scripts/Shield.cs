using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

public class Shield : MonoBehaviour
{

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "L_RNote" && this.transform.tag == "R_Shield")
        {
            PlayerController.haptic.Execute(0.2f, 0.2f, 50.0f, 0.5f, PlayerController.rightHand);
            Destroy(other.gameObject, 0.5f);
        }
        if (other.transform.tag == "L_BNote" && this.transform.tag == "B_Shield")
        {
            PlayerController.haptic.Execute(0.2f, 0.2f, 50.0f, 0.5f, PlayerController.leftHand);
            Destroy(other.gameObject, 0.5f);
        }
    }

}