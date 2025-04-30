using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class ButtonInstantiatePrefab : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
        {
            if (prefab != null) Instantiate(prefab);
        });
    }
}