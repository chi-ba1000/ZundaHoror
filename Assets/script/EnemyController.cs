using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem; // NavMeshAgentを使うために必要

public class EnemyController : MonoBehaviour
{
    public Transform player; // 追いかける対象（プレイヤー）を格納する変数
    public float attackdistance;
    public float rotationSpeed;
    public float searchdistance;
    public Wepon wepon;
    public knifethrower knifethrower;
    public Vector3 eyeposition;
    private NavMeshAgent agent; // NavMeshAgentコンポーネントを格納する変数
    private Animator anim;
    private Vector3 position;
    private bool isSprint;

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
        float distance = Vector3.Distance(transform.position + eyeposition, player.position + new Vector3(0f, 1.2f, 0f));
        Vector3 eyePosition = transform.position + eyeposition;
        Vector3 playerCenter = player.position + Vector3.up * 1.0f;
        Vector3 directionToPlayer = (playerCenter - eyePosition).normalized;

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
        else if (distance < searchdistance)
        {
            agent.isStopped = false;
            Debug.Log(distance);
            RaycastHit hit;
            if (Physics.Raycast(eyePosition, directionToPlayer, out hit, searchdistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    agent.SetDestination(player.position);
                    anim.SetBool("walk", true);
                    anim.SetBool("idle", false);
                    Debug.Log("detect");
                }               
            }
        }
        

    }
    public enum Wepon
    {
        none,throwableknife
    }
    void OnDrawGizmosSelected()
    {
        // 索敵範囲の円
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, searchdistance);

        // 視線のデバッグ表示
        if (player != null)
        {
            Vector3 eyePosition = transform.position + eyeposition;
            Vector3 playerCenter = player.position + Vector3.up * 1.0f;
            Vector3 directionToPlayer = (playerCenter - eyePosition).normalized;

            RaycastHit hit;
            // Rayを飛ばしてみて、何に当たったかで色を変える
            if (Physics.Raycast(eyePosition, directionToPlayer, out hit, searchdistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    // プレイヤーに届いている（緑）
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(eyePosition, hit.point);
                }
                else
                {
                    // 壁に遮られている（赤）
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(eyePosition, hit.point);
                }
            }
            else
            {
                // 誰にも当たらない（グレー）
                Gizmos.color = Color.gray;
                Gizmos.DrawRay(eyePosition, directionToPlayer * searchdistance);
            }
        }
    }
    public void Makenoise()
    {
        agent.SetDestination(player.position);
    }
}