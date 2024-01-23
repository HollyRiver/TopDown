using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Xml;
using UnityEngine;
using System.IO;
using UnityEngine.UIElements;
using System.Linq;
using System;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI TalkText;
    public GameObject IMG;
    public bool IsAction;

    XmlNodeList TalkNodeList;
    bool WaitNext;
    int RandomTextIndex;
    int ActionIndex;
    int CurrIndex;

    void Awake()
    {
        IsAction = false;
        CurrIndex = 0;
    }

    void Start()
    {  
        // 파일 존재 여부 확인
        if (!File.Exists("./Data")) {
            CreateXml();
        }
    }

    public void Action(ReadXml scanedObj) {
        if (IsAction) {
            if (WaitNext) {
                IsAction = true;
                Talk(scanedObj);
            }

            else {
                IsAction = false;
            }
        }

        else {
            IsAction = true;
            Talk(scanedObj);
        }
    }

    void Talk(ReadXml TalkObj) {
        // 최초 액션 시도 시 노드를 가져옴
        if (TalkNodeList == null) {
            TalkNodeList = TalkObj.NodeData.SelectNodes("DefaultTalking"); // 일상어 노드 두개를 불러옴
            RandomTextIndex = UnityEngine.Random.Range(0, TalkNodeList.Count);

            ActionIndex = TalkNodeList[RandomTextIndex].SelectNodes("Details").Count;

            CurrIndex = 0;
            TalkText.text = TalkNodeList[RandomTextIndex].SelectNodes("Details")[CurrIndex].InnerText;
            WaitNext = true;
            IMG.SetActive(true);
        }

        // 다음 성분으로 넘김
        else if (ActionIndex-1 > CurrIndex) {
            CurrIndex ++;
            TalkText.text = TalkNodeList[RandomTextIndex].SelectNodes("Details")[CurrIndex].InnerText;
        }

        // 텍스트 인덱스 초과 시 노드를 초기화
        else {
            TalkNodeList = null;
            WaitNext = false;
            IsAction = false;

            IMG.SetActive(false);
        }

        // catch (Exception ex)
        // {
        //     Debug.Log($"오류 발생 : {ex.Message}");
        //     Debug.Log(CurrText);
        //     Debug.Log(TalkNodeList);
        // }
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
