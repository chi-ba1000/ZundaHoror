using UnityEngine;
using UnityEngine.AI; // NavMeshAgentを使うために必要

public class EnemyController : MonoBehaviour
{
    public Transform player; // 追いかける対象（プレイヤー）を格納する変数
    private NavMeshAgent agent; // NavMeshAgentコンポーネントを格納する変数

    void Start()
    {
        // このオブジェクトにアタッチされているNavMeshAgentコンポーネントを取得
        agent = GetComponent<NavMeshAgent>();

        // プレイヤーを自動で見つける場合（タグを"Player"に設定しておく）
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        // プレイヤーが設定されていれば、その位置を目的地に設定し続ける
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }
}