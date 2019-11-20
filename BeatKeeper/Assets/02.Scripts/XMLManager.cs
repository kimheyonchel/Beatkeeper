using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class XMLManager : MonoBehaviour
{
    string filePath = "Attack";
    XmlNodeList all_nodes;

    void Awake()
    {
        ReadPath();
    }

    public void ReadPath()
    {
        TextAsset txtAsset = (TextAsset)Resources.Load("Attack");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(txtAsset.text);
        all_nodes = xmlDoc.SelectNodes("Attacks");

        XmlElement attack = xmlDoc["Attacks"];

        //노드 설정값 변수에 대입
        foreach (XmlElement a in attack.ChildNodes)
        {
            Attack attacks = new Attack();

            attacks.Note = a.GetAttribute("noteType");
            attacks.Position = a.GetAttribute("generationPostion");
            attacks.Pass = a.GetAttribute("passTime");
            attacks.Create = a.GetAttribute("generationTime");
            attacks.Shooting = a.GetAttribute("shootingTime");

            //Debug.Log("noteType : " + a.GetAttribute("noteType"));
            //Debug.Log("generationPostion : " + a.GetAttribute("generationPostion"));
            //Debug.Log("generationTime : " + a.GetAttribute("generationTime"));
            //Debug.Log("passTime : " + a.GetAttribute("passTime"));
            //Debug.Log("shootingTime : " + a.GetAttribute("shootingTime"));


            XmlParser.INSTANCE.AddType(attacks);
        }
    }
}