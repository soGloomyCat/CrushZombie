using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Zombie : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private int _reward;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;


    public int Reward => _reward;
    public Base Target { get; private set; }
    public float Health => _health;

    public event UnityAction<Zombie> Dying;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _animator.SetTrigger("TakeDamage");

        if (_health <= 0)
        {
            _animator.SetTrigger("Die");
            Invoke("Hide", 0.4f);
            Dying?.Invoke(this);
        }
    }

    public void InitTarget(Base target)
    {
        Target = target;
    }

    public void SetColor()
    {
        Color color;

        color = new Color(Random.Range(0.65f, 1.0f), Random.Range(0.65f, 1.0f), Random.Range(0.65f, 1.0f));
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = color;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void IncreaseHealth(float multiplier)
    {
        _health *= multiplier;
    }
}
