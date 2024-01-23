using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ReadXml : MonoBehaviour
{
    public XmlNode NodeData;  // 외부 클래스에 넣을 데이터 파일
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

        Debug.Log(NodeData.SelectSingleNode("Name").InnerText + "의 데이터를 불러왔습니다.");
        
        // Debug.Log(NodeData.SelectNodes("DefaultTalking")[0].SelectNodes("Details")[0].InnerText);
    }

    void LoadXml()
    {   
        Debug.Log("NPC 데이터를 불러오는 중입니다.");
        XmlNodeList NPCNodes = xmlDoc.SelectNodes("root/NPC");
        
        foreach (XmlNode node in NPCNodes) {
            if (node.SelectSingleNode("ID").InnerText == ID.ToString()) {
                NodeData = node;
            }
        }
    }

    void LoadXmlObj()
    {   
        Debug.Log("오브젝트 데이터를 불러오는 중입니다.");
        XmlNodeList ObjNodes = xmlDoc.SelectNodes("root/Objects");
        
        foreach (XmlNode node in ObjNodes) {
            if (node.SelectSingleNode("ID").InnerText == ID.ToString()) {
                NodeData = node;
            }
        }
    }
}
