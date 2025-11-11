using UnityEngine;

public class forwardjudge : MonoBehaviour
{
    private bool enter = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            enter = true;
        }
    }

    public bool forwardenter()
    {
        if (enter)
        {
            enter = false; 
            return true;
        }
        else
        {
            return false;
        }   
    }
}
