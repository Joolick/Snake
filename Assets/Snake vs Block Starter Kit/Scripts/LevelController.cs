using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    public Text pointsText;

    public GameObject gamePanel;
    public GameObject startPanel;
    public GameObject gameOverPanel;

    public float gameSpeed = 2;

    public int obstaclesAmount = 6;

    public float damageTime = 0.1f;

    public Color easyColor, mediumColor, hardColor;

    public float obstaclesDistance = 13;

    public ObjectPool pickupPool;
    public Vector2 xLimit;

    public float multiplier = 1;
    public float cicleTime = 10;

    public AudioClip clickSound;

    public bool gameOver = true;

    private int points;

    private Transform player;
    private void Awake()
    {
        instance = this;
    }

    IEnumerator Start()
    {
        player = FindObjectOfType<Player>().transform;

        while (gameOver)
        {
            yield return null;
        }

        SpawnPickups();

        InvokeRepeating("IncreaseDifficulty", cicleTime, cicleTime);

    }

    private void Update()
    {
        
    }

    public void StartGame()
    {
        AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
        gamePanel.SetActive(true);
        startPanel.SetActive(false);
        gameOver = false;
    }

    public void GameOver()
    {
        gameOver = true;
        gameSpeed = 0;
        gameOverPanel.SetActive(true);
    }

    public void ReloadScene()
    {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Score(int amount)
    {
        points += amount;

        pointsText.text = points.ToString();
    }

    void IncreaseDifficulty()
    {
        obstaclesAmount += 2;

        multiplier *= 1.1f;
    }
    void SpawnPickups()
    {
        pickupPool.GetObject().transform.position = new Vector2(Random.Range(xLimit.x, xLimit.y), player.position.y + 15);

        Invoke("SpawnPickups", Random.Range(1f, 3f));
    }


}
