using UnityEngine;

public class mirror : MonoBehaviour
{
    [SerializeField] private int activeID = 0;
    [SerializeField] GameObject beforeMirror, afterMirror, Towel;
    private Item towel;
    private pickedItem towelC;
    void Start()
    {
        afterMirror.SetActive(false);
        towelC = Towel.GetComponent<pickedItem>();
        towel = towelC.itemData;
    }
    private void OnTriggerStay(Collider other)
    {
        inventory inv = other.GetComponent<inventory>();
        if (other.CompareTag("player") && inv.checkItem(activeID) && towel.Id == activeID && Input.GetKey(KeyCode.F))
        {
            beforeMirror.SetActive(false);
            afterMirror.SetActive(true);
        }
    }
}
