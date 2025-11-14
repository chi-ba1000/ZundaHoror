using UnityEngine;

// 既存の SimplestarGame 名前空間を使うか、
// 名前空間なし（デフォルト）でも構いません
namespace SimplestarGame
{
    public class PlayerInteractor : MonoBehaviour
    {
        public int activeID = 3;
        private inventory inv;
        private GameObject playerObject;
        [SerializeField] Camera playerCamera; // プレイヤーの視点となるカメラ
        [SerializeField] float interactRange = 3.0f; // アイテム（破壊）が届く距離

        void Start()
        {
            playerObject = GameObject.FindWithTag("player");
            inv = playerObject.GetComponent<inventory>();
        }

        void Update()
        {
            if (inv.checkItem(activeID))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    // カメラの中心から正面にレイを飛ばす
                    Ray ray = playerCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
                
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        if (hit.distance <= interactRange)
                        {
                            if (hit.collider.TryGetComponent(out DestructibleWall destructibleWall))
                            {
                                // 3. 壁に「壊れろ」と指示を出す
                                destructibleWall.Activate(hit);
                                Debug.Log("Ray: " + hit.point);
                            }
                        }
                    }
                }
            }         
        }
    }
}