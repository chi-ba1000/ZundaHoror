using UnityEngine;

public class pickladder : MonoBehaviour
{
    [SerializeField] private Item itemData;
    [SerializeField] private int activeID;
    [SerializeField] private GameObject ladderPickUp, checkPut, checkPick ,liftUp;
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
            if (Input.GetKeyDown(KeyCode.F))
            {
                inv = other.GetComponent<inventory>();
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Debug.Log("触ってる");

                    if (!inv.checkItem(activeID))
                    {
                        inv.itemPick(itemData);
                        ladderPickUp.SetActive(false);
                        checkPut.SetActive(true);
                        checkPick.SetActive(false);
                        liftUp.SetActive(false);
                        Debug.Log("もつ");
                    }
                }
            }
        }
    }
}

