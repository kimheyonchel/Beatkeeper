using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using UnityEngine.UI;

public class XmlParser : MonoBehaviour
{
    // XmlParser을 Singleton으로 만들어 어디서든 접근가능하도록 하기
    private static XmlParser AttackNote;
    private static object alock = new object();

    #region [XmlParser 스크립트 싱글톤화]
    public static XmlParser INSTANCE
    {
        get
        {
            lock (alock)
            {
                if (AttackNote == null)
                {
                    AttackNote = (XmlParser)FindObjectOfType(typeof(XmlParser));

                    if (FindObjectsOfType(typeof(XmlParser)).Length > 1)
                    {
                        return AttackNote;
                    }
                    if (AttackNote == null)
                    {
                        GameObject singleton = new GameObject();

                        AttackNote = singleton.AddComponent<XmlParser>();

                        singleton.name = typeof(XmlParser).ToString();

                        DontDestroyOnLoad(singleton);
                    }
                }
            }
            return AttackNote;
        }
    }
    #endregion [XmlParser 스크립트 싱글톤화]

    void Start()
    {
        
    }

    // 파싱한 데이터 저장
    public static Dictionary<string, Attack> attackData = new Dictionary<string, Attack>();


    // 여기에 xml파일 데이터 담겨져있음
    public static ArrayList attacksList = new ArrayList();

    // 리스트 추가.
    public void AddType(Attack _cInfo)
    {
        // 받아온 데이터 추가
        // attacksList가 xml에서 가져온 정보를 담고있음
        attacksList.Add(_cInfo);
    }

    // 전체 갯수 얻기
    public int GetListCount()
    {
        return attackData.Count;
    }

}



public class Attack
{
    # region [xml파일의 각각의 정보를 받아옴]
    private string nodeType;
    private string generationPostion;
    private string generationTime;
    private string passTime;
    private string shootingTime;

    public string Note
    {
        get
        {
            return nodeType;
        }
        set
        {
            nodeType = value;
            //Debug.Log("nodeType : " + value);
        }
    }

    public string Position
    {
        get
        {
            return generationPostion;
        }
        set
        {
            generationPostion = value;
            //Debug.Log("generationPostion : " + value);
        }
    }

    public string Create
    {
        get
        {
            return generationTime;
        }
        set
        {
            generationTime = value;
            
            //Debug.Log("generationTime : " + value);
        }
    }

    public string Pass
    {
        get
        {
            return passTime;
        }
        set
        {
            passTime = value;
            //Debug.Log("passTime : " + value);
        }
    }

    public string Shooting
    {
        get
        {
            return shootingTime;
        }
        set
        {
            shootingTime = value;
            //Debug.Log("shootingTime : " + value);
        }
    }
    #endregion [xml파일의 각각의 정보를 받아옴]

    #region [string화되어 있는 시간값 float 값으로 변형]
    // 시간들의 string값을 float값으로 변경하기 위한 메소드
    public static float ConvertFloat(string value)
    {
        // 문자열 분리 >> 00:00:00으로 되어있는걸 00, 00, 00으로 문자열을 분리하여 float로 변형하기 쉽게 바꿈
        // 변형한 값을 string [] >> 이 배열에 넣는다.
        string[] times = value.Split(':');
        
        float total = 0;
        // 맨마지막은 그냥 둔다. (초)
        // 맨마지막에서 한칸 앞은 60초를 곱한다.(분)
        // 그 앞은 60 * 60 초를 곱한다.
        // 그 앞은 60 * 60 * 60
        int j = 0;
        for (int i = times.Length - 1; i >= 0; i--)
        {
            float temp = float.Parse(times[i]);

            temp *= Mathf.Pow(60, j); // 60의 제곱근j >> j가 2이면 60 * 60 j가 3이면 60 * 60 * 60

            total += temp;

            j++;
        }
        return total;
    }

    public override string ToString()
    {
        return nodeType + ", " + generationPostion + ", " + ConvertFloat(Create) + ", " + ConvertFloat(passTime) + ", " + ConvertFloat(shootingTime);
    }
    #endregion [string화되어 있는 시간값 float 값으로 변형]
}




//public struct NoteData
//{
//    public string noteType;
//    public string generationPostion;
//    public long generationTime;
//    public long passTime;
//    public long shootingTime;
//}
//public class XmlParser : MonoBehaviour
//{
//    List<NoteData> noteDataList = new List<NoteData>();

//    public NoteData CheckTimeline(int requestIndex)
//    {
//        return noteDataList[requestIndex];
//    }

//    public List<NoteData> ListParser(XmlNodeList all_nodes)
//    {

//        // 파싱

//        foreach (XmlNode node in all_nodes)
//        {
//            NoteData noteData = new NoteData();
//            noteData.noteType = node.SelectSingleNode("noteType").InnerText;
//            noteData.noteType = node.SelectSingleNode("generationPostion").InnerText;
//            noteData.noteType = node.SelectSingleNode("generationTime").InnerText;
//            noteData.noteType = node.SelectSingleNode("passTime").InnerText;
//            noteData.noteType = node.SelectSingleNode("shootingTime").InnerText;

//            noteDataList.Add(noteData);
//            Debug.Log("[at once] shootingTime :" + node.SelectSingleNode("shootingTime").InnerText);
//            Debug.Log("[at once] passTime : " + node.SelectSingleNode("passTime").InnerText);
//            Debug.Log("[at once] generationTime : " + node.SelectSingleNode("generationTime").InnerText);
//            Debug.Log("[at once] generationPostion : " + node.SelectSingleNode("generationPostion").InnerText);
//            Debug.Log("[at once] noteType : " + node.SelectSingleNode("noteType").InnerText);
//        }

//        return noteDataList;
//    }
//}


// GetList -> ReadPath 로 부터 읽어 들인 XML 을 분석하여 NodeData 목록을 만들어 준다.
// ListParser -> ReadPath 로 부터 읽어 들인 xML 을 파싱하여 XmlNodeList 로 변환하여 반환한다.
// CheckTimeLine -> GetList, ListParse 에서 만들어진 최종 데이터(변형된 - NodeData 목록) 에서 필요한 인덱스번째 녀석을 반환한다.

//ListParser 함수를 만들어야 하는데
// return type 이 list 다.
// 이 list 에 어떤 데이터가 들어가 있어야 하는가??
