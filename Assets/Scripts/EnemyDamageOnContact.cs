// EnemyDamageOnContact.cs
// -----------------------------------------------------------------------------
// À METTRE SUR : l'enfant "Hitbox" de l'ennemi (un GameObject avec un Collider2D)
// - Le "Body" de l'ennemi (parent) garde un Collider2D NON-TRIGGER pour bloquer
//   physiquement le joueur (évite de le traverser).
// - La "Hitbox" (enfant) a un Collider2D avec isTrigger = true et ce script.
// PRÉREQUIS SCÈNE :
// - Le Player a un Rigidbody2D (Dynamic ou Kinematic) + un Collider2D NON-TRIGGER.
// - Le Player porte le Tag "Player" (sur la racine qui a le Rigidbody2D).
// - Physics 2D > Layer Collision Matrix : Player <-> Enemy/Hitbox cochés.
// COMPORTEMENT :
// - Inflige des dégâts au Player à l'entrée dans la hitbox (OnTriggerEnter2D)
//   avec un "cooldown" pour éviter le spam si on re-rentre rapidement.
// - Si tu veux des dégâts en continu pendant le contact, ajoute un OnTriggerStay2D
//   qui réutilise TryHit(...) ou copie la logique (voir NOTE en bas).
// -----------------------------------------------------------------------------

using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyDamageOnContact : MonoBehaviour
{
    /// <summary>
    /// Tag attendu sur la racine du joueur (celle qui porte le Rigidbody2D et PlayerHealth).
    /// </summary>
    public string playerTag = "Player";

    /// <summary>
    /// Dégâts infligés à chaque "hit".
    /// </summary>
    public int damage = 25;

    /// <summary>
    /// Délai minimal entre deux coups sur le même joueur (en secondes).
    /// Évite d'infliger plusieurs hits dans le même instant lors d'entrées/sorties rapides.
    /// </summary>
    public float hitCooldown = 0.4f;  // évite le spam

    // Mémorise le dernier moment où un coup a été porté (par ennemi).
    // Pour un cooldown par CIBLE, utiliser un Dictionary< PlayerHealth, float > (voir NOTE).
    float lastHitTime = -999f;

    void Reset()
    {
        // Comme il s'agit d'une hitbox, on force le collider en "trigger"
        var col = GetComponent<Collider2D>();
        if (col) col.isTrigger = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Quand un collider entre dans la hitbox
        if (other.CompareTag("Player"))
        {
            // C'est le joueur (on suppose que le tag est sur la racine)
            var hp = other.GetComponent<PlayerHealth>();

            // Essaie de lui infliger des dégâts
            if (hp) hp.TakeDamage(damage);

            // Log de debug
            Debug.Log($"Ennemi inflige {damage} dégâts! HP restant: {hp.maxHealth - damage}");

        }
    }


}
