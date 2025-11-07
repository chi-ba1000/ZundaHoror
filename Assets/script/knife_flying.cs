using System.Collections;
using UnityEngine;

public class knife_flying : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 power;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.Rotate(0, 90, 0);
        rb.AddRelativeForce(power, ForceMode.Impulse);
        Destroy(gameObject, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
