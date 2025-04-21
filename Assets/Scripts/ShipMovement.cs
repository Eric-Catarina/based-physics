using TMPro;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [Header("Propulsion Settings")]
    public float mainThrust = 10f;
    public float forwardThrust = 7f;
    public float backwardThrust = 3f;
    public float rotationTorque = 5f;
    public float autoStabilization = 2f;

    [Header("Fuel Settings")]
    public float maxFuel = 100f;
    public float fuelConsumption = 1f;
    public GameObject fuelText;
    
    private Rigidbody rb;
    private float currentFuel;
    private bool isStabilizing = true;

    public GameObject LoseScreenGameObject;
    
    public GameObject[] explosionVFXs;
    public GameObject successVFX;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentFuel = maxFuel;
        rb.centerOfMass = Vector3.zero; // Melhora a estabilidade
    }

    void Update()
    {
        HandleControls();
        StabilizeRotation();
        UpdateFuel();
    }

    void HandleControls()
    {
        // Propulsão principal (Espaço)
        if (Input.GetKey(KeyCode.Space)) ApplyThrust(transform.up, mainThrust);

        // Movimento para frente (Seta Cima)
        if (Input.GetKey(KeyCode.UpArrow)) ApplyThrust(transform.forward, forwardThrust);

        // Movimento para trás (Seta Baixo)
        if (Input.GetKey(KeyCode.DownArrow)) ApplyThrust(-transform.forward, backwardThrust);

        // Rotação lateral (Setas Esquerda/Direita)
        float rotation = Input.GetAxis("Horizontal");
        rb.AddTorque(transform.up * rotationTorque * rotation);
    }

    void ApplyThrust(Vector3 direction, float force)
    {
        if (currentFuel <= 0) return;
        
        rb.AddForce(direction * force);
        currentFuel -= fuelConsumption * Time.deltaTime;

        // Pequena inclinação durante a propulsão
        if (direction == transform.forward)
            rb.AddTorque(transform.right * -force * 0.1f); // Inclina para frente
        else if (direction == -transform.forward)
            rb.AddTorque(transform.right * force * 0.05f); // Inclina para trás
    }

    void StabilizeRotation()
    {
        if (!isStabilizing) return;

        // Suaviza a rotação usando Quaternion.Slerp
        Quaternion targetRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            autoStabilization * Time.deltaTime
        );

        // Amortece a rotação manualmente
        rb.angularVelocity *= 0.95f;
    }

    void UpdateFuel()
    {
        // Atualiza a UI do combustível (implemente sua própria UI aqui)
        if (fuelText != null)
        {
            fuelText.GetComponent<TextMeshProUGUI>().text = "Fuel: " + currentFuel.ToString("F1");
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.relativeVelocity.magnitude > 10f)
        {
            Debug.Log("Colidiu");
            Invoke("ShowLoseScreen", 1f);
            
            Instantiate(explosionVFXs[Random.Range(0, explosionVFXs.Length)], transform.position, transform.rotation);
        }
        else if (collision.relativeVelocity.magnitude > 1f)
        {
            Instantiate(successVFX, transform.position, transform.rotation);
        }
        if (collision.relativeVelocity.magnitude > 1f)
        {
            isStabilizing = false;
            Invoke("EnableStabilization", 2f);
        }
    }

    void EnableStabilization() => isStabilizing = true;
    
    public void RestartScene() => UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    
    private void ShowLoseScreen() => LoseScreenGameObject.SetActive(true);
}