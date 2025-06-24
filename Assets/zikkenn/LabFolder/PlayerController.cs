using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Transform cameraParent;  
    [SerializeField] public Transform cameraParent2;
    public float walkSpeed = 10f, jumpPower = 3f, mouseSens = 2.5f;
    
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
        cameraParent.transform.Rotate(new Vector3(0, X_Rotation * mouseSens, 0));
        cameraParent2.transform.Rotate(-Y_Rotation * mouseSens, 0, 0);
        Vector3 charaMove2 = new Vector3(charaMove.x, 0, charaMove.y);
        charaMove2 = transform.TransformDirection(charaMove2);
        float speed = Input.GetKey(KeyCode.LeftShift) ? walkSpeed * 3f : walkSpeed;
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        //Debug.Log(X_Rotation);
        //Debug.Log(Y_Rotation);
        Debug.Log(cameraForward);

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
                move = new Vector3(xMove, -0.1f, yMove);
            }
            move = transform.TransformDirection(move);
        }
        else
        {
            yVelocity += Physics.gravity.y * Time.deltaTime;
            move = new Vector3(xMoveKeep, yVelocity, yMoveKeep);
        }
        /*if (charaMove2.magnitude > 0.5f)
        {
            targetRotation = Quaternion.LookRotation(charaMove2, Vector3.up);
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 600 * Time.deltaTime);*/
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
