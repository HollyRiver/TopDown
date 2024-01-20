using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    float h;
    float v;
    bool IsMoving;
    bool IsHorizontalMove;
    bool IsVerticalMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        IsMoving = false;
        IsHorizontalMove = false;
        IsVerticalMove = false;
    }

    void FixedUpdate()
    {   
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        anim.SetInteger("hAxisRaw", (int)h);
        anim.SetInteger("vAxisRaw", (int)v);

        // 혹시모를 이중처리
        if (h == 0) {
            IsHorizontalMove = false;
            anim.SetBool("Horizontal1st", false);
        }

        else {
            IsHorizontalMove = true;
        }

        if (v == 0) {
            IsVerticalMove = false;
            anim.SetBool("Horizontal1st", true);
        }

        else {
            IsVerticalMove = true;
        }

        // if (IsHorizontalMove) {
        //     h = Input.GetAxisRaw("Horizontal") * 3;
        //     v = 0;
        // }

        // else {
        //     h = 0;
        //     v = Input.GetAxisRaw("Vertical") * 3;
        // }
        // 대각이동을 제한한 코드

        rigid.velocity = new Vector2(h, v) * 3;  // velocity를 설정해준 거라 델타타임 안해줘도 됨
    }

    void Update()
    {
        // // 횡이동시 || 종이동 버튼에서 손을 뗄 시
        // if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Vertical")) {
        //     IsHorizontalMove = true;  // 횡이동이 가능
        // }
        // // 종이동시 || 횡이동 버튼에서 손을 뗄 시
        // else if (Input.GetButtonDown("Vertical") || Input.GetButtonUp("Horizontal")) {
        //     IsHorizontalMove = false;  // 종이동이 가능
        // }
        // 대각이동을 제한한 코드

        // 토비폭스 식 무브먼트
        // 정지 상태일 때 횡이동
        if (!IsMoving && Input.GetButtonDown("Horizontal")) {
            IsMoving = true;  // 움직인다
            IsHorizontalMove = true;  // 횡이동
            IsVerticalMove = false;  // 종이동은 없다(최초상태에서)
            anim.SetBool("Horizontal1st", true);  // 횡이동 우선
        }
        // 정지 상태일 때 종이동
        if (!IsMoving && Input.GetButtonDown("Vertical")) {
            IsMoving = true;
            IsHorizontalMove = false;
            IsVerticalMove = true;
            anim.SetBool("Horizontal1st", false);  // 종이동 우선
        }
        // 횡이동 중 종이동 실행
        if (IsHorizontalMove && Input.GetButtonDown("Vertical")) {
            IsVerticalMove = true;  // 종이동 중
            anim.SetBool("Horizontal1st", true);  // 횡이동 우선
        }
        // 종이동 중 횡이동 실행
        if (IsVerticalMove && Input.GetButtonDown("Horizontal")) {
            IsHorizontalMove = true;  // 횡이동 중
            anim.SetBool("Horizontal1st", false);
        }
        // 종이동 중 종이동 종료
        if (!IsHorizontalMove && Input.GetButtonUp("Vertical")) {
            IsVerticalMove = !IsVerticalMove;  // 이중 입력 처리
            anim.SetBool("Horizontal1st", !IsVerticalMove);  // true, 횡이동 우선
        }
        // 횡이동 중 횡이동 종료
        if (!IsVerticalMove && Input.GetButtonUp("Horizontal")) {
            IsHorizontalMove = !IsHorizontalMove;  // 이중 입력 처리
            anim.SetBool("Horizontal1st", !IsHorizontalMove);  // false, 종이동 우선
        }
        // 대각이동 중
        if (IsHorizontalMove && IsVerticalMove) {
            if (Input.GetButtonUp("Horizontal"))  // 종이동 전환
            {
                IsHorizontalMove = false;
                anim.SetBool("Horizontal1st", false);
            }
            
            else if (Input.GetButtonUp("Vertical"))  // 횡이동 전환
            {
                IsVerticalMove = false;
                anim.SetBool("Horizontal1st", true);
            }
        }
        // 이동중이 아닐 시
        if (!IsHorizontalMove && !IsVerticalMove) {
            IsMoving = false;
        }
    }
}
