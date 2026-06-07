using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb2d;

    [SerializeField] private float velocity;

    [SerializeField] private Animator _animator;
    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb2d.linearVelocity = new Vector2(velocity, _rb2d.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerLife>().damage(1, other.GetContact(0).normal);
        }
    }

}
