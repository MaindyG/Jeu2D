// => Classe qui permet au joueur de sauvegarder sa progression au niveau des checkpoints
using UnityEngine;
public class Checkpoint : MonoBehaviour
{
    
    
    // Quand le joueur entre en collision avec le check point
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerHealth>();
            player.SetCheckpoint(this.transform.position);
            Debug.Log("Checkpoint atteint !");
        }
    }

    
}