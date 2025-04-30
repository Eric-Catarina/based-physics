using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class ButtonActivateGameObject : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;

    private void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
        {
            if (targetObject != null) targetObject.SetActive(true);
        });
    }
}
