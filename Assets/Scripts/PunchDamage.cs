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

        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player") // Om n�got levande blir slaget
        {
            _audioSource.clip = _hitSound;
            _audioSource.Play();

            MeleeCombat combat = other.gameObject.transform.GetChild(0).GetComponent<MeleeCombat>();
            if (combat == null) combat = other.gameObject.GetComponent<MeleeCombat>();
            if (combat.GuardState) // N�r man blockar
            {
                Health health = other.gameObject.GetComponent<Health>();
                if (health == null) health = other.gameObject.GetComponentInParent<Health>();

                health.TakeDamage((Damage * DamageMultiplier) / 2); // Tar halva skadan
                ChaserAI stunned = transform.parent.transform.GetComponentInParent<ChaserAI>(); // Tar komponenten fr�n dess parents parent, fr�n handen till arms till fienden
                StartCoroutine(stunned.StunnedTimer(0.5f)); // Blir stunned
            }
            else if (combat.ParryState) // N�r man blir parried
            {
                _audioSource.clip = _parrySound;
                _audioSource.Play();
                ChaserAI stunned = transform.parent.transform.GetComponentInParent<ChaserAI>(); // Tar komponenten fr�n dess parents parent, fr�n handen till arms till fienden
                StartCoroutine(stunned.StunnedTimer(2f)); // Blir stunned
            }
            else
            {
                if (other.gameObject.tag == "Enemy") 
                {
                    ChaserAI enemyAI = other.gameObject.GetComponent<ChaserAI>();
                    if (enemyAI != null)
                    {
                        if (!enemyAI.AwareOfPlayer) DamageMultiplier += 1; // Om man smyger upp p� en fiende som inte k�nner till en g�r man 100% mer skada
                        else
                        {
                            EnemyVision enemyVision = other.gameObject.GetComponent<EnemyVision>(); // Om man smyger upp p� en fiende som k�nner till en g�r man 50% mer skada
                            if (!enemyVision.CanSeePlayer) DamageMultiplier += 0.5f;
                        }
                    }
                }

                Health health = other.gameObject.GetComponent<Health>();
                health.TakeDamage(Damage * DamageMultiplier);
                DamageMultiplier = 1;
            }
            //Debug.Log("Damage: " + Damage * DamageMultiplier);

        }
    }
}
