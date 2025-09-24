// => Classe qui permet à l’ouverture et la fermeture d’une porte
using UnityEngine;
 
public class DoorOpener : MonoBehaviour
{
    public GameObject door;      // Assigner la porte dans l’Inspector

    private Animator doorAnimator; // Référence à l’Animator de la porte
 
    //
    void Start()
    {
        doorAnimator = door.GetComponent<Animator>();
    }
 

    // Quand le joueur entre en collision avec la zone de la porte
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetTrigger("OpenDoor");
        }
    }
 
    // Quand le joueur sort de la collision avec la zone de la porte
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetTrigger("CloseDoor");
        }
    }
}
