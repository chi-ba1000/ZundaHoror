using UnityEngine;

// 既存の SimplestarGame 名前空間を使うか、
// 名前空間なし（デフォルト）でも構いません
namespace SimplestarGame
{
    // このスクリプトは VoronoiFragmenter と同じオブジェクトにいる必要がある
    [RequireComponent(typeof(VoronoiFragmenter))]
    public class DestructibleWall : MonoBehaviour
    {
        private VoronoiFragmenter fragmenter;

        void Awake()
        {
            // 起動時に、自分の隣にある VoronoiFragmenter を見つけておく
            fragmenter = GetComponent<VoronoiFragmenter>();
        }

        // PlayerInteractor から呼び出される「起動スイッチ」
        public void Activate(RaycastHit hit)
        {
            // 破壊処理を実行する
            if (fragmenter != null)
            {
                fragmenter.Fragment(hit);
            }
        }
    }
}