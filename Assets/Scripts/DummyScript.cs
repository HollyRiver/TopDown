using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyScript : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData() {
        talkData.Add(101, new string[] {"안녕?", "이곳에는 처음 왔구나?"});  // Key, value(list)

        talkData.Add(1001, new string[] { "상자다, 놀랍게도." });
        talkData.Add(1002, new string[] { "책상이다, 누군가 사용했던 흔적이 남아있다." });
    }

    public string GetTalk(int id, int talkIndex) {
        return talkData[id][talkIndex];
    }
}
