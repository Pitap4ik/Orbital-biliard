using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public Rigidbody2D Rigidbody { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float VelocityX = Input.GetAxis("Horizontal")*10f;
        float VelocityY = Input.GetAxis("Vertical")*10f;
        Rigidbody.linearVelocity = new Vector2(VelocityX, VelocityY);
    }
}
