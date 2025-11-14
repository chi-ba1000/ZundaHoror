using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject target; // 有効化したいオブジェクト（例：GameOver画面）

    private PlayerController player;
    private EnemyController[] enemies; // シーン内の全EnemyControllerを保持

    void Start()
    {
        // プレイヤー取得
        player = Object.FindFirstObjectByType<PlayerController>();

        // 全ての敵を取得
        enemies = Object.FindObjectsByType<EnemyController>(FindObjectsSortMode.None);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // GameOver画面表示
            if (target != null)
            {
                target.SetActive(true);
                Debug.Log("GameOver: " + target.name + " を有効化しました (Trigger)");
            }

            // プレイヤー停止
            if (player != null)
            {
                player.enabled = false;
                Debug.Log("PlayerControllerを停止しました");
            }

            // 敵全員を停止
            if (enemies != null && enemies.Length > 0)
            {
                foreach (var enemy in enemies)
                {
                    if (enemy != null)
                    {
                        enemy.enabled = false;
                    }
                }
                Debug.Log("EnemyControllerを全て停止しました");
            }

            // マウスカーソルを表示（必要なら）
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
        }
    }
}
