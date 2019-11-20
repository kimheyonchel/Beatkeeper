using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timeText;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("musicStart", 3.4f);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeText.text = time.ToString();

        if(Input.GetButtonDown("Horizontal"))
        {
            Debug.Log("현재 시간은" + time.ToString());
        }
    }

    void musicStart()
    {
        this.GetComponent<AudioSource>().enabled = true;
    }

}
