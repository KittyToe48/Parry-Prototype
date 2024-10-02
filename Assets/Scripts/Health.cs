using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] GameObject _hitText;

    AudioSource _audioSource;
    [SerializeField] AudioClip _damageSound;

    [SerializeField] bool _immortal;
    bool _invincible = false;
    [SerializeField] float _timeInvincible;

    [SerializeField] TextMeshProUGUI _healthText;
    //[SerializeField] TextMeshProUGUI _scoreText;
    float _health;
    [SerializeField] float _maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _health = _maxHealth;
        if (gameObject.tag == "Player")
        {
            _healthText.text = _health.ToString() + " / " + _maxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(float damage)
    {
        if (!_immortal && !_invincible)
        {
            _audioSource.clip = _damageSound;
            _audioSource.Play();
            _health -= damage;

            if (gameObject.tag == "Player")
            {
                
                _healthText.text = _health.ToString() + " / " + _maxHealth;
            }
            else // Om fienden tar skada. 
            {
                // GÖr så att de blir aware av spelaren om de tar skada och blir stunned om de tar för mycket skada på en gång.
                GameObject hitText = Instantiate(_hitText, transform.position + (transform.up * 1.5f + transform.right * Random.Range(-0.7f, 0.7f)), transform.rotation);
                hitText.GetComponent<HitNumber>().Damage = damage;
            }
            if (_health <= 0)
            {
                Death();
            }
            StartCoroutine(Invinsibility());
        }
    }

    IEnumerator Invinsibility()
    {
        _invincible = true;
        yield return new WaitForSeconds(_timeInvincible);
        _invincible = false;
    }

    public void TakeHealth(float regen)
    {
        _health += regen;

        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
        //_healthText.text = _health.ToString() + " / " + _maxHealth;
    }

    void Death()
    {
        //if (gameObject.tag == "Player") Application.Quit();
        Destroy(gameObject);

    }
}
