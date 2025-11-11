using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.U2D; // NavMeshAgentを使うために必要

public class EnemyController : MonoBehaviour
{
    public Transform player; // 追いかける対象（プレイヤー）を格納する変数
    public float attackdistance;
    public float rotationSpeed;
    public float searchdistance;
    public Wepon wepon;
    public knifethrower knifethrower;
    public Vector3 eyeposition;
    public Vector3 centerpoint;
    public float XRange;
    public float YRange;
    public float ZRange;
    public float waitSec;
    public int stackSec;
    private NavMeshAgent agent; // NavMeshAgentコンポーネントを格納する変数
    private Animator anim;
    private Vector3 position;
    private bool isSprint;
    private int fixedCount;
    private int stackCounter;

    void Start()
    {
        // このオブジェクトにアタッチされているNavMeshAgentコンポーネントを取得
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        RandomDest();
    }

    void Update()
    {
        //キャラクターの目からプレイヤーまでの距離を計算
        float distance = Vector3.Distance(transform.position + eyeposition, player.position + new Vector3(0f, 1.2f, 0f));
        Vector3 eyePosition = transform.position + eyeposition;
        Vector3 playerCenter = player.position + Vector3.up * 1.0f;
        Vector3 directionToPlayer = (playerCenter - eyePosition).normalized;

        //距離がattackdistance以下の時
        if (distance < attackdistance)
        {
            agent.isStopped = true;
            RaycastHit hit;
            //プレイヤーに向けてRayを出してそれがプレイヤーに当たったとき
            if (Physics.Raycast(eyePosition, directionToPlayer, out hit, searchdistance) && hit.collider.CompareTag("Player"))
            {
                agent.isStopped = true;
                anim.SetBool("idle", true);
                anim.SetBool("walk", false);
                anim.SetTrigger("attack");
                //この敵キャラがthrowableknifeを持っているとき
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

        }

        //距離がsearchdistance以下の時
        else
        {
            agent.isStopped = false;
            RaycastHit hit;
            bool isCanseePlayer = false;
            //プレイヤーに向けてRayを出してそれがプレイヤーに当たったとき
            if (Physics.Raycast(eyePosition, directionToPlayer, out hit, searchdistance) && hit.collider.CompareTag("Player"))
            {
                isCanseePlayer = true; 
            }
            //プレイヤーが見えたとき目的地をプレイヤーにセット
            if (isCanseePlayer)
            {
                agent.SetDestination(player.position);
                anim.SetBool("walk", true);
                anim.SetBool("idle", false);
                Debug.Log("detect");
            }
            //でないときランダムウォーク
            else
            {
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    WaitforNewDest();
                    RandomDest();
                }
            }
        }
    }

    //スタック処理
    private void FixedUpdate()
    {
        fixedCount++;
        Vector3 currentposition = transform.position;
        //stackSecの間スタックした場合目的地を更新する
        if (fixedCount % 50 == 0 && currentposition == position)
        {
            stackCounter++;
            Debug.Log("stackked");
        }
        if (stackCounter == stackSec)
        {
            RandomDest();
            stackCounter = 0;
        }
        position = currentposition;
    }
    public enum Wepon
    {
        none,throwableknife
    }

    //デバック用
    void OnDrawGizmosSelected()
    {
        // 索敵範囲の円
        Gizmos.color = Color.yellow;
        Vector3 patrolBoxSize = new Vector3(XRange * 2, YRange * 2, ZRange * 2);
        Gizmos.DrawWireCube(centerpoint, patrolBoxSize);
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

    //音に反応する敵の時プレイヤーから発火できるようにして，目的地をプレイヤーの座標へセット
    public void Makenoise()
    {
        agent.SetDestination(player.position);
    }

    //範囲内のランダムな座標を指定する
    void RandomDest()
    {
        anim.SetBool("walk", true);
        anim.SetBool("idle", false);
        float randomX = Random.Range(-XRange, XRange);
        float randomY = Random.Range(-YRange, YRange);
        float randomZ = Random.Range(-YRange, YRange);
        Vector3 RandomPosition = centerpoint + new Vector3(randomX, randomY, randomZ);
        NavMeshHit hit;
        if (NavMesh.SamplePosition(RandomPosition, out hit, YRange, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
            Debug.Log($"NewDestenation{hit.position}");
        }

    }

    //目的地に着いた後次の移動をするまでの待機時間
    IEnumerator WaitforNewDest()
    {
        anim.SetBool("walk", false);
        anim.SetBool("idle", true);
        float randomSec = Random.Range(waitSec, waitSec + 2);
        yield return new WaitForSeconds(randomSec);
    }
}