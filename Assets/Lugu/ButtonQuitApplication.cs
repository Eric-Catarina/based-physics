using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class ButtonQuitApplication : MonoBehaviour
{
    private void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        });
    }
}