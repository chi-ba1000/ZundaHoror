using UnityEngine;

public class hantei_test : MonoBehaviour
{
    public GameObject target;
    public Gimmick gimmick;
    public Trigger trigger;
    private Rigidbody rb;
    private MeshRenderer rbMesh;
    private int switchcount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target.TryGetComponent<Rigidbody>(out rb);
        target.TryGetComponent<MeshRenderer>(out rbMesh);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        switchcount++;
        Debug.Log("îﬁÇÕÉgÉäÉKÅ[Ç≈Ç∑");
        if (other.CompareTag("Player"))
        {
            if (gimmick == Gimmick.fall)
            {
                rb.useGravity = true;
            }
            else if (gimmick == Gimmick.transparent && trigger == Trigger.whilePress)
            {
                rbMesh.enabled = false;
            }
            else if (gimmick == Gimmick.transparent && trigger == Trigger.bottun)
            {
                if(switchcount == 1)
                {
                    rbMesh.enabled = false;
                }
                else if (switchcount == 2)
                {
                    rbMesh.enabled = true;
                    switchcount = 0;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && trigger != Trigger.bottun)
        {
            rbMesh.enabled = true;
        }
    }
    public enum Gimmick
    {
        fall,key,transparent,destroy
    }
    public enum Trigger
    {
        bottun, whilePress
    }
}
