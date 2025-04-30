using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class ButtonChangeSceneString : MonoBehaviour
{
    [SerializeField] private string sceneName = "SampleScene";

    private void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { SceneManager.LoadScene(sceneName); });
    }
}
