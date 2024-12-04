using UnityEngine;
using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _constant1;
    [SerializeField] private Vector2 _velocity;
    [SerializeField] private bool _isCircularMotion;
    private float k;
    private float _constant3;
    public Transform Transform { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    //public float VelocityX { get; private set; }
    //public float VelocityY { get; private set; } 
    
    void Start()
    {
        Transform = GetComponent<Transform>();
        Rigidbody = GetComponent<Rigidbody2D>();

        if (_isCircularMotion){
            _velocity = GetCircularMotionVelocity(Transform.position, _constant1);
        }

        _constant3 = _velocity.x * GetDistance(Transform.position);
    }

    void FixedUpdate()
    {
        k = GameObject.Find("Cosmic Slider").GetComponent<Slider>().value;
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
<<<<<<< Updated upstream
        _velocity.x += (_constant1/MathF.Pow(distance, 2))*dT*sinB*k;
        _velocity.y += (_constant1/MathF.Pow(distance, 2))*dT*cosB*k;
=======
        VelocityX += (_constant1/MathF.Pow(distance, 2))*dT*sinB*k;
        VelocityY += (_constant1/MathF.Pow(distance, 2))*dT*cosB*k;
        Debug.Log(_constant1 / MathF.Pow(distance, 2) * dT * cosB * k+","+distance);
        if (distance < 0.2) {
            GameObject.Destroy(gameObject);
        }
>>>>>>> Stashed changes

        Rigidbody.linearVelocity = new Vector2(_velocity.x*k, _velocity.y*k);
        //Debug.Log(velocity +","+  currentX+ ","+ cosA); 
    }

    public float GetDistance(Vector2 pos){
        return MathF.Sqrt(MathF.Pow(pos.x, 2) + MathF.Pow(pos.y, 2));
    }

    public Vector2 GetCircularMotionVelocity(Vector2 position, float constant1){
        float distance = GetDistance(position);
        float velocityX = -MathF.Sqrt(constant1/distance) * position.y / distance;
        float velocityY = -MathF.Sqrt(constant1/distance) * position.x / distance;
        return new Vector2(velocityX, velocityY);
    }
}
