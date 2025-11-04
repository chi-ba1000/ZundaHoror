using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;

public class knifethrower : MonoBehaviour
{
    private Transform player;
    private Vector3 direction;
    private Animator anim;
    private bool throwtrigger;
    public Vector3 hight;
    public GameObject knife;
    public float cooltime;
    public float motion;
    public float distance;
    public Transform spawnpoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = gameObject.GetComponent<Animator>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = transform.position + new Vector3(0, hight.y, 0);
        direction = transform.forward;
        Vector3 targetPoint = origin + direction * distance;
        Debug.DrawRay(origin, direction * distance, Color.red);

        direction = targetPoint - transform.position + new Vector3(0, hight.y, 0);
    }

    public void Throw()
    {
        StartCoroutine(Throwable());        
    }
    private IEnumerator Throwable()
    {
        if (throwtrigger == false)
        {
            throwtrigger = true;
            yield return new WaitForSeconds(motion);
            Instantiate(knife, spawnpoint.position + new Vector3(0, hight.y, 0), Quaternion.LookRotation(direction));
            Debug.Log("respawn");
            Debug.Log(this.transform.position);
            yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"));
            throwtrigger = false;
            
        }
    }
}
