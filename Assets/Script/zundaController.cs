using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class zundaController : MonoBehaviour
{
    [SerializeField] public Transform verRot;  
    [SerializeField] public Transform horRot;
    public float walkSpeed = 10f ,jumpPower = 0.5f ,mouseSens = 2.5f;
    private Vector2 charaMove = Vector2.zero;
    private float charaJump ,yVelocity ,xMove ,yMove ,xMoveKeep ,yMoveKeep ;
    private Vector3 move;
    private CharacterController charaCon;

    void Start()
    {
        charaCon = GetComponent<CharacterController>();
        Cursor.visible = false;
    }
    void Update()
    {
        float X_Rotation = Input.GetAxis("Mouse X");
        float Y_Rotation = Input.GetAxis("Mouse Y");
        horRot.transform.Rotate(new Vector3(0, X_Rotation * mouseSens, 0));
        verRot.transform.Rotate(-Y_Rotation * mouseSens, 0, 0);
        float speed = Input.GetKey(KeyCode.LeftShift) ? walkSpeed * 1.5f : walkSpeed;

        if (charaCon.isGrounded)
        {
            xMove = charaMove.x * speed;
            yMove = charaMove.y * speed;
            if (yVelocity > 0)
            {
                move = new Vector3(xMove, yVelocity, yMove);
            }
            else
            {
                move = new Vector3(xMove, -0.5f, yMove);
            }
            move = transform.TransformDirection(move);
        }
        else
        {
            yVelocity += Physics.gravity.y * Time.deltaTime;
            move = new Vector3(xMoveKeep, yVelocity, yMoveKeep);
        }
        xMoveKeep = move.x;
        yMoveKeep = move.z;
        charaCon.Move(move * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        charaMove = context.ReadValue<Vector2>();
    }

    public void Onjump(InputAction.CallbackContext context)
    {
        if (charaCon.isGrounded && context.phase == InputActionPhase.Performed)
        {
            //charaJump = context.ReadValue<float>();
            //yVelocity = charaJump * jumpPower;
            yVelocity = jumpPower;
            //Debug.Log(context.phase);   
            //Debug.Log("ジャンプしました");
        }
        
    }
}


