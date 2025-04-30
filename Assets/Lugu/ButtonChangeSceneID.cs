using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class ButtonChangeSceneID : MonoBehaviour
{


    [SerializeField] private int sceneID = 0;
    private void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { SceneManager.LoadScene(sceneID); });
    }
}
