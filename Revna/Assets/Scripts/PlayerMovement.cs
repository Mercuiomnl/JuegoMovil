using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerMovement : MonoBehaviour
{
    //variables
    [SerializeField] private float velocity;

    [SerializeField] private float upForce;

    [SerializeField] Vector2 sizeRayBox;
    [SerializeField] float castDistance; 
    public LayerMask capaSuelo;
    private bool enSuelo; 

    //componentes
    [SerializeField] private Animator _animator; 

    private Rigidbody2D rb2d;
    private PlayerInput py;

    private Vector2 _input;

    [SerializeField] private Transform controladorAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private int cantDamage;
    [SerializeField] private int cantStrongDamage;

    public float cooldown1 = 0.5f;
    public float timer1; 

    private void Start()
    {
        //Llamada de componentes 
        rb2d = GetComponent<Rigidbody2D>();
        py = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(timer1 > 0)
        {
            timer1 -= Time.deltaTime;
        }

        Move();
        Animator();

    }
    public bool touchFloor()
    {
        if(Physics2D.BoxCast(transform.position, sizeRayBox, 0, -transform.up, castDistance, capaSuelo))
        {
            return true;
        }
        else
        {
            return false; 
        }
    }
    public void Jump()
    {
        
        if (touchFloor())
        {
            rb2d.AddForce(Vector2.up * upForce, ForceMode2D.Impulse); 
        }
    }

    private void Move()
    {
        _input = py.actions["Move"].ReadValue<Vector2>();
        //Debug.Log(_input); 

        rb2d.transform.position += new Vector3(_input.x, 0, 0) * velocity * Time.deltaTime;
        //rb2d.linearVelocity = new Vector2(_input.x, 0) * velocity * Time.deltaTime;
    }
    private void Animator()
    {
        _animator.SetFloat("Movement", _input.x);
        if(_input.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (_input.x > 0) 
        {
            transform.localScale = new Vector3(1, 1, 1); 
        }

    }

    public void Atacar(InputAction.CallbackContext callbackContext)
    {
        if(timer1 <= 0)
        {
            Collider2D[] touchedObjects = Physics2D.OverlapCircleAll(controladorAtaque.position, radioAtaque);
            
            //if (callbackContext.performed == true)
            {
                foreach (Collider2D objeto in touchedObjects)
                {
                    if (objeto.TryGetComponent(out EnemyHealth enemyHealth))
                    {
                        _animator.SetTrigger("Atacar");
                        enemyHealth.TakeDamage(cantDamage);
                        Debug.Log("Atacó 1");
                    }
                }
            }
            timer1 = cooldown1;
        }
    }

    public void AtacarDoble(InputAction.CallbackContext callbackContext)
    {
        Collider2D[] touchedObjects = Physics2D.OverlapCircleAll(controladorAtaque.position, radioAtaque); 
        if(callbackContext.performed == true)
        {
            foreach (Collider2D objeto in touchedObjects)
            {
                if(objeto.TryGetComponent(out EnemyHealth enemyHealth))
                {
                    enemyHealth.TakeDamage(cantStrongDamage);
                    Debug.Log("Atacó 2");
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, sizeRayBox);
        Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque); 
    }
}
