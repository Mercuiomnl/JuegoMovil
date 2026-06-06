using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;

    [SerializeField] private float _spawnTime;
    private BoxCollider2D _boxCollider;
    private float _spawnTimer;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _spawnTimer = _spawnTime;
    }
    private void Update()
    {
        _spawnTimer -= Time.deltaTime;

        if( _spawnTimer <= 0)
        {
            SpawnEnemy();
            _spawnTimer = _spawnTime; 
        }
    }

    private void SpawnEnemy()
    {
        Vector2 position = Getposition(); 

        Instantiate(_enemy, position, Quaternion.identity);
    }

    private Vector2 Getposition()
    {
        Bounds bounds = _boxCollider.bounds;
        float x = bounds.min.x;
        float y = bounds.min.y; 

        return new Vector2(x, y);
    }
}
