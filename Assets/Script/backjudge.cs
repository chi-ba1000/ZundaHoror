using UnityEngine;

public class backjudge : MonoBehaviour
{
    private bool enter = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player") || other.CompareTag("enemy"))
        {
            enter = true;
        }
    }

    public bool backenter()
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
