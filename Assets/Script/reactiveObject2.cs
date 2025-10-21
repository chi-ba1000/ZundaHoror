using System.Collections;
using UnityEngine;

public class reactiveObject2 : MonoBehaviour
{
    //このスクリプトは却下
    public int activeID = 0;
    public float openDistance = 1f;
    private Vector3 closePos, openPos;
    private bool firstEnter = true;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            Transform doorbasis = transform.parent.parent;
            closePos = doorbasis.position;
            if(firstEnter)
            {
                openPos = closePos + new Vector3(openDistance, 0, 0);
                firstEnter = false;
            }
            


            Debug.Log("触ってる");
            inventory inv = other.GetComponent<inventory>();

            if (inv.checkItem(activeID))
            {
                StartCoroutine(DoorOpen());
                //StartCoroutine(DoorHit());
            }
        }
    }
    private IEnumerator DoorOpen()
    {
        Debug.Log("開く");
        Debug.Log(closePos);
        Transform doorbasis = transform.parent.parent;
        //Transform doorbasis = transform.Find("doorbasis");
        float openTime = 0.5f;
        float waitTime = 0.0f;

        while (waitTime < openTime)
        {
            waitTime += Time.deltaTime;
            float openState = waitTime / openTime;

            //Lerpは線形補完、一つ目の要素と二つ目の要素の間を３つ目の要素の状態で移動
            doorbasis.position = Vector3.Lerp(closePos, openPos, openState);

            yield return null;
        }
        yield return new WaitForSeconds(1f);
        waitTime = 0.0f;
        while (waitTime < openTime)
        {
            waitTime += Time.deltaTime;
            float openState = waitTime / openTime;

            doorbasis.position = Vector3.Lerp(openPos, closePos, openState);

            yield return null;
        }
        doorbasis.position = closePos;
    }

    /*
    private IEnumerator DoorHit()
    {
        GameObject doorslide = transform.parent.gameObject;
        foreach (Transform children in doorslide.transform)
        {
            Collider col = children.GetComponent<Collider>();
            col.enabled = false;
        }
        yield return new WaitForSeconds(2.1f);
        foreach (Transform children in doorslide.transform)
        {
            Collider col = children.GetComponent<Collider>();
            col.enabled = true;
        }
        yield return null;
    }
    */
}
