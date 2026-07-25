using System.Security.Cryptography;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float min_size = 0.5f;
    public float max_size = 3.0f;
    public float min_speed = 100f;
    public float max_speed = 350f;
    public float max_spin_speed = 10f;
    public GameObject impact_effect;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        float random_size = Random.Range(min_size, max_size);
        transform.localScale = new Vector3(random_size, random_size, 1);

        rb = GetComponent<Rigidbody2D>();

        float random_speed = Random.Range(min_speed, max_speed) / random_size;
        Vector2 random_direction = Random.insideUnitCircle;
        rb.AddForce(random_direction * random_speed);

        float random_torque = Random.Range(-max_spin_speed, max_spin_speed);
        rb.AddTorque(random_torque);
    }

    // Update is called once per frame
    private void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 contact_point = collision.GetContact(0).point;
        GameObject temp_impact_effect = Instantiate(impact_effect, transform.position, transform.rotation);
        Destroy(temp_impact_effect, 1f);
    }
}
