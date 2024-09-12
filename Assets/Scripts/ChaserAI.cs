using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaserAI : MonoBehaviour
{
    [SerializeField] Material[] _materials;
    MeshRenderer _meshRenderer;

    [SerializeField] float _attackCooldown;
    [SerializeField] float _punchTimer;

    [SerializeField] AnimationClip _punchClip;

    NavMeshAgent _agent;

    GameObject _player;

    MeleeCombat _combat;

    bool _attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        _player = GameObject.FindGameObjectWithTag("Player");

        _combat = GetComponent<MeleeCombat>();
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_combat.Stunned) Chase();

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
        _meshRenderer.material = _materials[1];
        while(true)
        {
            yield return null;
            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0)
            {
                _combat.Punch();
                _meshRenderer.material = _materials[2];
                yield return new WaitForSeconds(_punchClip.length);
                _attacking = false;
                _meshRenderer.material = _materials[0];
                break;
            }
            else if (_combat.Stunned)
            {
                _attacking = false;
                _meshRenderer.material = _materials[0];
                break;
            }
        }
    }
}
