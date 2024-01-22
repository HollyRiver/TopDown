using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI TalkText;
    public GameObject IMG;
    public bool IsAction;

    void Awake() {
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
}
