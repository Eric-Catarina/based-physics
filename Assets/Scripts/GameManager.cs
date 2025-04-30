using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro; // Certifique-se de ter o TextMesh Pro instalado

public class GameManager : MonoBehaviour
{
    [Header("Pontuação e Condições")]
    public int scoreToWin = 5;
    public int currentScore = 0;

    [Header("UI")]
    public GameObject victoryPanel;
    public TextMeshProUGUI victoryText; // Use TextMeshProUGUI
    public GameObject defeatPanel;
    public TextMeshProUGUI enemiesLeftText; // Use TextMeshProUGUI
    public int totalEnemies = 5; // Total de inimigos no início do jogo
    private int enemiesAlive; // Contador de inimigos vivos

    [Header("Eventos")]
    public UnityAction OnEnemyDied;

    // Singleton para fácil acesso de outros scripts
    public static GameManager Instance;

    void Awake()
    {
        scoreToWin = totalEnemies;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(false);
        enemiesAlive = totalEnemies; // Inicializa a contagem de inimigos vivos
        UpdateEnemiesLeftText(); // Atualiza o texto inicial

        OnEnemyDied += IncreaseScore; // Subscreve o evento
    }

    // Função para aumentar a pontuação (chamada por Enemy.cs quando um inimigo morre)
    public void IncreaseScore()
    {
        currentScore++;
        enemiesAlive--;
        UpdateEnemiesLeftText();

        Debug.Log("Pontuação: " + currentScore);

        if (currentScore >= scoreToWin)
        {
            WinGame();
            
        }
    }

    //Função chamada pelo Player quando ele cai no void
    public void LoseGame()
    {
        Time.timeScale = 0; // Para o tempo
        // defeatPanel.SetActive(true);
        defeatPanel.GetComponent<Juice>().PlayActivationAnimation();
        victoryText.color = Color.red;
        Debug.Log("Você Perdeu!");
    }

    // Função para ativar o painel de vitória
    void WinGame()
    {
        Time.timeScale = 0; // Para o tempo
        defeatPanel.GetComponent<Juice>().PlayActivationAnimation();

        victoryText.color = Color.green;
        victoryText.text = "You Win!";
        Debug.Log("Você Ganhou!");
    }

    // Atualiza o texto de inimigos restantes
    void UpdateEnemiesLeftText()
    {
        enemiesLeftText.text = (totalEnemies - currentScore) + "/" + totalEnemies + " Enemies left";
        //enemiesLeftText.text = enemiesAlive + "/" + totalEnemies + " Enemies left"; // Alternativamente, usar o contador de inimigos vivos
    }

    // Resetar o Tempo ao destruir o GameObject (se for persistente entre as cenas)
    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
}