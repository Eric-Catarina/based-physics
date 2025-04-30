using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class ButtonLoadAdditiveSceneName : MonoBehaviour
{
    [SerializeField] private string sceneName = "";

    private void Start() => GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => 
    {
        if(!string.IsNullOrEmpty(sceneName))
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    });
}