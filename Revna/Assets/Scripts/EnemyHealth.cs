using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("ref")]
    [SerializeField] private Rigidbody2D rb;


    [Header("Life")]
    [SerializeField] private int maxLife;
    [SerializeField] private int actualLife;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        actualLife = maxLife; 
    }

    public void TakeDamage(int damage)
    {
        int tempLife = actualLife - damage;

        tempLife = Mathf.Clamp(tempLife, 0, maxLife);

        actualLife = tempLife;

        if(actualLife == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
