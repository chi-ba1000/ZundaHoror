using UnityEngine;

public class itemMarge : MonoBehaviour
{
    [SerializeField] private GameObject player ,Item1 ,Item2;
    [SerializeField] private int activeItem1 = 0, activeItem2 = 0;
    [SerializeField] private Item itemData;
    private inventory inv;
    private pickedItem item1C, item2C;
    private Item item1, item2;
    void Start()
    {
        inv = player.GetComponent<inventory>();
        item1C = Item1.GetComponent<pickedItem>();
        item2C = Item2.GetComponent<pickedItem>();
        item1 = item1C.itemData;
        item2 = item2C.itemData;
    }
    void Update()
    {
        if(inv.checkItem(activeItem1) && inv.checkItem(activeItem2))
        {
            if (item1.Id == activeItem1 && item2.Id == activeItem2)
            {
                inv.itemPick(itemData);
                inv.delItem(item1.Id);
                inv.delItem(item2.Id);
            }
        }
    }
}
