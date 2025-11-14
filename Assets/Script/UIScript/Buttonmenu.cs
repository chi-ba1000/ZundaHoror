using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerDownHandler
{
    public GameObject menu;                 // メニューUI
    private PlayerController player;

    void Start()
    {
        // シーン内の PlayerController を取得
        player = Object.FindFirstObjectByType<PlayerController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (menu == null) return;

        // クリックされたオブジェクトを取得（子オブジェクトでも正しく取得）
        GameObject clicked = eventData.pointerCurrentRaycast.gameObject;

        // menu が非表示なら開く
        if (!menu.activeSelf)
        {
            OpenMenu();
        }
        else
        {
            CloseMenu();
        }
    }

    void Update()
    {
        // ESCキーでメニュー開閉
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menu.activeSelf)
                OpenMenu();
            else
                CloseMenu();
        }
    }

    // メニューを開く
    public void OpenMenu()
    {
        menu.SetActive(true);
        Time.timeScale = 0f;

        if (player != null)
            player.enabled = false;  // PlayerController を停止
    }

    // メニューを閉じる
    public void CloseMenu()
    {
        menu.SetActive(false);
        Time.timeScale = 1f;

        if (player != null)
            player.enabled = true;   // PlayerController を再開
    }
}
