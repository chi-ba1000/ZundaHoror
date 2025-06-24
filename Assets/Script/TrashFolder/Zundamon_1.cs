using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Zundamon_1 : MonoBehaviour
{
    private CharacterController characterController;  // CharacterController型の変数
    private Vector3 moveVelocity;  // キャラクターコントローラーを動かすためのVector3型の変数
    private Vector3 horizontalVelocity;
    [SerializeField] private Animator animator;
    [SerializeField] public Transform verRot;  //縦の視点移動の変数(カメラに合わせる)
    [SerializeField] public Transform horRot;  //横の視点移動の変数(プレイヤーに合わせる)
    [SerializeField] public float moveSpeed;  //移動速度
    [SerializeField] public float jumpPower;  //ジャンプ力

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.visible = false;
    }

    void Update()
    {
        // マウスによる視点操作
        float X_Rotation = Input.GetAxis("Mouse X");
        float Y_Rotation = Input.GetAxis("Mouse Y");
        horRot.transform.Rotate(new Vector3(0, X_Rotation * 2, 0));
        verRot.transform.Rotate(new Vector3(-Y_Rotation * 2, 0, 0));


        // 接地しているとき
        if (characterController.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                moveVelocity = transform.forward * moveSpeed * Time.deltaTime;
            }
            //Sキーがおされたら
            if (Input.GetKey(KeyCode.S))
            {
                moveVelocity = transform.forward * -1 * moveSpeed * Time.deltaTime;
            }
            //Aキーがおされたら
            if (Input.GetKey(KeyCode.A))
            {
                moveVelocity = transform.right * -1 * moveSpeed * Time.deltaTime;
            }
            //Dキーがおされたら
            if (Input.GetKey(KeyCode.D))
            {
                moveVelocity = transform.right * moveSpeed * Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveVelocity.y = jumpPower;
            }
            
        }
        else
        {
            moveVelocity.y += Physics.gravity.y * Time.deltaTime;
        }


        // キャラクターを動かす
        characterController.Move(moveVelocity);

        moveVelocity.x = 0;
        moveVelocity.z = 0;
        
        // 移動スピードをアニメーターに反映する
        animator.SetFloat("MoveSpeed", new Vector3(moveVelocity.x, 0, moveVelocity.z).magnitude);       
    }
}
