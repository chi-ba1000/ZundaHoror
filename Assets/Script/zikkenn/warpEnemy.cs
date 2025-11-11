using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class warpEnemy : MonoBehaviour
{
    [SerializeField] GameObject player;
    Vector3 playerPos, warpEnemyPos;
    float warpPos_x, warpPos_z;
    int distance = 30;
    NavMeshAgent agent;
    private Animator animator;
    private SkinnedMeshRenderer SkinMesh;
    [SerializeField] ParticleSystem particle;
    bool ifWarp;

    void Start()
    {
        player = GameObject.Find("Zundamon_2025A_VRC");
        StartCoroutine("Warp");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        SkinMesh = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    void Update()
    {
        Distance();
        if (ifWarp == false)
        {
            if (1 < distance && distance < 40)
            {
                agent.isStopped = false;
                agent.destination = player.transform.position;
                animator.SetBool("walk", true);
                animator.SetBool("idle", false);
                animator.SetBool("attack", false);
            }
            else if (distance <= 1)
            {
                animator.SetBool("walk", false);
                animator.SetBool("attack", true);
                agent.isStopped = true;
            }
            else
            {
                animator.SetBool("walk", false);
                animator.SetBool("idle", true);
                agent.isStopped = true;
            }
        }
        else
        {
            agent.isStopped = true;
            animator.SetBool("walk", false);
            animator.SetBool("attack", false);
            animator.SetBool("idle", true);
        }
    }

    IEnumerator Warp()
    {
        while (true)
        {
            int waitTime = Random.Range(10, 15);         

            if (distance < 15)
            {
                ifWarp = true;

                StartCoroutine("disappear");
                ParticleSystem newParticle = Instantiate(particle);
                newParticle.transform.position = this.transform.position;
                newParticle.Play();
                Destroy(newParticle.gameObject, 5.5f);
                yield return new WaitForSeconds(4f);

                Distance();
                for (int i = 0; i < 2; i++)
                {
                    int[] spownRange = { -3, -2, 2, 3 };
                    int spownIndex = Random.Range(0, spownRange.Length);
                    int warpPosIndex = spownRange[spownIndex];
                    if (i == 0)
                    {
                        warpPos_x = playerPos.x + warpPosIndex;
                    }
                    else
                    {
                        warpPos_z = playerPos.z + warpPosIndex;
                    }
                }
                Vector3 warpPos = new Vector3(warpPos_x, playerPos.y + 1, warpPos_z);

                transform.position = warpPos;
                newParticle.transform.position = this.transform.position - new Vector3(0, 1, 0);

                StartCoroutine("appear");
                yield return new WaitForSeconds(0.7f);
                ifWarp = false;
            }
            yield return new WaitForSeconds(waitTime);
        }
    }
    IEnumerator disappear()
    {
        for ( int i = 0 ; i < 255 ; i++ )
        {
          SkinMesh.material.color = SkinMesh.material.color - new Color32(0,0,0,1);
          yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator appear()
    {
        for ( int i = 0 ; i < 255 ; i++ )
        {
          SkinMesh.material.color = SkinMesh.material.color + new Color32(0,0,0,1);
          yield return new WaitForSeconds(0.001f);
        }
    }
    private void Distance()
    {
        playerPos = player.transform.position;
        warpEnemyPos = this.gameObject.transform.position;
        distance = (int)Mathf.Ceil(Vector3.Distance(playerPos, warpEnemyPos));
    }
}
