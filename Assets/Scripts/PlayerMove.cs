//
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Clip audio Ã  jouer quand le joueur saute (assignÃ© dans lâ€™Inspector)
    [SerializeField] AudioClip sfxJump;

    // Clip audio Ã  jouer quand le joueur attaque (assignÃ© dans lâ€™Inspector)
    [SerializeField] AudioClip sfxAttack;

    

    // Composant AudioSource qui jouera les sons
    private AudioSource audioSource;

    // Valeur dâ€™entrÃ©e horizontale (âˆ’1 = gauche, 0 = immobile, 1 = droite)
    private float x;
    // Composant pour gÃ©rer lâ€™affichage du sprite (retourner Ã  gauche/droite)
    private SpriteRenderer spriteRenderer;
    // Composant pour gÃ©rer les animations du joueur
    private Animator animator;
    // Composant physique pour gÃ©rer les forces (notamment le saut)
    private Rigidbody2D rb;

    // Indique si le joueur doit sauter Ã  la prochaine frame physique
    private bool jump = false;

    void Awake()
    {
        // RÃ©cupÃ¨re les composants nÃ©cessaires attachÃ©s au GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        // MÃ©thode appelÃ©e au lancement, vide ici mais disponible pour init
    }

    // Update est appelÃ© une fois par frame (logique liÃ©e aux entrÃ©es joueur)
    void Update()
    {
        
        // ---- DÃ©placement horizontal ----
        x = Input.GetAxis("Horizontal"); // rÃ©cupÃ¨re lâ€™input clavier/flÃ¨ches
        animator.SetFloat("x", Mathf.Abs(x)); // anime la marche selon vitesse
        transform.Translate(Vector2.right * 7f * Time.deltaTime * x); // dÃ©place le joueur

        // ---- Orientation du sprite ----
        if (x > 0f) { spriteRenderer.flipX = false; } // regarde Ã  droite
        if (x < 0f) { spriteRenderer.flipX = true; }  // regarde Ã  gauche


        if (x==5)
        {
            jump = true; // signal quâ€™il faut sauter dans FixedUpdate
            audioSource.PlayOneShot(sfxJump); // joue le son du saut
        }



        // ---- Gestion du saut ----
        // Ancienne version commentÃ©e (force directe au moment de lâ€™appui)
        // if (Input.GetKeyDown(KeyCode.UpArrow)) { rb.AddForce(Vector2.up * 900f); }

        // Nouvelle version : dÃ©clenche un "flag" de saut
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump = true; // signal quâ€™il faut sauter dans FixedUpdate
            audioSource.PlayOneShot(sfxJump); // joue le son du saut
        }

        // ---- Animation dâ€™attaque ----
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("Attack", true); // lance lâ€™animation
            audioSource.PlayOneShot(sfxAttack); // joue le son du saut

        }
        else
        {
            animator.SetBool("Attack", false); // arrÃªte lâ€™animation
        }
    }

    // FixedUpdate est appelÃ© Ã  chaque frame physique (idÃ©al pour Rigidbody)
    private void FixedUpdate()
    {
        // DÃ©placement horizontal rÃ©pÃ©tÃ© ici (doublon avec Update)
        transform.Translate(Vector2.right * 1f * Time.deltaTime * x);
        
        // ---- Saut ----
        if (jump) // si le flag est actif
        {
            jump = false; // rÃ©initialise pour Ã©viter des sauts infinis

            audioSource.PlayOneShot(sfxJump); // rejoue le son du saut (âš  doublon aussi)

            rb.AddForce(Vector2.up * 900f); // applique une force vers le haut
        }
    }
    
}
