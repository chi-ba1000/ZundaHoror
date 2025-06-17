using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class move_test : MonoBehaviour
{
    public float forwardspeed;
    public float movespeed;
    public float sprintspeed;
    public float jumppower;
    public float sensitivity;
    public float framepermove;
    private bool isSprint;
    private bool isJump;
    private float speed;
    private float ypower;
    private Vector2 move;
    private Vector2 mouse_input;
    private Vector3 inertia;
    private Vector3 gravity;
    private CharacterController character;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (move.y > 0f)
        {
            if (isSprint)
            {
                speed = sprintspeed;
            }
            else
            {
                speed = forwardspeed;
            }
        }
        else
        {
            speed = movespeed;
        }



        if (isJump && character.isGrounded)
        {
            ypower = jumppower;
        }
        else if (character.isGrounded)
        {
            Vector3 newinertia = transform.TransformDirection(new Vector3(move.x * speed * 0.01f, 0f, move.y * speed * 0.01f));
            inertia = Vector3.MoveTowards(inertia, newinertia, framepermove * Time.deltaTime);

            gravity.y = 0f;
            ypower = -0.1f;
        }
        else
        {
            ypower = 0f;
        }

        gravity.y += ypower + Physics.gravity.y * Time.deltaTime;
        Vector3 move_dsp = new Vector3(inertia.x, 1.0f * gravity.y * Time.deltaTime, inertia.z);
        character.Move(move_dsp);



        Vector3 rotation = transform.localEulerAngles;
        Debug.Log(rotation);
        float rotation_powerX = 0;
        if (rotation.x >= 310f || rotation.x <= 50f)
        {
            rotation_powerX = sensitivity * 0.01f * -mouse_input.y;
        }
        else if (rotation.x < 310f && rotation.x > 180f && -mouse_input.y > 0f)
        {
            rotation_powerX = sensitivity * 0.01f * -mouse_input.y;
        }
        else if (rotation.x > 50f && rotation.x < 180f && -mouse_input.y < 0f)
        {
            rotation_powerX = sensitivity * 0.01f * -mouse_input.y;
        }


        float rotation_powerY = sensitivity * 0.01f * mouse_input.x;
        transform.Rotate (rotation_powerX, rotation_powerY, 0, Space.Self);
        Vector3 Nowrotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(Nowrotation.x, Nowrotation.y, 0f);

    }
    public void Onmove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
    public void Onsprint(InputAction.CallbackContext context)
    {
        isSprint = context.ReadValueAsButton();
    }
    public void Onjump(InputAction.CallbackContext context)
    {
        isJump = context.ReadValueAsButton();
    }
    public void Onlook(InputAction.CallbackContext context)
    {
        mouse_input = context.ReadValue<Vector2>();
    }


}
