using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchDamage : MonoBehaviour
{
    [SerializeField] float _damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.gameObject.GetComponent<Health>();
        
        Debug.Log("Yeouch + " + other.gameObject.gameObject.name);

        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player")
        {
            MeleeCombat combat = other.gameObject.transform.GetChild(0).GetComponent<MeleeCombat>();
            if (combat.GuardState) health.TakeDamage(_damage / 2);
            else if (combat.ParryState)
            {
                MeleeCombat stunned = transform.GetComponentInParent<MeleeCombat>();
                StartCoroutine(stunned.StunnedTimer());
            }
            else health.TakeDamage(_damage);

        }
    }
}
