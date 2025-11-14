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

    private bool isSprint;
    private bool isJump;
    private bool isGround;
    private float speed;
    private float ypower;
    private float rotation_powerX = 26.395f;//���̐����̓f�t�H��Neck�̊p�x
    private Vector2 move;
    private Vector2 mouse_input;
    private Vector3 inertia;
    private Vector3 gravity;
    private Vector3 neckrotation;
    private CharacterController character;

    void Start()
    {
        character = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        //�_�b�V���ړ����Ă�Ƃ�Makenoise�𔭉΂�����
        if (move != new Vector2 (0f,0f) && isSprint)
        {
            makenoise?.Makenoise();
        }
        {
            
        }
        //�ړ��n
        //�e�����ɑ΂��ĈقȂ�ړ����x�̑��
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
            isSprint = false;
        }



            //�ڒn���Ƒ؋󎞂̏���
            if (isJump && isGround)
        {
            ypower = jumppower;
        }
        else if (isGround)
        {
            Debug.Log("grounded");
            //�ڒn���Ă���Ƃ��C�L�[���͂��󂯎��O�t���[���̕ψʂ���͂��ꂽ�l�ɏ��X�ɕω�������
            Vector3 newinertia = transform.TransformDirection(new Vector3(move.x * speed * 0.01f, 0f, move.y * speed * 0.01f));
            inertia = Vector3.MoveTowards(inertia, newinertia, framepermove * Time.deltaTime);

            gravity.y = 0f;
            ypower = -0.1f;
        }
        else
        {
            ypower = 0f;
        }

        //�d�͂��蓮�Œ�`���C�L�[���͂ƍ������L�����N�^�[�𓮂���
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
        //��]�n
        //�}�E�X��x���͂��������C���ꂼ��̉�]��������
        float rotation_powerY = sensitivity * 0.01f * mouse_input.x;
        transform.Rotate(0, rotation_powerY, 0, Space.Self);
        //���̃��[�J����]���擾���C�㉺�����̉�]�p�𐧌����C�}�E�X��y���͂������@�����Ƃ��������������肻��
        Vector3 rotation = neck.localEulerAngles;
        
        rotation_powerX += sensitivity * 0.01f * -mouse_input.y;

        rotation_powerX = Mathf.Clamp(rotation_powerX, minAngle, maxAngle);

        neck.localRotation = Quaternion.Euler(rotation_powerX, 0, 0);

        fpsCamera.transform.localRotation = Quaternion.Euler(rotation_powerX - 26.395f, 0, 0);
    }

    //InputAction�n
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
        if (context.performed)
        {
            isJump = true;
        }
        else
        {
            isJump = false;
        }
    }
    public void Onlook(InputAction.CallbackContext context)
    {
        mouse_input = context.ReadValue<Vector2>();
    }

    //�_���[�W����Enemy�^�O���Ă�I�u�W�F�N�g�ɓ��������Ƃ�����
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Death");
        }
    }

    private void OnDrawGizmos()
    {
        // �ڒn���莞�͗΁A�󒆂ɂ���Ƃ��͐Ԃɂ���
        Gizmos.color = isGround ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up * rayoffset, Vector3.down * grounddetect);
    }
}
