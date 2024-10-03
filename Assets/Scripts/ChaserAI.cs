using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaserAI : MonoBehaviour
{
    public bool Stunned = false;

    public Material[] Materials;
    [HideInInspector] public MeshRenderer MeshRenderer;

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
        MeshRenderer = GetComponent<MeshRenderer>();

        player = GameObject.FindGameObjectWithTag("Player");

        combat = transform.GetChild(0).GetComponent<MeleeCombat>();
        enemyVision = GetComponent<EnemyVision>();

        agent = GetComponent<NavMeshAgent>();
    }

    public void Chase()
    { 
        if (!Stunned) // Jagar spelaren om de inte är stunned
        {
            AwareOfPlayer = true;
            agent.SetDestination(player.transform.position);
            
            //if (lostPlayer) transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

            if (Vector3.Distance(transform.position, agent.destination) <= 5 && !attacking)
            {
                StartCoroutine(Attack());
            }
        }
        else enemyVision.CanSeePlayer = false; // Kan inte se spelaren om de är stunned
    }

    public IEnumerator Search(float timer)
    {
        enemyVision.CanSeePlayer = false;
        //Debug.Log("Searching: " + enemyVision.CanSeePlayer);
        while (timer > 0)
        {
            
            timer -= Time.deltaTime;
            Chase();

            if (enemyVision.CanSeePlayer)
            {
                Debug.Log("Saw you");
                break;
            }
            Debug.Log("Yerp: " + timer);
            yield return null;
        }
    }

    public IEnumerator LostPlayer(float multiplier)
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
        MeshRenderer.material = Materials[1];
        while(true)
        {
            yield return null;
            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0)
            {
                combat.PunchUp();
                MeshRenderer.material = Materials[2];
                yield return new WaitForSeconds(punchClip.length);
                attacking = false;
                if (!Stunned) MeshRenderer.material = Materials[0];
                break;
            }
            else if (Stunned)
            {
                attacking = false;
                MeshRenderer.material = Materials[0];
                break;
            }
        }
    }

    public IEnumerator StunnedTimer(float timer)
    {
        if (!Stunned)
        {
            Stunned = true;
            MeshRenderer.material = Materials[3];
            Debug.Log("Arggh I am stunned: " + gameObject.tag);

                yield return new WaitForSeconds(timer);
                StartCoroutine(LostPlayer(1));
            
            Stunned = false;
            MeshRenderer.material = Materials[0];
        }
    }
}
