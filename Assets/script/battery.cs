using UnityEngine;

public class battery : MonoBehaviour
{
    public spotlight spotlight;
    public bool canReuse;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spotlight.Getbattery();
            if (!canReuse)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
