using UnityEditor.Build;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb2d; 
    [SerializeField] private Transform player;
    [SerializeField] private float radioDetec;
    [SerializeField] private float velocity;
    private Vector2 movement;
    
    private Animator animator;

    private bool enMovimiento; 
    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move(); 
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerLife>().damage(1, other.GetContact(0).normal);
        }
    }
    private void Move()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < radioDetec)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            movement = new Vector2(direction.x, 0);
            enMovimiento = true;
        }
        else
        {
            movement = Vector2.zero;
            enMovimiento = false;
        }
        _rb2d.MovePosition(_rb2d.position + movement * velocity * Time.deltaTime);
        animator.SetBool("Moving", enMovimiento);
    }
    void OnDrawGizmos()
    {   
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDetec); 
    }
}
