using UnityEngine;

public class EXPERIMENTALSCRIPT : MonoBehaviour
{
    public Rigidbody2D Rigidbody { get; private set; }

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Rigidbody.AddForce(new Vector2(50f, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Rigidbody.linearVelocity);
    }
}
