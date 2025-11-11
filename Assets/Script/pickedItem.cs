using UnityEngine;

public class pickedItem : MonoBehaviour
{
    //このスクリプトは鍵などの拾われるアイテムにアタッチして
    public Item itemData;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("触ってる");
            inventory inv = other.GetComponent<inventory>();

            inv.itemPick(itemData);
            Destroy(gameObject);
        }
    }
}
