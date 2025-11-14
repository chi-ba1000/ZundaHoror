using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float forwardspeed;
    public float movespeed;
    public float sprintspeed;
    public float jumppower;
    public float sensitivity;
    public float framepermove;
    public float minAngle;
    public float maxAngle;
    public float rayoffset;
    public float grounddetect;
    public Transform neck;
    public GameObject fpsCamera;
    public EnemyController  makenoise;
    public AudioClip walkSound;

    private bool isSprint;
    private bool isJump;
    private bool isGround;
    private float speed;
    private float ypower;
    private float rotation_powerX = 26.395f;//ï¿½ï¿½ï¿½Ìï¿½ï¿½ï¿½ï¿½Íƒfï¿½tï¿½Hï¿½ï¿½Neckï¿½ÌŠpï¿½x
    private Vector2 move;
    private Vector2 mouse_input;
    private Vector3 inertia;
    private Vector3 gravity;
    private Vector3 neckrotation;
    private CharacterController character;
    private AudioSource AudioSource;
    private bool isWalk;

    void Start()
    {
        character = GetComponent<CharacterController>();
        AudioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        //ï¿½_ï¿½bï¿½Vï¿½ï¿½ï¿½Ú“ï¿½ï¿½ï¿½ï¿½Ä‚ï¿½Æ‚ï¿½Makenoiseï¿½ğ”­‰Î‚ï¿½ï¿½ï¿½ï¿½ï¿½
        if (move != new Vector2 (0f,0f) && isSprint)
=======
        //ƒ_ƒbƒVƒ…ˆÚ“®‚µ‚Ä‚é‚Æ‚«Makenoise‚ğ”­‰Î‚³‚¹‚é
        if (move != new Vector2(0f, 0f))
>>>>>>> d678f1b5cd85b24717f647705a1711dcb3c6d05b
        {
            if (isSprint)
            {
                makenoise?.Makenoise();
            }
            if (!isWalk)
            {
                StartCoroutine(Sounds());
            }
            isWalk = true;

        }
        else
        {
            isWalk = false;
        }
<<<<<<< HEAD
        //ï¿½Ú“ï¿½ï¿½n
        //ï¿½eï¿½ï¿½ï¿½ï¿½ï¿½É‘Î‚ï¿½ï¿½ÄˆÙ‚È‚ï¿½Ú“ï¿½ï¿½ï¿½ï¿½xï¿½Ì‘ï¿½ï¿½
=======

        //ˆÚ“®Œn
        //Še•ûŒü‚É‘Î‚µ‚ÄˆÙ‚È‚éˆÚ“®‘¬“x‚Ì‘ã“ü
>>>>>>> d678f1b5cd85b24717f647705a1711dcb3c6d05b
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

        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * rayoffset, Vector3.down, out hit, grounddetect))
        {
            isGround = true;
        }
        else
        {
            isGround = false;
            isJump = false;
        }



<<<<<<< HEAD
            //ï¿½Ú’nï¿½ï¿½ï¿½Æ‘Ø‹ó‚Ìï¿½ï¿½ï¿½
            if (isJump && isGround)
=======
            //Ú’n‚Æ‘Ø‹ó‚Ìˆ—
        if (isJump && isGround)
>>>>>>> d678f1b5cd85b24717f647705a1711dcb3c6d05b
        {
            Debug.Log("jump");
            ypower = jumppower;
        }
        else if (isGround)
        {
            Debug.Log("grounded");
            //ï¿½Ú’nï¿½ï¿½ï¿½Ä‚ï¿½ï¿½ï¿½Æ‚ï¿½ï¿½Cï¿½Lï¿½[ï¿½ï¿½ï¿½Í‚ï¿½ï¿½ó‚¯ï¿½ï¿½Oï¿½tï¿½ï¿½ï¿½[ï¿½ï¿½ï¿½Ì•ÏˆÊ‚ï¿½ï¿½ï¿½Í‚ï¿½ï¿½ê‚½ï¿½lï¿½Éï¿½ï¿½Xï¿½É•Ï‰ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½
            Vector3 newinertia = transform.TransformDirection(new Vector3(move.x * speed * 0.01f, 0f, move.y * speed * 0.01f));
            inertia = Vector3.MoveTowards(inertia, newinertia, framepermove * Time.deltaTime);

            gravity.y = 0f;
            ypower = -0.1f;
        }
        else
        {
            ypower = 0f;
        }

        //ï¿½dï¿½Í‚ï¿½ï¿½è“®ï¿½Å’ï¿½`ï¿½ï¿½ï¿½Cï¿½Lï¿½[ï¿½ï¿½ï¿½Í‚Æï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Lï¿½ï¿½ï¿½ï¿½ï¿½Nï¿½^ï¿½[ï¿½ğ“®‚ï¿½ï¿½ï¿½
        gravity.y += ypower + Physics.gravity.y * Time.deltaTime;
        Vector3 move_dsp = new Vector3(inertia.x, 1.0f * gravity.y * Time.deltaTime, inertia.z);
        Debug.Log(move_dsp);
        Debug.Log(move_dsp);
        if (GetComponent<CharacterController>() != null)
        {
            character.Move(move_dsp);
        }
    }
    private void LateUpdate()
    {
        //ï¿½ï¿½]ï¿½n
        //ï¿½}ï¿½Eï¿½Xï¿½ï¿½xï¿½ï¿½ï¿½Í‚ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Cï¿½ï¿½ï¿½ê‚¼ï¿½ï¿½Ì‰ï¿½]ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½
        float rotation_powerY = sensitivity * 0.01f * mouse_input.x;
        transform.Rotate(0, rotation_powerY, 0, Space.Self);
        //ï¿½ï¿½ï¿½Ìƒï¿½ï¿½[ï¿½Jï¿½ï¿½ï¿½ï¿½]ï¿½ï¿½ï¿½æ“¾ï¿½ï¿½ï¿½Cï¿½ã‰ºï¿½ï¿½ï¿½ï¿½ï¿½Ì‰ï¿½]ï¿½pï¿½ğ§Œï¿½ï¿½ï¿½ï¿½Cï¿½}ï¿½Eï¿½Xï¿½ï¿½yï¿½ï¿½ï¿½Í‚ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½@ï¿½ï¿½ï¿½ï¿½ï¿½Æ‚ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½è‚»ï¿½ï¿½
        Vector3 rotation = neck.localEulerAngles;
        
        rotation_powerX += sensitivity * 0.01f * -mouse_input.y;

        rotation_powerX = Mathf.Clamp(rotation_powerX, minAngle, maxAngle);

        neck.localRotation = Quaternion.Euler(rotation_powerX, 0, 0);

        fpsCamera.transform.localRotation = Quaternion.Euler(rotation_powerX - 26.395f, 0, 0);
    }

    //InputActionï¿½n
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
        if (context.performed && !isJump == true)
        {
            isJump = true;
        }
    }
    public void Onlook(InputAction.CallbackContext context)
    {
        mouse_input = context.ReadValue<Vector2>();
    }

    //ï¿½_ï¿½ï¿½ï¿½[ï¿½Wï¿½ï¿½ï¿½ï¿½Enemyï¿½^ï¿½Oï¿½Â‚ï¿½ï¿½Ä‚ï¿½Iï¿½uï¿½Wï¿½Fï¿½Nï¿½gï¿½É“ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Æ‚ï¿½ï¿½ï¿½ï¿½ï¿½
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Death");
        }
    }

    private void OnDrawGizmos()
    {
        // ï¿½Ú’nï¿½ï¿½ï¿½èï¿½Í—ÎAï¿½ó’†‚É‚ï¿½ï¿½ï¿½Æ‚ï¿½ï¿½ÍÔ‚É‚ï¿½ï¿½ï¿½
        Gizmos.color = isGround ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up * rayoffset, Vector3.down * grounddetect);
    }

    private IEnumerator Sounds()
    {
        Debug.Log("sound");
        if (isSprint)
        {
            AudioSource.pitch = 2.0f;
            AudioSource.PlayOneShot(walkSound);
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            AudioSource.pitch = 1.0f;
            AudioSource.PlayOneShot(walkSound);
            yield return new WaitForSeconds(1.0f);
        }

        isWalk = false;
    }
}
