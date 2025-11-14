using System;
using System.Collections;
using UnityEngine;


public class reactiveObject2 : MonoBehaviour
{
    //このスクリプトはドアオブジェクトにくっつけて（自機にplayer、敵にenemyというタグをつけて）
    public int activeID = 0;
    public AudioClip openSound;
    public AudioClip closeSound;
    private AudioSource AudioSource;
    private float openAng;
    private Quaternion closePos;
    private bool keyCheck = false;
    [SerializeField] GameObject forward, back;


    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerStay(Collider other)
    {
        Transform doorshaft = transform.parent.parent;
        closePos = doorshaft.localRotation;


        if (other.CompareTag("Player") && Input.GetKey(KeyCode.F) && keyCheck == false)
        {
            Debug.Log("触ってる");
            inventory inv = other.GetComponent<inventory>();

            if (inv.checkItem(activeID))
            {
                StartCoroutine(DoorOpen());
                StartCoroutine(DoorHit());
                inv.delItem(activeID);
                keyCheck = true;
            }
        }
        else if (((other.CompareTag("Player") && Input.GetKey(KeyCode.F))|| other.CompareTag("Enemy")) && keyCheck == true)
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
            doorbasis.localRotation = Quaternion.Slerp(closePos, openPos, openState);
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

            doorbasis.localRotation = Quaternion.Slerp(openPos, closePos, openState);

            yield return null;
        }
        doorbasis.localRotation = closePos;
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
