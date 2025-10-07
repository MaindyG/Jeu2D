using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class Menu : MonoBehaviour
{
    [Header("Nom de la scène de jeu")]
    public string gameSceneName = "MainGame"; // Nom de la scène de jeu à charger
    // Assignée au bouton "Jouer"
    public void OnPlay()
    {
        // Lance la scene pour jouer
        if (!string.IsNullOrEmpty(gameSceneName))
        {
            SceneManager.LoadScene(gameSceneName);
        }
        else
        {
            Debug.LogError("[MainMenu] gameSceneName est vide.");
        }
    }
    // Assignée au bouton "Quitter"
    public void OnQuit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
        Debug.Log("Quitter le jeu.");
    }
}

