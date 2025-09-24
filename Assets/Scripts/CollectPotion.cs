// => Classe qui permet au joueur de recolter des potions de vie
using UnityEngine;
public class CollectPotion : MonoBehaviour
{
    
    public int healAmount = 1;      // Quantité de vie gagnée a chaque potion

    public AudioClip sfxPickup;  // Son joué lors de la collecte de la potion// Son de ramassage


    // Quand le joueur entre en collision avec la  potion
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        var hp = other.GetComponent<PlayerHealth>();
        if (hp) hp.AddHealth(healAmount);
        if (sfxPickup) AudioSource.PlayClipAtPoint(sfxPickup, transform.position, 0.8f);
        Destroy(gameObject);
    }

    
}