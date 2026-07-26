using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    public float thrust_force = 4f;
    public float max_speed = 5f;
    public GameObject booster_flame;
    public UIDocument ui_document;
    public GameObject explosion_effect;
    public GameObject border_parent;
    public AudioSource explosion_sound;

    private float elapsed_time = 0f;
    private float score = 0f;
    private float score_multiplier = 10f;
    private Rigidbody2D rb;
    private Label score_text;
    private Button restart_button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        explosion_sound.Stop();
        rb = GetComponent<Rigidbody2D>();
        score_text = ui_document.rootVisualElement.Q<Label>("ScoreLabel");
        restart_button = ui_document.rootVisualElement.Q<Button>("RestartButton");
        restart_button.style.display = DisplayStyle.None;
        restart_button.clicked += ReloadScene;
        //booster_flame = GetComponent<GameObject>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Calculate score based on time alive

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
            // Calculate mouse direction
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
        explosion_sound.Play();
        Destroy(gameObject);
        border_parent.SetActive(false);
        Instantiate(explosion_effect, transform.position, transform.rotation);
        restart_button.style.display = DisplayStyle.Flex;
    }

    private void ReloadScene()
    {
        explosion_sound.Stop();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
