using UnityEngine;
using System.Collections.Generic;

public class daiza : MonoBehaviour
{
    [SerializeField] private appearKey allSetCheck;
    [SerializeField] KeyCode[] keyCode = new KeyCode[5];
    private int[] activeID = new int[5];
    private Transform black, blue, green, red, white;
    private bool blackB = false, blueB = false, greenB = false, redB = false, whiteB = false;
    public int blackId = 4, blueId = 5, greenId = 6, redId = 7, whiteId = 8;
    Item blackI, blueI, greenI, redI, whiteI;
    [HideInInspector] public bool put = false, blackC = false, blueC = false, greenC = false, redC = false, whiteC = false;
    inventory inv;
    [HideInInspector]

    //列挙enumは指定しない限り上から0,1,2...と割り当てられる
    public enum color
    {
        Black ,
        Blue ,
        Green ,
        Red ,
        White
    }
    [SerializeField] private color choseColor;
    private Dictionary<color, int> colorIdSet;
    private int setColor;
    void Awake()
    {
        //()はコンストラクタ設定時に必要空白だとからのインスタンス作成、()の中身を入れると中身の設計図に応じたインスタンスを作成
        colorIdSet = new Dictionary<color, int>();

        colorIdSet[color.Black] = blackId;
        colorIdSet[color.Blue] = blueId;
        colorIdSet[color.Green] = greenId;
        colorIdSet[color.Red] = redId;
        colorIdSet[color.White] = whiteId;

        setColor = colorIdSet[choseColor];



        activeID[0] = blackId;
        activeID[1] = blueId;
        activeID[2] = greenId;
        activeID[3] = redId;
        activeID[4] = whiteId;


        /*colorIdSet[color.Black]の中身は0ではだめ、なぜenumの中ではBlack=0, Blue=1, Green=2... 
        と扱われているはずなのに0やcolor.0ダメなのか
        →　enum のメンバーには名前（識別子）と値（内部的な数値）の2つの側面がある
        color.Black　はグループに属する Black という名前のメンバーをしてい（名前は数字から始められない）
        color.0　は値そのものを呼び出そうとしている。しかしC#では0を名前として認識しようとするそのため使えない
        */
        Debug.Log("黒初期: " + colorIdSet[color.Black]);
        Debug.Log("青初期: " + colorIdSet[color.Blue]);
        Debug.Log("緑初期: " + colorIdSet[color.Green]);
        Debug.Log("赤初期: " + colorIdSet[color.Red]);
        Debug.Log("白初期: " + colorIdSet[color.White]);
    }
    void Start()
    {
        blackI = new Item("black", blackId); blueI = new Item("blue", blueId);
        greenI = new Item("green", greenId); redI = new Item("red", redId); 
        whiteI = new Item("white", whiteId);
        Transform daizaAll = this.transform.parent;
        black = daizaAll.Find("black");
        blue = daizaAll.Find("blue");
        green = daizaAll.Find("green");
        red = daizaAll.Find("red");
        white = daizaAll.Find("white");
        allCrear();

        Debug.Log("指定色: " + setColor);
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
                    if (activeID[i] == blackId)
                    {
                        black.gameObject.SetActive(true);
                        blackB = true;
                        put = true;
                    }
                    else if (activeID[i] == blueId)
                    {
                        blue.gameObject.SetActive(true);
                        blueB = true;
                        put = true;
                    }
                    else if (activeID[i] == greenId)
                    {
                        green.gameObject.SetActive(true);
                        greenB = true;
                        put = true;
                    }
                    else if (activeID[i] == redId)
                    {
                        red.gameObject.SetActive(true);
                        redB = true;
                        put = true;
                    }
                    else if (activeID[i] == whiteId)
                    {
                        white.gameObject.SetActive(true);
                        whiteB = true;
                        put = true;
                    }
                    inv.delItem(activeID[i]);

                    if (activeID[i] == setColor && setColor == blackId)
                    {
                        blackC = true;
                    }
                    else if (activeID[i] == setColor && setColor == blueId)
                    {
                        blueC = true;
                    }
                    else if (activeID[i] == setColor && setColor == greenId)
                    {
                        greenC = true;
                    }
                    else if (activeID[i] == setColor && setColor == redId)
                    {
                        redC = true;
                    }
                    else if (activeID[i] == setColor && setColor == whiteId)
                    {
                        whiteC = true;
                    }
                    Debug.Log("黒判定: " + blackC);
                    Debug.Log("青判定: " + blueC);
                    Debug.Log("緑判定: " + greenC);
                    Debug.Log("赤判定: " + redC);
                    Debug.Log("白判定: " + whiteC);
                }
            }
        }
        if (allSetCheck.keyC == false && other.CompareTag("player") && Input.GetKey(KeyCode.F))
        {
            inv = other.GetComponent<inventory>();
            if (blackB)
            {
                inv.itemPick(blackI);
            }
            else if (blueB)
            {
                inv.itemPick(blueI);
            }
            else if (greenB)
            {
                inv.itemPick(greenI);
            }
            else if (redB)
            {
                inv.itemPick(redI);
            }
            else if (whiteB)
            {
                inv.itemPick(whiteI);
            }
            setFalse();
            allCrear();
            setFalsePriv();
            put = false;
        }
    }
    private void allCrear()
    {
        black.gameObject.SetActive(false);
        blue.gameObject.SetActive(false);
        green.gameObject.SetActive(false);
        red.gameObject.SetActive(false);
        white.gameObject.SetActive(false);
    }
    private void setFalse()
    {
        blackB = false;
        blueB = false;
        greenB = false;
        redB = false;
        whiteB = false;
    }
    private void setFalsePriv()
    {
        blackC = false;
        blueC = false;
        greenC = false;
        redC = false;
        whiteC = false;
    }
}
