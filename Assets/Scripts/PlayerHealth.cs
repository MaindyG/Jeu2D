//=> Gestion des points de vie du joueur, prise de dégâts et mort

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour


{
    private Animator playerAnimator; // pour jouer l'animation de mort

    // Nombre de points de vie maximum du joueur (réglable dans l’Inspector).
    public int maxHealth = 100;
    public int chances = 3; // Nombre de chances (vies) du joueur

    // Points de vie courants 
    public int currentHealth;
    public int chancesRestantes;



    // ⟵ délai avant reload
    public float reloadDelay = 2f;
    public string sceneToLoad = "GameOver"; // nom de la scène à charger en cas de mort

    public string sceneToRestart = "MainGame"; // nom de la scène à charger en cas de mort
    public Vector3 respawnPoint; // point de respawn du joueur
    void Start()
    {
        currentHealth = maxHealth;
        chancesRestantes = chances;
        respawnPoint = transform.position; // Initialiser le point de respawn à la position
        playerAnimator = GetComponent<Animator>();
    }

    public void SetCheckpoint(Vector3 pos)
    {
        respawnPoint = pos;
    }

    // Awake est appelé au chargement du GameObject (avant Start).
    // On initialise les PV courants au maximum.
    void Awake() => currentHealth = maxHealth;

    // Méthode à appeler quand le joueur subit des dégâts.
    // 'dmg' représente la quantité de dégâts à retirer.
    public void TakeDamage(int dmg)
    {
        // On soustrait les PV.
        currentHealth -= dmg;

        // Log de debug pour visualiser la perte de PV dans la Console.
        Debug.Log("Player prend " + dmg + " dégâts. HP restants = " + currentHealth);

        // Si les PV tombent à 0 ou moins, on déclenche la mort.
        if (currentHealth <= 0) Die();


    }

    public void AddHealth(int v)
    {
        currentHealth = Mathf.Clamp(currentHealth + v, 0, maxHealth);
        Debug.Log("Player récupère " + v + " points de vie. HP = " + currentHealth);
    }


    // Gère la mort du joueur : animations, désactivation d’input, rechargement de scène, etc.
    public void Die()
    {
        Debug.Log("Player est mort ! | Chances restantes: " + (chancesRestantes - 1));

        // Ici : désactiver le joueur, lancer une animation, recharger la scène, etc.
        // Exemple (selon ton architecture) :
        GetComponent<PlayerMove>().enabled = false;
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // (Optionnel) désactiver l'input / jouer une anim / un fade-out ici…
        playerAnimator.SetTrigger("Dead");
        if (chancesRestantes > 1)
        {
            chancesRestantes--;
            // Réinitialiser la position du joueur au point de respawn
            transform.position = respawnPoint;
            currentHealth = maxHealth; // Réinitialiser la santé
            GetComponent<PlayerMove>().enabled = true; // Réactiver le contrôle du joueur
            

            // Reset des animations
            playerAnimator.ResetTrigger("Dead");



      
            Debug.Log("Respawn! Chances restantes: " + chancesRestantes);
        }
        else
        {
            StartCoroutine(CoDelay(sceneToLoad, reloadDelay));
        }
    }

    IEnumerator CoDelay(string name, float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        SceneManager.LoadScene(name);
    }
   
}
