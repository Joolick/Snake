using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float speed = 5;

    public Text livesText;
    private float mouseDistance;
    private Rigidbody2D rb;

    private float lastYPos;

    private bool sliding;
    private int dir;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        lastYPos = transform.position.y;
    }

   
    void Update()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float xPos = worldPoint.x;

        mouseDistance = Mathf.Clamp(xPos - transform.position.x, -1, 1);

        if (transform.position.y > lastYPos + 5)
        {
            LevelController.instance.Score(10);
            lastYPos = transform.position.y;
        }
    }
    private void FixedUpdate()
    {
        if (LevelController.instance.gameOver)
            return;

        if (!sliding)
            rb.velocity = new Vector2(mouseDistance * speed, LevelController.instance.gameSpeed * LevelController.instance.multiplier);
        else
            rb.velocity = new Vector2(dir * 2.5f, LevelController.instance.gameSpeed * LevelController.instance.multiplier);
    }

    public void SetText(int amount)
    {
        livesText.text = amount.ToString();

    }
    public void TakeDamage()
    {
        if (LevelController.instance.gameOver)
            return;

        int children = transform.childCount;
        if (children <= 1)
        {
            LevelController.instance.GameOver();
        }
        else
        {
            Destroy(transform.GetChild(children - 1).gameObject);
        }

        SetText(children - 1);
}

    public void Slide(int direction)
    {
        sliding = true;
        dir = direction;
        Invoke("SetSlideToFalse", 0.25f);
    }

    void SetSlideToFalse()
    {
        sliding = false;
    }

}
