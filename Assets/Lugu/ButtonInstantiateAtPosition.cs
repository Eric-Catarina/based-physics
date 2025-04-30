using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class ButtonInstantiateAtPosition : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
        {
            if (prefab != null && spawnPoint != null)
                Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        });
    }
}