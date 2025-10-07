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
    public GameObject fpsCamera;
    public GameObject camera1;
    public GameObject camera2;

    private bool isSprint;
    private bool isJump;
    private int Cameranum;
    private float speed;
    private float ypower;
    private Vector2 move;
    private Vector2 mouse_input;
    private Vector3 inertia;
    private Vector3 gravity;
    private CharacterController character;

    void Start()
    {
        character = GetComponent<CharacterController>();
        fpsCamera.SetActive(true);
        camera1.SetActive(false);
        camera2.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
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


        //接地時と滞空時の処理
        if (isJump && character.isGrounded)
        {
            ypower = jumppower;
        }
        else if (character.isGrounded)
        {
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
        character.Move(move_dsp);


        //回転系
        //今のローカル回転を取得し，上下方向の回転角を制限し，マウスのy入力を合成　もっといい書き方がありそう
        Vector3 rotation = transform.localEulerAngles;
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

        //マウスのx入力を合成し，それぞれの回転をさせる
        float rotation_powerY = sensitivity * 0.01f * mouse_input.x;
        transform.Rotate(rotation_powerX, rotation_powerY, 0, Space.Self);
        //z軸周りの回転を固定しないときもいので今の回転角を取得し，z軸周りだけ0にする
        Vector3 Nowrotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(Nowrotation.x, Nowrotation.y, 0f);

        //カメラ系
        fpsCamera.SetActive(false);
        camera1.SetActive(false);
        camera2.SetActive(false);
        if (Cameranum == 0)
        {
            fpsCamera.SetActive(true);
        }
        if (Cameranum == 1)
        {
            camera1.SetActive(true);
        }
        if (Cameranum == 2)
        {
            camera2.SetActive(true);
        }
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
        isJump = context.ReadValueAsButton();
    }
    public void Onlook(InputAction.CallbackContext context)
    {
        mouse_input = context.ReadValue<Vector2>();
    }
    public void OnChangeCamera(InputAction.CallbackContext context)
    {
        //押された1フレームのみ呼び出す
        if (context.performed)
        {
            Cameranum = (Cameranum + 1) % 3;
            Debug.Log(Cameranum);
        }
    }

}
