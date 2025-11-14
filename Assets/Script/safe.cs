using UnityEngine;
using System.Collections;

public class safe : MonoBehaviour
{
    [SerializeField] private string password = "1234";
    [SerializeField] private SafeUIController uiController;
    [SerializeField] private GameObject door;
    private GameObject playerObject;
    private Quaternion closePos;
    private bool farst = true;
    [SerializeField] private Item itemData;
    private inventory inv;
    [SerializeField] GameObject player;
    private void OnTriggerStay(Collider other)
    {
        if (farst)
        {
            inv = player.GetComponent<inventory>();
            Debug.Log("金庫1");
            playerObject = other.gameObject;
            if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("金庫2");

                // UIコントローラーに「UIを開け」と命令する
                // 自分自身 "this" と、操作をロックしたい "playerObject" を渡す
                uiController.OpenPanel(this, playerObject);
            }
        }
    }



    public void CheckPassword(string input)
    {
        if (input == password)
        {
            StartCoroutine("DoorOpen");
            farst = false;
            inv.itemPick(itemData);
        }
        else
        {
            Debug.Log("不正解");

        }
    }

    private IEnumerator DoorOpen()
    {
        Transform doorbasis = door.transform;
        closePos = doorbasis.rotation;
        float openTime = 0.5f;
        float waitTime = 0f;


        float openAng = -120f;


        Quaternion openPos = closePos * Quaternion.Euler(0, openAng, 0);

        while (waitTime < openTime)
        {
            waitTime += Time.deltaTime;
            float openState = waitTime / openTime;

            //quaternion.slerp – 球面線形補間、一つ目の要素と二つ目の要素の間を３つ目の要素の状態で移動
            doorbasis.rotation = Quaternion.Slerp(closePos, openPos, openState);

            yield return null;
        }
    }
}
