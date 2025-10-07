using UnityEngine;
using UnityEngine.AI; // NavMeshAgentを使うために必要

public class EnemyController : MonoBehaviour
{
    public Transform player; // 追いかける対象（プレイヤー）を格納する変数
    public float attackdistance;
    public float rotationSpeed;
    public Wepon wepon;
    public knifethrower knifethrower;
    private NavMeshAgent agent; // NavMeshAgentコンポーネントを格納する変数
    private Animator anim;
    private Vector3 position;

    void Start()
    {
        // このオブジェクトにアタッチされているNavMeshAgentコンポーネントを取得
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        // プレイヤーを自動で見つける場合（タグを"Player"に設定しておく）
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < attackdistance)
        {
            
            agent.isStopped = true;
            anim.SetBool("idle", true);
            anim.SetBool("walk", false);
            anim.SetTrigger("attack");
            if (wepon == Wepon.throwableknife && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                // 1. ターゲットへの方向ベクトルを計算
                Vector3 direction = player.position - transform.position;

                // 2. その方向を向くための回転（クォータニオン）を計算
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // 3. 現在の回転から目標の回転へ滑らかに補間
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                knifethrower.Throw();
            }
            
        }
        // プレイヤーが設定されていれば、その位置を目的地に設定し続ける
        else if (player != null && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            anim.SetBool("walk", true);
            anim.SetBool("idle", false);
        }

    }
    public enum Wepon
    {
        none,throwableknife
    }
}