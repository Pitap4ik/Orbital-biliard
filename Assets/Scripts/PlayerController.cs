using UnityEngine;
using System;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _initialX;
    [SerializeField] private float _initialY;
    [SerializeField] private float _constant1;
    [SerializeField] private float _constant2;
    [SerializeField] private float VelocityX;
    [SerializeField] private float VelocityY;
    private float _constant3;
    private Vector2 _initialVelocity;
    public Transform Transform { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    //public float VelocityX { get; private set; }
    //public float VelocityY { get; private set; } 
    
    void Start()
    {
        Transform = GetComponent<Transform>();
        Rigidbody = GetComponent<Rigidbody2D>();

        Vector2 initialPosition = new Vector2(_initialX, _initialY);
        Transform.position = initialPosition;

        _initialVelocity = new Vector2(VelocityX, VelocityY);
          _constant3 = _initialVelocity.x * GetDistance(Transform.position);

        Debug.Log(_initialVelocity.x);
    }

    void FixedUpdate()
    {
        float currentX = Transform.position.x;
        float currentY = Transform.position.y;
        float distance = GetDistance(Transform.position);
        float sinB = -currentX / distance;
        float cosB = currentY / distance * -1;

        /*float velocity = MathF.Sqrt(_constant1 * (-1/_constant2 +1 / distance));
        float cosA = _constant3 / (distance*sinB * velocity);
        if (cosA > 1f) { cosA= 1f; }
        if (cosA < -1f) { cosA = -1f; }
        float sinA = MathF.Sqrt(1 - MathF.Pow(cosA, 2));

        if (sinB < 0f) { sinA = -sinA; }
        if (cosB < 0f) { cosA = -cosA; }

        float sinAB = sinA * cosB + sinB * cosA;
        float cosAB = MathF.Sqrt(1 - MathF.Pow(sinAB, 2));

        VelocityX = velocity * sinAB;
        VelocityY = velocity * cosAB;*/

        float dT = Time.deltaTime;

        VelocityX +=(_constant1/ MathF.Pow(distance, 2)) *dT* sinB;

        VelocityY += (_constant1 / MathF.Pow(distance, 2)) * dT*cosB;


        Rigidbody.linearVelocity = new Vector2(VelocityX, VelocityY);

        //Debug.Log(velocity +","+  currentX+ ","+ cosA); 
    }

    public float GetDistance(Vector2 pos){
        return MathF.Sqrt(MathF.Pow(pos.x, 2) + MathF.Pow(pos.y, 2));
    }
}
