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
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player")
        {
            Health health = other.gameObject.GetComponent<Health>();
            health.TakeDamage(_damage);
            Debug.Log("Yeouch + " + other.gameObject.gameObject.name);
        }
    }
}
