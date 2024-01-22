using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class CreateXML : MonoBehaviour
{
    void Start()
    {
        CreateXml();
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
