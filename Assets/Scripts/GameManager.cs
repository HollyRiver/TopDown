using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Xml;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI TalkText;
    public GameObject IMG;
    public bool IsAction;

    void Awake()
    {
        IsAction = false;
    }

    public void Action(GameObject scanedObj) {
        if (IsAction) {
            IsAction = false;
        }

        else {
            IsAction = true;
            TalkText.text = "이것의 이름은 " + scanedObj.name + "(이)라고 한다.";
        }
        
        IMG.SetActive(IsAction);
    }

    void Start()
    {  
        // 파일 존재 여부 확인
        if (!File.Exists("./Data")) {
            CreateXml();
        }
    }

    void CreateXml()
    {
        XmlDocument xmlDoc = new XmlDocument();
        // Xml 선언
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));  // xmlDoc에 파일을 종속시킨다?

        // 루트 노드 생성
        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "CharacterInfo", string.Empty);  // 노드를 CharacterInfo라는 이름의 빈 문자 파일로 생성한다?
        xmlDoc.AppendChild(root);

        // 자식 노드 생성
        XmlNode child = xmlDoc.CreateNode(XmlNodeType.Element, "Character", string.Empty);
        root.AppendChild(child);

        // 자식 노드에 들어갈 속성 생성
        XmlElement name = xmlDoc.CreateElement("Name");
        name.InnerText = "wergia";  // name에 텍스트 할당
        child.AppendChild(name);

        XmlElement lv = xmlDoc.CreateElement("Level");
        lv.InnerText = "1";
        child.AppendChild(lv);

        XmlElement exp = xmlDoc.CreateElement("Experience");
        exp.InnerText = "0";
        child.AppendChild(exp);

        xmlDoc.Save("./Data/character.xml");
    }
}
