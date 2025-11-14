using UnityEngine;
using UnityEngine.UI;

public class ShowMap : MonoBehaviour
{
    [SerializeField] private Image mapImage;  // 表示・非表示を切り替える対象

    void Update()
    {
        if (mapImage == null) return;

        // Mキーを押している間だけ表示
        if (Input.GetKey(KeyCode.M))
        {
            mapImage.gameObject.SetActive(true);
        }
        else
        {
            mapImage.gameObject.SetActive(false);
        }
    }
}
