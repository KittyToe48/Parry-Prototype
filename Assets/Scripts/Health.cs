using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    ChaserAI enemyAI;

    [SerializeField] GameObject _hitText;

    AudioSource audioSource;
    [SerializeField] AudioClip _damageSound;

    [SerializeField] bool _immortal;
    bool invincible = false;
    [SerializeField] float _timeInvincible;

    [SerializeField] TextMeshProUGUI _healthText;
    //[SerializeField] TextMeshProUGUI _scoreText;
    float health;
    [SerializeField] float _maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        enemyAI = GetComponent<ChaserAI>();

        audioSource = GetComponent<AudioSource>();

        health = _maxHealth;
        if (gameObject.tag == "Player")
        {
            _healthText.text = health.ToString() + " / " + _maxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(float damage)
    {
        if (!_immortal && !invincible)
        {
            audioSource.clip = _damageSound;
            audioSource.Play();
            health -= damage;

            if (gameObject.tag == "Player")
            {
                
                _healthText.text = health.ToString() + " / " + _maxHealth;
            }
            else // Om fienden tar skada. 
            {
                enemyAI.AwareOfPlayer = true;
                StartCoroutine(enemyAI.Search(1));
                if (damage > (_maxHealth / 2)) StartCoroutine(enemyAI.StunnedTimer(1f));
                GameObject hitTextObject = Instantiate(_hitText, transform.position + (transform.up * 1.5f + transform.right * Random.Range(-0.7f, 0.7f)), transform.rotation);
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
        yield return new WaitForSeconds(_timeInvincible);
        invincible = false;
    }

    public void TakeHealth(float regen)
    {
        health += regen;

        if (health > _maxHealth)
        {
            health = _maxHealth;
        }
        //_healthText.text = _health.ToString() + " / " + _maxHealth;
    }

    void Death()
    {
        //if (gameObject.tag == "Player") Application.Quit();
        Destroy(gameObject);

    }
}
