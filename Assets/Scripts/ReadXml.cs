using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ReadXml : MonoBehaviour
{
    public int ID;
    public bool IsNPC;
    TextAsset textAsset;
    XmlDocument xmlDoc = new XmlDocument();

    void Start()
    {
        textAsset = (TextAsset)Resources.Load("Data/TalkScript");
        Debug.Log(textAsset);
        xmlDoc.LoadXml(textAsset.text);

        if (IsNPC)
            LoadXml();
        
        else
            LoadXmlObj();
    }

    void LoadXml()
    {   
        Debug.Log("NPC 데이터를 불러오는 중입니다.");
        XmlNodeList NPCNodes = xmlDoc.SelectNodes("root/NPC");
        
        foreach (XmlNode node in NPCNodes) {
            Debug.Log("Name : " + node.SelectSingleNode("Name").InnerText);
            Debug.Log("ID : " + node.SelectSingleNode("ID").InnerText);
            Debug.Log("DefaultTalking : " + node.SelectSingleNode("DefaultTalking").InnerText);
        }
    }

    void LoadXmlObj()
    {   
        Debug.Log("오브젝트 데이터를 불러오는 중입니다.");
        XmlNodeList ObjNodes = xmlDoc.SelectNodes("root/Objects");
        
        foreach (XmlNode node in ObjNodes) {
            Debug.Log("Name : " + node.SelectSingleNode("Name").InnerText);
            Debug.Log("ID : " + node.SelectSingleNode("ID").InnerText);
            Debug.Log("DefaultDescription : " + node.SelectSingleNode("DefaultDescription").InnerText);
        }
    }
}
