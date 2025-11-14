using UnityEngine;

public class NotImplementedShow : MonoBehaviour
{
    [SerializeField] private GameObject target;  // 表示する対象

    private void OnMouseDown()
    {
        if (target != null)
        {
            target.SetActive(true);
            Debug.Log("未実装オブジェクトを表示しました");
        }
        else
        {
            Debug.LogWarning("target が指定されていません");
        }
    }
}
