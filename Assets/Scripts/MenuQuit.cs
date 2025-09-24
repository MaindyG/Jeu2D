using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class MenuQuit : MonoBehaviour
{
    private Button button;

    public float reloadDelay = 2f;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClip);
    }

    private void TaskOnClip()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}