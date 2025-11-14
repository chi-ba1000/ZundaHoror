using UnityEngine;

public class putladder : MonoBehaviour
{
    [SerializeField] private Item itemData;
    [SerializeField] private int activeID;
    [SerializeField] private GameObject ladderPickUp ,checkPut ,checkPick ,liftUp;
    [SerializeField] private bool setappear = true;
    private inventory inv;


    void Start()
    {
        ladderPickUp.SetActive(setappear);
        checkPick.SetActive(setappear);
        checkPut.SetActive(!setappear);
        liftUp.SetActive(setappear);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("player"))
        {
            inv = other.GetComponent<inventory>();
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("触ってる");

                if (inv.checkItem(activeID))
                {
                    inv.delItem(activeID);
                    ladderPickUp.SetActive(true);
                    checkPut.SetActive(false);
                    checkPick.SetActive(true);
                    liftUp.SetActive(true);
                    Debug.Log("もつ");
                }
            }
        }
    }
}
