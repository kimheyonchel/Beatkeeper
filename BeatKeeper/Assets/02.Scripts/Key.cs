using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    public enum State
    {
        Minus,
        Plus
    }

    public State currentState;

    public Scrollbar scrollbar;
    public int scrollContentsNumber;

    public float oneIntervalMoveSpeed;

    float scrollOneMovementValue;

    bool isPlusMove;
    bool isMinusMove;

    float minusValue;
    float plusValue;

    void Start()
    {
        scrollbar.value = 0;
        scrollOneMovementValue = 1f / (scrollContentsNumber - 1);
    }

    private void Update()
    {
        if (!isPlusMove)
        {
            plusValue = Mathf.Clamp(scrollbar.value + scrollOneMovementValue, 0, 1);
        }
        else if (isPlusMove)
        {
            scrollbar.value += oneIntervalMoveSpeed * Time.deltaTime;
            if (scrollbar.value >= plusValue)
            {
                isPlusMove = false;
            }
        }

        if (!isMinusMove)
        {
            minusValue = Mathf.Clamp(scrollbar.value - scrollOneMovementValue, 0, 1);
        }
        else if (isMinusMove)
        {
            scrollbar.value -= oneIntervalMoveSpeed * Time.deltaTime;
            if (scrollbar.value <= minusValue)
            {
                isMinusMove = false;
            }
        }
    }

    public void OneIntervalMove()
    {
        if (currentState == State.Plus)
        {
            isPlusMove = true;
        }
        else if (currentState == State.Minus)
        {
            isMinusMove = true;
        }
    }
}