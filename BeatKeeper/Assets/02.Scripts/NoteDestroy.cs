using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDestroy : MonoBehaviour
{
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject && other.gameObject.tag != "L_RNote" || other.gameObject.tag != "L_BNote")
        {
            Destroy(other.gameObject);

            ScoreManager.combo = 0;
            ScoreManager.miss += 1;
        }

    }
}
