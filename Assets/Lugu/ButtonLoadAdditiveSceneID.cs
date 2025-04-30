using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class ButtonLoadAdditiveSceneID : MonoBehaviour
{
    [SerializeField] private int sceneID = 0;

    private void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(sceneID, LoadSceneMode.Additive);
        });
    }
}