using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    // Components
    [SerializeField] LayerMask _whatIsPlayer, _whatIsWall;

    [HideInInspector] public Transform Player;

    GameObject player;

    // Variables
    [Range(0, 360)]
    public float Angle;

    public float Radius;

    public bool CanSeePlayer;

    ChaserAI enemyAI;

    // Start is called before the first frame update
    private void Awake()
    {
        enemyAI = GetComponent<ChaserAI>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck() //Kollar om den ser spelaren
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, Radius, _whatIsPlayer);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < Angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _whatIsWall))
                {
                    CanSeePlayer = true;
                    enemyAI.Chase();
                }
                else
                {
                    CanSeePlayer = false;
                }
            }
            else
            {
                CanSeePlayer = false;
            }
        }
        else if (CanSeePlayer)
        {
            CanSeePlayer = false;
            enemyAI.LostPlayer();
        }
    }
}
