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
    private float rotation_powerX = 26.395f;//この数字はデフォのNeckの角度
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
        //ダッシュ移動してるときMakenoiseを発火させる
        if (move != new Vector2 (0f,0f) && isSprint)
        {
            makenoise?.Makenoise();
        }
        {
            
        }
        //移動系
        //各方向に対して異なる移動速度の代入
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



            //接地時と滞空時の処理
            if (isJump && isGround)
        {
            ypower = jumppower;
        }
        else if (isGround)
        {
            Debug.Log("grounded");
            //接地しているとき，キー入力を受け取り前フレームの変位を入力された値に徐々に変化させる
            Vector3 newinertia = transform.TransformDirection(new Vector3(move.x * speed * 0.01f, 0f, move.y * speed * 0.01f));
            inertia = Vector3.MoveTowards(inertia, newinertia, framepermove * Time.deltaTime);

            gravity.y = 0f;
            ypower = -0.1f;
        }
        else
        {
            ypower = 0f;
        }

        //重力を手動で定義し，キー入力と合成しキャラクターを動かす
        gravity.y += ypower + Physics.gravity.y * Time.deltaTime;
        Vector3 move_dsp = new Vector3(inertia.x, 1.0f * gravity.y * Time.deltaTime, inertia.z);
        Debug.Log(move_dsp);
        Debug.Log(move_dsp);
        character.Move(move_dsp);
    }
    private void LateUpdate()
    {
        //回転系
        //マウスのx入力を合成し，それぞれの回転をさせる
        float rotation_powerY = sensitivity * 0.01f * mouse_input.x;
        transform.Rotate(0, rotation_powerY, 0, Space.Self);
        //今のローカル回転を取得し，上下方向の回転角を制限し，マウスのy入力を合成　もっといい書き方がありそう
        Vector3 rotation = neck.localEulerAngles;
        
        rotation_powerX += sensitivity * 0.01f * -mouse_input.y;

        rotation_powerX = Mathf.Clamp(rotation_powerX, minAngle, maxAngle);

        neck.localRotation = Quaternion.Euler(rotation_powerX, 0, 0);

        fpsCamera.transform.localRotation = Quaternion.Euler(rotation_powerX - 26.395f, 0, 0);
    }

    //InputAction系
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

    //ダメージ処理Enemyタグつけてるオブジェクトに当たったとき発火
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Death");
        }
    }

    private void OnDrawGizmos()
    {
        // 接地判定時は緑、空中にいるときは赤にする
        Gizmos.color = isGround ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up * rayoffset, Vector3.down * grounddetect);
    }
}
