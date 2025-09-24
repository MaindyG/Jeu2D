// Script pour créer un effet de parallaxe sur un arrière-plan
using UnityEngine;
 
public class ParallaxBackground : MonoBehaviour
{
    public Transform cameraTransform; // Drag & drop la caméra principale ici dans l’Inspector
    public float parallaxEffect = 0.5f; 

    private Vector3 lastCameraPos; // Position précédente de la caméra

    void Start()
    {
        // Si la caméra n'est pas assignée, utilise la caméra principale
        if (!cameraTransform)
            cameraTransform = Camera.main.transform;
        lastCameraPos = cameraTransform.position;
    }

    void Update()
    {
        Vector3 delta = cameraTransform.position - lastCameraPos;
        transform.position += new Vector3(delta.x * parallaxEffect, delta.y * parallaxEffect, 0);
        lastCameraPos = cameraTransform.position;
    }
}