using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{

    public float thrust_force = 4f;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            Debug.Log("Mouse position: " + mouse_pos);

            Vector2 direction = (mouse_pos - transform.position).normalized;
            //vì spite cua con tau ban đầu hướng lên:
            // neu ban dau nhin sang phai nhu nhan vat thi dung transform.right
            transform.up = direction;

            rb.AddForce(direction * thrust_force);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
