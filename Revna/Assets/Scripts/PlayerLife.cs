using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
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
        StartCoroutine(NoCollision()); 
        playerMovement.Rebote(posicion); 
        if(life <= 0)
        {
            SceneManager.LoadScene("Menu"); 
        }
    }
    private IEnumerator NoCollision()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true); 
        yield return new WaitForSeconds(timeNoControl);
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }
    private IEnumerator loseControl()
    {
        playerMovement.sePuedeMover = false;
        yield return new WaitForSeconds(timeNoControl);
        playerMovement.sePuedeMover = true; 
    }
}
