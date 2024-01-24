using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.IO;
using System.Linq;
using System;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI TalkText;
    public GameObject PanelIMG;
    public UnityEngine.UI.Image CharacterIMG;
    public bool IsAction;

    XmlNodeList TalkNodeList;
    string[] TextStatementList;
    bool WaitNext;
    int RandomTextIndex;
    int ActionIndex;
    int TalkIndex;

    void Awake()
    {
        IsAction = false;
        TextStatementList = new string[0];
        TalkIndex = 0;
        WaitNext = false;
        RandomTextIndex = 0;
        ActionIndex = 1;
        TalkIndex = 0;
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
            TalkNodeList = TalkObj.NodeData.SelectNodes("DefaultTalking"); // 일상어 노드들을 가져옴
            RandomTextIndex = UnityEngine.Random.Range(0, TalkNodeList.Count);

            ActionIndex = TalkNodeList[RandomTextIndex].SelectNodes("Details").Count;
            TalkIndex = 0;
            TextStatementList = TalkNodeList[RandomTextIndex].SelectNodes("Details")[TalkIndex].InnerText.Split("**");
            
            if (TalkObj.IsNPC) {
                CharacterIMG.sprite = TalkObj.FeelingSprite[int.Parse(TextStatementList[1])];
                CharacterIMG.color = new Color32(255, 255, 255, 255);
                TalkText.rectTransform.offsetMin = new Vector2(200, TalkText.rectTransform.offsetMin.y);
            }

            else {
                CharacterIMG.sprite = null;
                CharacterIMG.color = new Color32(0, 0, 0, 0);
                TalkText.rectTransform.offsetMin = new Vector2(40, TalkText.rectTransform.offsetMin.y);
            }

            TalkText.text = TextStatementList[0];
            WaitNext = true;
            PanelIMG.SetActive(true);
        }

        // 다음 성분으로 넘김
        else if (ActionIndex-1 > TalkIndex) {
            TalkIndex ++;
            TextStatementList = TalkNodeList[RandomTextIndex].SelectNodes("Details")[TalkIndex].InnerText.Split("**");

            if (TalkObj.IsNPC) {
                CharacterIMG.sprite = TalkObj.FeelingSprite[int.Parse(TextStatementList[1])];
            }

            else {
                CharacterIMG.sprite = null;
            }

            TalkText.text = TextStatementList[0];
        }

        // 텍스트 인덱스 초과 시 노드를 초기화
        else {
            CharacterIMG.sprite = null;
            TalkNodeList = null;
            TalkText.text = null;
            WaitNext = false;
            IsAction = false;

            PanelIMG.SetActive(false);
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
