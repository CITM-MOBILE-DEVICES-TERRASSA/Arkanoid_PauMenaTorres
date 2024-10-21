using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BouncyBall : MonoBehaviour
{
    public float minY = -5.5f;
    public float maxVelocity = 15f;
    Rigidbody2D rb;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxScoreText; // Texto para mostrar el puntaje máximo
    public GameObject[] livesImage;
    public GameObject gameOverPanel;
    public GameObject youWinPanel;
    public int brickCount;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        brickCount = GameObject.FindObjectOfType<LevelGenerator>().transform.childCount; 
        rb.velocity = Vector2.down * 7.5f;

        // Actualizamos el texto del puntaje al iniciar
        scoreText.text = GameManager.instance.score.ToString("00000");
        maxScoreText.text = GameManager.instance.maxScore.ToString("00000"); // Mostramos el puntaje máximo
        UpdateLivesUI();
    }

    void Update()
    {
        if (transform.position.y < minY)
        {
            if (GameManager.instance.lives <= 0)
            {
                GameOver();
            }
            else 
            {
                GameObject paddle = GameObject.Find("Paddle");
                if (paddle != null)
                {
                    Vector3 paddlePosition = paddle.transform.position;
                    transform.position = new Vector3(paddlePosition.x, paddlePosition.y, 0);
                }
                else
                {
                    transform.position = Vector3.zero;
                }
                rb.velocity = Vector2.down * 7.5f;
                GameManager.instance.lives--;
                UpdateLivesUI();
            }
        }

        if (brickCount <= 0)
        {
            youWinPanel.SetActive(true);
            Time.timeScale = 0;
        }

        if (brickCount > 0)
        {
            Time.timeScale = 1;
        }

        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity); 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            Brick brick = collision.gameObject.GetComponent<Brick>();

            if (brick != null)
            {
                brick.TakeDamage();

                if (brick.resistance <= 0)
                {
                    Destroy(collision.gameObject);
                    GameManager.instance.score += 50;
                    scoreText.text = GameManager.instance.score.ToString("00000");

                    // Actualizamos el puntaje máximo si es necesario
                    GameManager.instance.UpdateMaxScore();
                    maxScoreText.text = GameManager.instance.maxScore.ToString("00000");

                    brickCount--;
                }
            }
        }
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
        Destroy(gameObject);
    }

    void UpdateLivesUI()
    {
        for (int i = 0; i < livesImage.Length; i++)
        {
            if (i < GameManager.instance.lives)
            {
                livesImage[i].SetActive(true);
            }
            else
            {
                livesImage[i].SetActive(false);
            }
        }
    }
}
