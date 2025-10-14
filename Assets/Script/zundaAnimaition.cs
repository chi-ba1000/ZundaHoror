using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class zundaAnimaition : MonoBehaviour
{
    private Vector2 charaMove = Vector2.zero;
    private Animator animator;
    private CharacterController charaCon;

    void Start()
    {
        animator = GetComponent<Animator>();
        charaCon = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (charaCon.isGrounded)
        {
            animator.SetBool("jump", false);
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                resetwalkrun();
                switch (charaMove.x, charaMove.y)
                {
                    case (0, 1):
                        animator.SetBool("walkForward", true);
                        break;
                    case (0, -1):
                        animator.SetBool("walkBack", true);
                        break;
                    case (-1, 0):
                        animator.SetBool("walkLeft", true);
                        break;
                    case (1, 0):
                        animator.SetBool("walkRight", true);
                        break;
                    case (-1, 1):
                        animator.SetBool("walkForwardLeft", true);
                        break;
                    case (1, 1):
                        animator.SetBool("walkForwardRight", true);
                        break;
                    case (-1, -1):
                        animator.SetBool("walkBackwardLeft", true);
                        break;
                    case (1, -1):
                        animator.SetBool("walkBackwardRight", true);
                        break;
                    case (0, 0):
                        resetwalkrun();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                resetwalkrun();
                switch (charaMove.x, charaMove.y)
                {
                    case (0, 1):
                        animator.SetBool("runForward", true);
                        break;
                    case (0, -1):
                        animator.SetBool("runBack", true);
                        break;
                    case (-1, 0):
                        animator.SetBool("runLeft", true);
                        break;
                    case (1, 0):
                        animator.SetBool("runRight", true);
                        break;
                    case (-1, 1):
                        animator.SetBool("runForwardLeft", true);
                        break;
                    case (1, 1):
                        animator.SetBool("runForwardRight", true);
                        break;
                    case (-1, -1):
                        animator.SetBool("runBackwardLeft", true);
                        break;
                    case (1, -1):
                        animator.SetBool("runBackwardRight", true);
                        break;
                    case (0, 0):
                        resetwalkrun();
                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            resetwalkrun();
        }
    }
    private void resetwalkrun()
    {
        animator.SetBool("walkForward", false);
        animator.SetBool("walkBack", false);
        animator.SetBool("walkLeft", false);
        animator.SetBool("walkRight", false);
        animator.SetBool("walkForwardRight", false);
        animator.SetBool("walkForwardLeft", false);
        animator.SetBool("walkBackwardLeft", false);
        animator.SetBool("walkBackwardRight", false);
        animator.SetBool("runForward", false);
        animator.SetBool("runBack", false);
        animator.SetBool("runLeft", false);
        animator.SetBool("runRight", false);
        animator.SetBool("runForwardRight", false);
        animator.SetBool("runForwardLeft", false);
        animator.SetBool("runBackwardLeft", false);
        animator.SetBool("runBackwardRight", false);
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        charaMove = context.ReadValue<Vector2>();
        charaMove = new Vector2(Mathf.Round(charaMove.x), Mathf.Round(charaMove.y));
    }

    
    public void Onjump(InputAction.CallbackContext context)
    {
        if (charaCon.isGrounded && context.phase == InputActionPhase.Performed)
        {
            animator.SetBool("jump", true);
        }
    }

}
