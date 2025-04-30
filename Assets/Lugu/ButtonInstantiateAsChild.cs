using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class ButtonInstantiateAsChild : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform parent;

    private void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
        {
            if (prefab != null && parent != null)
                Instantiate(prefab, parent);
        });
    }
}