using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Vides verk
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
            _health -= damage;
            if (gameObject.tag == "Player")
            {
                _healthText.text = _health.ToString() + " / " + _maxHealth;
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
