using UnityEngine;

public class characterWarp : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        CharacterController cc = other.GetComponent<CharacterController>();
        if (other.CompareTag("Player"))
        {
            cc.enabled = false;
            player.transform.position = targetPosition;
            cc.enabled = true;
            Debug.Log("飛ぶ");
        }
    }           
}
