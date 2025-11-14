using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class SafeUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] numberDisplays;
    private PlayerController playerController;
    private int[] currentCode = new int[4];
    private safe currentSafe;
    private GameObject player;

    void Start()
    {
        gameObject.SetActive(false);
    }

    //金庫 (Safe.cs) から呼び出される
    public void OpenPanel(safe safe, GameObject playerObject)
    {
        currentSafe = safe;
        player = playerObject;

        playerController = player.GetComponent<PlayerController>();

        //プレイヤーの操作をロック
        playerController.enabled = false;

        // UIを初期化 (0, 0, 0, 0 にする)
        currentCode = new int[4];
        UpdateAllDisplays();

        gameObject.SetActive(true); // パネルを表示
        Cursor.visible = true;
    }

    //UpButton
    public void OnDigitUp(int slotIndex)
    {
        currentCode[slotIndex]++; 
        if (currentCode[slotIndex] > 9)
        {
             currentCode[slotIndex] = 0; // 9 の次は 0
        }
        UpdateDisplay(slotIndex); // 見た目を更新
    }

    //DownButton
    public void OnDigitDown(int slotIndex)
    {
        currentCode[slotIndex]--; 
        if (currentCode[slotIndex] < 0) currentCode[slotIndex] = 9; // 0 の次は 9
        UpdateDisplay(slotIndex); // 見た目を更新
    }

    //Enter
    public void OnEnterClicked()
    {
        if (currentSafe != null)
        {
            string codeString = string.Join("", currentCode);
            
            currentSafe.CheckPassword(codeString);
        }
        ClosePanel(); 
    }

    //パネルを閉じる（プレイヤー操作を元に戻す）
    public void ClosePanel()
    {
        playerController.enabled = true;
        gameObject.SetActive(false);
        Cursor.visible = false;
    }
    
    //UIの見た目を更新するヘルパー
    
    private void UpdateAllDisplays()
    {
        for (int i = 0; i < numberDisplays.Length; i++) 
        {
            UpdateDisplay(i);
        }
    }
    
    private void UpdateDisplay(int slotIndex)
    {
        if (numberDisplays[slotIndex] != null)
        {
            numberDisplays[slotIndex].text = currentCode[slotIndex].ToString();
        }
    }
}