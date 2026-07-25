using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    public float thrust_force = 4f;
    public float max_speed = 5f;
    public GameObject booster_flame;
    public UIDocument ui_document;

    private float elapsed_time = 0f;
    private float score = 0f;
    private float score_multiplier = 10f;
    private Rigidbody2D rb;
    private Label score_text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        score_text = ui_document.rootVisualElement.Q<Label>("ScoreLabel");
        //booster_flame = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // Caculate time alive

        UpdateScore();
        MovePlayer();
    }

    private void UpdateScore()
    {
        elapsed_time += Time.deltaTime;
        score = Mathf.FloorToInt(elapsed_time * score_multiplier);
        Debug.Log("Score: " + score);
        score_text.text = "Score: " + score;
    }

    private void MovePlayer()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            // Caculate mouse direction
            Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            Vector2 direction = (mouse_pos - transform.position).normalized;

            // vì spite cua con tau ban đầu hướng lên:
            // neu ban dau nhin sang phai nhu nhan vat thi dung transform.right
            transform.up = direction;

            // Move Player
            rb.AddForce(direction * thrust_force);
        }

        // clamp max speed
        if (rb.linearVelocity.magnitude > max_speed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * max_speed;
        }

        // animation
        if (Mouse.current.leftButton.wasPressedThisFrame == true)
        {
            booster_flame.SetActive(true);
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame == true)
        {
            booster_flame.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
