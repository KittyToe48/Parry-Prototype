using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    ChaserAI enemyAI;

    [SerializeField] GameObject hitText;

    AudioSource audioSource;
    [SerializeField] AudioClip damageSound;

    [SerializeField] bool immortal;
    bool invincible = false;
    [SerializeField] float timeInvincible;

    [SerializeField] TextMeshProUGUI healthText;
    //[SerializeField] TextMeshProUGUI _scoreText;
    float health;
    [SerializeField] float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        enemyAI = GetComponent<ChaserAI>();

        audioSource = GetComponent<AudioSource>();

        health = maxHealth;
        if (gameObject.tag == "Player")
        {
            healthText.text = health.ToString() + " / " + maxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(float damage)
    {
        if (!immortal && !invincible)
        {
            audioSource.clip = damageSound;
            audioSource.Play();
            health -= damage;

            if (gameObject.tag == "Player")
            {
                
                healthText.text = health.ToString() + " / " + maxHealth;
            }
            else // Om fienden tar skada. 
            {
                enemyAI.AwareOfPlayer = true;
                StartCoroutine(enemyAI.Search(1));
                if ((maxHealth / damage) > (maxHealth / 2)) StartCoroutine(enemyAI.StunnedTimer(1.5f));
                GameObject hitTextObject = Instantiate(hitText, transform.position + (transform.up * 1.5f + transform.right * Random.Range(-0.7f, 0.7f)), transform.rotation);
                hitTextObject.GetComponent<HitNumber>().Damage = damage;
            }
            if (health <= 0)
            {
                Death();
            }
            StartCoroutine(Invinsibility());
        }
    }

    IEnumerator Invinsibility()
    {
        invincible = true;
        yield return new WaitForSeconds(timeInvincible);
        invincible = false;
    }

    public void TakeHealth(float regen)
    {
        health += regen;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
        //_healthText.text = _health.ToString() + " / " + _maxHealth;
    }

    void Death()
    {
        //if (gameObject.tag == "Player") Application.Quit();
        Destroy(gameObject);

    }
}
