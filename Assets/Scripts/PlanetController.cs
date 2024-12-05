using UnityEngine;
using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class PlanetController : MonoBehaviour
{
    [SerializeField] private float _constant1;
    [SerializeField] private Vector2 _velocity;
    [SerializeField] private bool _isCircularMotion;
    private float k;
    private float _constant3;
    public Transform Transform { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    
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

        float dT = Time.deltaTime;
        _velocity.x += (_constant1/MathF.Pow(distance, 2))*dT*sinB*k;
        _velocity.y += (_constant1/MathF.Pow(distance, 2))*dT*cosB*k;
        
        if (distance < 0.4) {
            GameObject.Destroy(gameObject);
        }

        Rigidbody.linearVelocity = new Vector2(_velocity.x*k, _velocity.y*k);

        Debug.Log(_constant1 / MathF.Pow(distance, 2) * dT * cosB * k+","+distance);
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

    private float kickPower = 1000;

    private void OnCollisionEnter2D(Collision2D other){
        Debug.Log ("A collider has made contact with the DoorObject Collider");
        Rigidbody.AddForce(other.collider.attachedRigidbody.linearVelocity * kickPower);
    }
}
