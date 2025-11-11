using UnityEngine;

public class forwardjudge : MonoBehaviour
{
    private bool enter = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player") || other.CompareTag("enemy"))
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
