using UnityEngine;

public class MouseCursorManager : MonoBehaviour
{
    public Texture2D cursorTexture;

    void Start()
    {
        // 起動直後はカーソル非表示
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // メニューを開くときに呼ぶ
    public void ShowCursor()
    {
        Vector2 hotspot = new Vector2(cursorTexture.width * 0.5f, cursorTexture.height * 0.5f);
        Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // メニューを閉じたら呼ぶ
    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
