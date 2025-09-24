// => Tue le joueur au contact d'un volume de feu)
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
public class AcidKill : MonoBehaviour
{
    [Tooltip("Tag du volume d'acide (ex: Acid).")]
    public string feuTag = "Fire"; // mets "Acid" si tu préfères

    [Tooltip("Dégâts instantanés à l'entrée. 9999 = mort instantanée.")]
    public int damageOnEnter = 9999; //Dommages infligés à l'entrée

    [Tooltip("Optionnel: dégâts par seconde tant qu'on reste dedans (0 = off).")]
    public int damagePerSecond = 0; //Dommages inflictés par seconde

    private Health playerHealth; // référence au script de vie du joueur
    private Animator playerAnimator; // pour jouer l'animation de mort

    // Clip audio Ã  jouer quand le joueur meurt
    [SerializeField] AudioClip sfxDeath;
    // Composant AudioSource qui jouera les sons
    private AudioSource audioSource;

    public float reloadDelay = 1f;
    public string sceneToLoad = "GameOver";

    void Start()
    {
        // Récupère Health + Animator sur le player une fois pour toutes
        var player = FindAnyObjectByType<PlayerMove>(); // on part de ton script existant :contentReference[oaicite:2]{index=2}
        if (player)
        {
            playerHealth = player.GetComponent<Health>();
            playerAnimator = player.GetComponent<Animator>();
        }
    }

    // Quand le joueur entre en collision avec l’acide
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(feuTag)) return;

        if (playerHealth)
        {
            if (damageOnEnter >= 9999)
            {
                playerHealth.Kill();
            }
            else playerHealth.TakeDamage(damageOnEnter);
        }
        if (playerAnimator) playerAnimator.SetTrigger("Dead");
        StartCoroutine(CoDelay(sceneToLoad, reloadDelay));



    }
    IEnumerator CoDelay(string name, float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        SceneManager.LoadScene(name);
    }



    // Tant que le joueur reste dans le feu (dégâts par seconde)
    void OnTriggerStay2D(Collider2D other)
    {
        if (damagePerSecond <= 0 || !other.CompareTag(feuTag) || playerHealth == null || playerHealth.IsDead) return;
        playerHealth.TakeDamage(Mathf.CeilToInt(damagePerSecond * Time.deltaTime));
    }
}
