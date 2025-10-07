//=> Gestion des points de vie du joueur, prise de dégâts et mort

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour


{
    private Animator playerAnimator; // pour jouer l'animation de mort

    // Nombre de points de vie maximum du joueur (réglable dans l’Inspector).
    public int maxHealth = 5;

    // Points de vie courants (privé pour éviter des modifications externes accidentelles).
    private int currentHealth;

    // ⟵ délai avant reload
    public float reloadDelay = 2f;
    public string sceneToLoad = "GameOver"; // nom de la scène à charger en cas de mort

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
    }


    // Gère la mort du joueur : animations, désactivation d’input, rechargement de scène, etc.
    public void Die()
    {
        Debug.Log("Player est mort !");
        playerAnimator = GetComponent<Animator>();
        
        // Ici : désactiver le joueur, lancer une animation, recharger la scène, etc.
        // Exemple (selon ton architecture) :
        GetComponent<PlayerMove>().enabled = false;
        // animator.SetTrigger("Dead");
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // (Optionnel) désactiver l'input / jouer une anim / un fade-out ici…
        playerAnimator.SetTrigger("Dead");
        StartCoroutine(CoDelay(sceneToLoad, reloadDelay));

    }

    IEnumerator CoDelay(string name, float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        SceneManager.LoadScene(name);
    }
}
