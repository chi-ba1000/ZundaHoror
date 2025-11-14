using System;
using System.Collections;
using UnityEngine;


public class reactiveObjectDoor : MonoBehaviour
{
    //このスクリプトはドアオブジェクトにくっつけて（自機にplayer、敵にenemyというタグをつけて）
    public AudioClip openSound;
    public AudioClip closeSound;
    private AudioSource AudioSource;
    private float openAng;
    private Quaternion closePos;
    [SerializeField] GameObject forward, back;


    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        Transform doorshaft = transform.parent.parent;
        closePos = doorshaft.rotation;
    }
    private void OnTriggerStay(Collider other)
    {
        if (((other.CompareTag("Player") && Input.GetKey(KeyCode.F))|| other.CompareTag("Enemy")))
        {
            StartCoroutine(DoorOpen());
            StartCoroutine(DoorHit());
        }
    }
    private IEnumerator DoorOpen()
    {
        Debug.Log("開く");
        Debug.Log(closePos);
        Transform doorbasis = transform.parent.parent.parent;
        float openTime = 0.5f;
        float waitTime = 0f;


        forwardjudge fJudge = forward.GetComponent<forwardjudge>();
        backjudge bJudge = back.GetComponent<backjudge>();
        if (fJudge.forwardenter())
        {
            openAng = -130f;
        }
        else if (bJudge.backenter())
        {
            openAng = 130f;
        }


        Quaternion openPos = closePos * Quaternion.Euler(0, openAng, 0);

        while (waitTime < openTime)
        {
            waitTime += Time.deltaTime;
            float openState = waitTime / openTime;
            
            //quaternion.slerp – 球面線形補間、一つ目の要素と二つ目の要素の間を３つ目の要素の状態で移動
            doorbasis.rotation = Quaternion.Slerp(closePos, openPos, openState);
            if (!AudioSource.isPlaying)
            {
                AudioSource.PlayOneShot(openSound);
            }

            yield return null;
        }
        yield return new WaitForSeconds(1f);
        waitTime = 0.0f;
        while (waitTime < openTime)
        {
            waitTime += Time.deltaTime;
            float openState = waitTime / openTime;

            doorbasis.rotation = Quaternion.Slerp(openPos, closePos, openState);

            yield return null;
        }
        doorbasis.rotation = closePos;
        if (!AudioSource.isPlaying)
        {
            AudioSource.PlayOneShot(closeSound);
        }
    }


    private IEnumerator DoorHit()
    {
        GameObject doorslide = transform.parent.gameObject;
        foreach (Transform children in doorslide.transform)
        {
            Collider col = children.GetComponent<Collider>();
            col.enabled = false;
        }
        yield return new WaitForSeconds(2.2f);
        foreach (Transform children in doorslide.transform)
        {
            Collider col = children.GetComponent<Collider>();
            col.enabled = true;
        }
        yield return null;
    }
}
