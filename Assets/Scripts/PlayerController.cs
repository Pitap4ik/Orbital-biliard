using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _initialX;
    [SerializeField] private float _initialY;
    [SerializeField] private float _constant1;
    [SerializeField] private float _constant2;
    private float _constant3;
    private Vector2 _initialVelocity;
    public Transform Transform { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public float VelocityX { get; private set; }
    public float VelocityY { get; private set; } 
    
    void Start()
    {
        Transform = GetComponent<Transform>();
        Rigidbody = GetComponent<Rigidbody2D>();

        Vector2 initialPosition = new Vector2(_initialX, _initialY);
        Transform.position = initialPosition;

        _initialVelocity = new Vector2(MathF.Sqrt(_constant1 * (_constant2 - 1 / GetDistance(initialPosition))), 0f);
        _constant3 = _initialVelocity.x * GetDistance(_initialVelocity);

        Debug.Log(_constant3);
    }

    void FixedUpdate()
    {
        float currentX = Transform.position.x;
        float currentY = Transform.position.y;
        float distance = GetDistance(Transform.position);

        float velocity = MathF.Sqrt(_constant1 * (_constant2 - 1 / distance));
        float cosA = _constant3 / (distance * velocity);
        float sinA = MathF.Sqrt(1 - MathF.Pow(cosA, 2));
        float cosB = currentX/distance;
        float sinB = currentY/distance*-1;
        float sinAB = sinA * cosB + sinB * cosA;
        float cosAB = MathF.Sqrt(1 - MathF.Pow(sinAB, 2));
        
        VelocityX = velocity * sinAB;
        VelocityY = velocity * cosAB;
        Rigidbody.linearVelocity = new Vector2(VelocityX, VelocityY);

        Debug.Log(cosA);
    }

    public float GetDistance(Vector2 pos){
        return MathF.Sqrt(MathF.Pow(pos.x, 2) + MathF.Pow(pos.y, 2));
    }
}
