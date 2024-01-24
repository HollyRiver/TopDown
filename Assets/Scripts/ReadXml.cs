using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ReadXml : MonoBehaviour
{
    public XmlNode NodeData;  // 외부 클래스에 넣을 데이터 파일
    public int ID;
    public bool IsNPC;
    public Sprite[] Image;

    public Dictionary<int, Sprite> FeelingSprite;
    TextAsset textAsset;
    XmlDocument xmlDoc;

    void Awake()
    {
        xmlDoc = new XmlDocument();
        FeelingSprite = new Dictionary<int, Sprite>();
    }

    void Start()
    {
        textAsset = (TextAsset)Resources.Load("Data/TalkScript");
        Debug.Log(textAsset);
        xmlDoc.LoadXml(textAsset.text);

        if (IsNPC) {
            LoadXml();
            GenerateData();
        }

        else {
            LoadXmlObj();
        }
        
        Debug.Log(NodeData.SelectSingleNode("Name").InnerText + "의 데이터를 불러왔습니다.");
    }

    void LoadXml()
    {   
        XmlNodeList NPCNodes = xmlDoc.SelectNodes("root/NPC");
        
        foreach (XmlNode node in NPCNodes) {
            if (node.SelectSingleNode("ID").InnerText == ID.ToString()) {
                NodeData = node;
            }
        }
    }

    void LoadXmlObj()
    {   
        XmlNodeList ObjNodes = xmlDoc.SelectNodes("root/Objects");
        
        foreach (XmlNode node in ObjNodes) {
            if (node.SelectSingleNode("ID").InnerText == ID.ToString()) {
                NodeData = node;
            }
        }
    }

    void GenerateData()
    {
        for (int i = 0; i < Image.Length - 1; i++) {
            FeelingSprite.Add(i, Image[i]);
        }
    }
}
