using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchDamage : MonoBehaviour
{
    public float Damage = 10;
    public float DamageMultiplier = 1;

    AudioSource _audioSource;
    [SerializeField] AudioClip _parrySound;
    [SerializeField] AudioClip _hitSound;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Yeouch + " + other.gameObject.gameObject.name);

        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player")
        {
            _audioSource.clip = _hitSound;
            _audioSource.Play();
            MeleeCombat combat = other.gameObject.transform.GetChild(0).GetComponent<MeleeCombat>();
            if (combat == null) combat = other.gameObject.GetComponent<MeleeCombat>();
            if (combat.GuardState)
            {
                Health health = transform.GetComponentInParent<Health>();
                health.TakeDamage((Damage * DamageMultiplier) / 2);
                MeleeCombat stunned = transform.GetComponentInParent<MeleeCombat>();
                StartCoroutine(stunned.StunnedTimer(0.5f));
            }
            else if (combat.ParryState)
            {
                _audioSource.clip = _parrySound;
                _audioSource.Play();
                MeleeCombat stunned = transform.GetComponentInParent<MeleeCombat>();
                StartCoroutine(stunned.StunnedTimer(2f));
            }
            else
            {
                Health health = other.gameObject.GetComponent<Health>();
                health.TakeDamage(Damage * DamageMultiplier);
            }
            //Debug.Log("Damage: " + Damage * DamageMultiplier);

        }
    }
}
