using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class ButtonUnloadAdditiveSceneName : MonoBehaviour
{
    [SerializeField] private string sceneName = "";

    private void Start() => GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => 
    {
        if(!string.IsNullOrEmpty(sceneName))
            SceneManager.UnloadSceneAsync(sceneName);
    });
}