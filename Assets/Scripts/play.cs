using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class TestClick : MonoBehaviour
{
    private Button button;

    public float reloadDelay = 2f;
    public string sceneToLoad = "MainGame";
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClip);
    }

    private void TaskOnClip()
    {
        Debug.Log("You have clicked the button!");
        SceneManager.LoadScene(sceneToLoad);

    }
}