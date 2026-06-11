using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System;
public class PlayerLife : MonoBehaviour
{
    [SerializeField] private float life = 4f;
    private PlayerMovement playerMovement;

    [SerializeField] private float timeNoControl;

    public event EventHandler MuerteJugador;
    
    private Animator animator;
    private void Awake()
    {
        life = 4f; 
    }
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }
        
    public void damage(float daño, Vector2 posicion)
    {
        life -= daño;
        if(life > 0)
        {
            animator.SetTrigger("Golpe");
            StartCoroutine(loseControl());
            StartCoroutine(NoCollision());
            playerMovement.Rebote(posicion);
        }
        if(life <= 0)
        {
            animator.SetTrigger("Muerte");
            Physics2D.IgnoreLayerCollision(6, 7,  true); 
            life = 4f;
            SceneManager.LoadScene("Menu"); 
        }
    }

    public void DeadPlayer()
    {
        MuerteJugador?.Invoke(this, EventArgs.Empty);
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
