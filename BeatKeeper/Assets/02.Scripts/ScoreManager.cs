using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text Score;
    public Text Combo;
    public Text X;
    public Text Miss;
    public Text MaxCount;
    public Text AllNote;
    public Text ShotNote;

    public static int TotalScore = 0;
    public static int combo = 0;
    public static int miss = 0;
    public static int maxcombo = 0;
    public int maxcount = 0;

    public static int best = 3;
    public static int good = 2;
    public static int bad = 1;

    // 생성된 총 노트 수
    public static int allNote = 0;
    public static int shotNote = 0;

    // 배율
    public static int x = 1;

    void Update()
    {
        if (combo == 5)
        {
            x = 2;
        }
        else if (combo == 10)
        {
            x = 4;
        }
        else if (combo == 20)
        {
            x = 8;
        }
        else if (combo == 0)
        {
            x = 1;
        }
        
        // maxcombo 받기
        // 만약 maxcombo의 값이 콤보보다 낮으면
        if (maxcombo < combo)
        {
            //maxcombo의 값은 콤보값이다.
            maxcombo = combo;
        }

        if (combo == 0)
        {
            maxcount = maxcombo;
        }

        Score.text = TotalScore.ToString();
        Combo.text = combo.ToString();
        Miss.text = miss.ToString();
        X.text = x.ToString();
        MaxCount.text = maxcombo.ToString();
        AllNote.text = allNote.ToString();
        ShotNote.text = shotNote.ToString();
    }
}
