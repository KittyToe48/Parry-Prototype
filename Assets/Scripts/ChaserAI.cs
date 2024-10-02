using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaserAI : MonoBehaviour
{
    [SerializeField] Material[] materials;
    MeshRenderer meshRenderer;

    [SerializeField] float attackCooldown;

    [SerializeField] AnimationClip punchClip;

    NavMeshAgent agent;

    GameObject player;

    MeleeCombat combat;
    EnemyVision enemyVision;

    bool attacking = false;
    public bool AwareOfPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        player = GameObject.FindGameObjectWithTag("Player");

        combat = transform.GetChild(0).GetComponent<MeleeCombat>();
        enemyVision = GetComponent<EnemyVision>();

        agent = GetComponent<NavMeshAgent>();
    }

    public void Chase()
    { 
        if (!combat.Stunned)
        {
            AwareOfPlayer = true;
            agent.SetDestination(player.transform.position);
            //transform.LookAt(new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z));

            if (Vector3.Distance(transform.position, agent.destination) <= 5 && !attacking)
            {
                StartCoroutine(Attack());
            }
        }
        else enemyVision.CanSeePlayer = false;
    }

    public void LostPlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    public IEnumerator StunnedSearch()
    {
        if (!enemyVision.CanSeePlayer)
        {
            int turns = 0;
            float turnsPerSecond = 100;
            float turnSpeed = turnsPerSecond;
            float turnTimer = 180 / turnsPerSecond;

            while (true)
            {
                transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed);
                turnTimer -= Time.deltaTime;

                if (turnTimer <= 0) //När tiden på timerTurn har tagit slut kommer den att vända riktning
                {
                    turns++;
                    if (turns == 1)
                    {
                        turnsPerSecond = 200;
                        turnTimer = 360 / turnsPerSecond;
                        turnsPerSecond = -turnsPerSecond;
                        turnSpeed = turnsPerSecond;
                        Debug.Log("Turning1: " + turns + ", " + turnTimer);
                        yield return null;
                    }
                    else if (turns == 2)
                    {
                        turnsPerSecond = 110;
                        turnSpeed = turnsPerSecond;
                        turnTimer = 180 / turnsPerSecond;
                        Debug.Log("Turning2: " + turns + ", " + turnTimer);
                        yield return null;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (enemyVision.CanSeePlayer) break;
                yield return null;
            }
        }
    }

    IEnumerator Attack()
    {
        attacking = true;
        //Debug.Log("Let me introduce you to the NEW JOCKER!");
        float attackCooldown = this.attackCooldown;
        meshRenderer.material = materials[1];
        while(true)
        {
            yield return null;
            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0)
            {
                combat.PunchUp();
                meshRenderer.material = materials[2];
                yield return new WaitForSeconds(punchClip.length);
                attacking = false;
                meshRenderer.material = materials[0];
                break;
            }
            else if (combat.Stunned)
            {
                attacking = false;
                meshRenderer.material = materials[0];
                break;
            }
        }
    }
}
