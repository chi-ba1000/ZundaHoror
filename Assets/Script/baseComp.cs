using UnityEngine;
using System.Collections.Generic;

public class baseComp : MonoBehaviour
{
    [SerializeField] private appearKeyVer2 allSetCheck;
    [SerializeField] KeyCode[] keyCode = new KeyCode[5];
    [SerializeField] private GameObject item1, item2, item3, item4, item5;
    [SerializeField] private string item1Name = "item1", item2Name = "item2", item3Name = "item3", item4Name = "item4", item5Name = "item5";
    private int[] activeID = new int[5];
    private bool item1B = false, item2B = false, item3B = false, item4B = false, item5B = false;
    public int item1Id = 4, item2Id = 5, item3Id = 6, item4Id = 7, item5Id = 8;
    Item item1I, item2I, item3I, item4I, item5I;
    [HideInInspector] public bool put = false, item1C = false, item2C = false, item3C = false, item4C = false, item5C = false;
    inventory inv;
    [HideInInspector]
    //列挙enumは指定しない限り上から0,1,2...と割り当てられる
    public enum whatItem
    {
        Item1 ,
        Item2 ,
        Item3 ,
        Item4 ,
        Item5
    }
    [SerializeField] private whatItem choseItem;
    private Dictionary<whatItem, int> itemIdSet;
    private int setItem;
    void Awake()
    {
        //()はコンストラクタ設定時に必要空白だとからのインスタンス作成、()の中身を入れると中身の設計図に応じたインスタンスを作成
        itemIdSet = new Dictionary<whatItem, int>();

        itemIdSet[whatItem.Item1] = item1Id;
        itemIdSet[whatItem.Item2] = item2Id;
        itemIdSet[whatItem.Item3] = item3Id;
        itemIdSet[whatItem.Item4] = item4Id;
        itemIdSet[whatItem.Item5] = item5Id;

        setItem = itemIdSet[choseItem];



        activeID[0] = item1Id;
        activeID[1] = item2Id;
        activeID[2] = item3Id;
        activeID[3] = item4Id;
        activeID[4] = item5Id;


        /*itemIdSet[color.Item1]の中身は0ではだめ、なぜenumの中ではItem1=0, Item2=1, Item3=2... 
        と扱われているはずなのに0やwhatItem.0ダメなのか
        →　enum のメンバーには名前（識別子）と値（内部的な数値）の2つの側面がある
        whatItem.Item1　はグループに属する Black という名前のメンバーをしてい（名前は数字から始められない）
        whatItem.0　は値そのものを呼び出そうとしている。しかしC#では0を名前として認識しようとするそのため使えない
        */
        Debug.Log("黒初期: " + itemIdSet[whatItem.Item1]);
        Debug.Log("青初期: " + itemIdSet[whatItem.Item2]);
        Debug.Log("緑初期: " + itemIdSet[whatItem.Item3]);
        Debug.Log("赤初期: " + itemIdSet[whatItem.Item4]);
        Debug.Log("白初期: " + itemIdSet[whatItem.Item5]);
    }
    void Start()
    {
        item1I = new Item(item1Name, item1Id); item2I = new Item(item2Name, item2Id);
        item3I = new Item(item3Name, item3Id); item4I = new Item(item4Name, item4Id); 
        item5I = new Item(item5Name, item5Id);
        allCrear();

        Debug.Log("指定色: " + setItem);
    }
    private void OnTriggerStay(Collider other)
    {
        for (int i = 0; i <= 4; i++)
        {
            if (other.CompareTag("player") && Input.GetKey(keyCode[i]) && put ==false)
            {
                Debug.Log("触ってる");
                inv = other.GetComponent<inventory>();

                if (inv.checkItem(activeID[i]))
                {
                    //以下三つは消してもいいけど念のため
                    allCrear();
                    setFalse();
                    setFalsePriv();
                    if (activeID[i] == item1Id)
                    {
                        item1.SetActive(true);
                        item1B = true;
                        put = true;
                    }
                    else if (activeID[i] == item2Id)
                    {
                        item2.SetActive(true);
                        item2B = true;
                        put = true;
                    }
                    else if (activeID[i] == item3Id)
                    {
                        item3.SetActive(true);
                        item3B = true;
                        put = true;
                    }
                    else if (activeID[i] == item4Id)
                    {
                        item4.SetActive(true);
                        item4B = true;
                        put = true;
                    }
                    else if (activeID[i] == item5Id)
                    {
                        item5.SetActive(true);
                        item5B = true;
                        put = true;
                    }
                    inv.delItem(activeID[i]);

                    if (activeID[i] == setItem && setItem == item1Id)
                    {
                        item1C = true;
                    }
                    else if (activeID[i] == setItem && setItem == item2Id)
                    {
                        item2C = true;
                    }
                    else if (activeID[i] == setItem && setItem == item3Id)
                    {
                        item3C = true;
                    }
                    else if (activeID[i] == setItem && setItem == item4Id)
                    {
                        item4C = true;
                    }
                    else if (activeID[i] == setItem && setItem == item5Id)
                    {
                        item5C = true;
                    }
                    Debug.Log("黒判定: " + item1C);
                    Debug.Log("青判定: " + item2C);
                    Debug.Log("緑判定: " + item3C);
                    Debug.Log("赤判定: " + item4C);
                    Debug.Log("白判定: " + item5C);
                }
            }
        }
        if (allSetCheck.keyC == false && other.CompareTag("player") && Input.GetKey(KeyCode.F))
        {
            inv = other.GetComponent<inventory>();
            if (item1B)
            {
                inv.itemPick(item1I);
            }
            else if (item2B)
            {
                inv.itemPick(item2I);
            }
            else if (item3B)
            {
                inv.itemPick(item3I);
            }
            else if (item4B)
            {
                inv.itemPick(item4I);
            }
            else if (item5B)
            {
                inv.itemPick(item5I);
            }
            setFalse();
            allCrear();
            setFalsePriv();
            put = false;
        }
    }
    private void allCrear()
    {
        item1.SetActive(false);
        item2.SetActive(false);
        item3.SetActive(false);
        item4.SetActive(false);
        item5.SetActive(false);
    }
    private void setFalse()
    {
        item1B = false;
        item2B = false;
        item3B = false;
        item4B = false;
        item5B = false;
    }
    private void setFalsePriv()
    {
        item1C = false;
        item2C = false;
        item3C = false;
        item4C = false;
        item5C = false;
    }
}
