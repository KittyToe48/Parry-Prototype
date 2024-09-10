using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaserAI : MonoBehaviour
{
    [SerializeField] float _attackCooldown;
    [SerializeField] float _punchTimer;

    NavMeshAgent _agent;

    GameObject _player;

    PlayerCombat _combat;

    bool _attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        _combat = GetComponent<PlayerCombat>();
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Chase();
    }

    void Chase() // Distance eller psyhics.checksphere?
    {
        _agent.SetDestination(_player.transform.position);
        transform.LookAt(_player.transform.position);

        if (Vector3.Distance(transform.position, _agent.destination) <= 5 && !_attacking)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        _attacking = true;
        Debug.Log("Let me introduce you to the NEW JOCKER!");
        float attackCooldown = _attackCooldown;
        while(true)
        {
            yield return null;
            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0)
            {
                StartCoroutine(_combat.Punch(_punchTimer));
                yield return new WaitForSeconds(_punchTimer);
                _attacking = false;
                break;
            }
        }
    }
}
