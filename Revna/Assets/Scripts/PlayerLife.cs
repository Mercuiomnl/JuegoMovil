using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private float life;
    private PlayerMovement playerMovement;

    [SerializeField] private float timeNoControl; 
    
    private Animator animator;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }
        
    public void damage(float daño, Vector2 posicion)
    {
        life -= daño;
        animator.SetTrigger("Golpe");
        StartCoroutine(loseControl());
        playerMovement.Rebote(posicion); 
    }
    private IEnumerator loseControl()
    {
        playerMovement.sePuedeMover = false;
        yield return new WaitForSeconds(timeNoControl);
        playerMovement.sePuedeMover = true; 
    }
}
