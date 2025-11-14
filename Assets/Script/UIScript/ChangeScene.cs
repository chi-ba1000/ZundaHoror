using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string sceneName;       // 遷移先のシーン名
    [SerializeField] private GameObject beforeLoad;  // シーン移動前に有効化するオブジェクト
    [SerializeField] private float delay = 0.5f;     // 有効化してからシーンを切り替えるまでの待機時間（秒）

    private bool isLoading = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isLoading)
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                StartCoroutine(ChangeSceneRoutine());
            }
            else
            {
                Debug.LogWarning("シーン名が指定されていません");
            }
        }
    }

    private System.Collections.IEnumerator ChangeSceneRoutine()
    {
        isLoading = true;

        // 指定オブジェクトを有効化
        if (beforeLoad != null)
        {
            beforeLoad.SetActive(true);
        }

        // 少し待つ（アニメーションやSEなどに対応できる）
        yield return new WaitForSeconds(delay);

        // シーン切り替え
        SceneManager.LoadScene(sceneName);
    }
}
