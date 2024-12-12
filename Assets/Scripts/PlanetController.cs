using UnityEngine;
using System;
using UnityEngine.UI;

public class PlanetController : MonoBehaviour
{
    [SerializeField] private float _constant1;
    [SerializeField] private Vector2 _velocity;
    [SerializeField] private bool _isCircularMotion;
    [SerializeField] private bool _isDraggable;
    [SerializeField] private float _kickPower;
    [SerializeField] private GameObject _pointOfGravitation;
    [SerializeField] private bool _clockwise;
    [SerializeField] private float _conservedEnergy; 
    private CanvasController _canvasController;
    private Vector3 _mousePosition;
    public Transform Transform { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public float KValue { get; private set; }
    public bool IsDraggable { get => _isDraggable; set => _isDraggable = value; }
    public float ConservedEnergy { get => _conservedEnergy; set => _conservedEnergy = value; }

    void Start()
    {
        Transform = GetComponent<Transform>();
        Rigidbody = GetComponent<Rigidbody2D>();
        _canvasController = GameObject.Find("Canvas").GetComponent<CanvasController>();
        _canvasController.EnergyInputField.onValueChanged.AddListener(UpdateConservedEnergy);
        _canvasController.KValueSlider.onValueChanged.AddListener(UpdateKValue);
        KValue = _canvasController.KValueSlider.value;

        if (_isCircularMotion){
            _velocity = GetCircularMotionVelocity(Transform.position, _constant1);
        }
    }

    private void FixedUpdate()
    {
        float currentX = Transform.position.x;
        float currentY = Transform.position.y;
        float distance = GetDistance(Transform.position);
        float sinB = -currentX / distance;
        float cosB = currentY / distance * -1;

        float dT = Time.deltaTime;
        _velocity.x += (_constant1/MathF.Pow(distance, 2))*dT*sinB*KValue;
        _velocity.y += (_constant1/MathF.Pow(distance, 2))*dT*cosB*KValue;
        
        if (distance < 1) {
            GameObject.Destroy(gameObject);
        }

        Rigidbody.linearVelocity = new Vector2(_velocity.x*KValue, _velocity.y*KValue);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        float mOther = other.collider.attachedRigidbody.mass;
        float mSelf = Rigidbody.mass;
        Vector2 VelSelf = Rigidbody.linearVelocity;
        Vector2 VelOther = other.collider.attachedRigidbody.linearVelocity;
        Vector2 CoorOther = other.transform.position;
        Vector2 CoorSelf = Transform.position;

        Vector2 deltapos = new Vector2(CoorOther.x-CoorSelf.x, CoorOther.y-CoorSelf.y);

        Vector2 VelSelfx1y1 = NormalAndPerpVelocity(VelSelf, deltapos);
        Vector2 VelOtherx1y1 = NormalAndPerpVelocity(VelSelf, deltapos);

        float Py1 = mOther * VelOtherx1y1.y + mSelf * VelSelfx1y1.y;

        float E = (MathF.Pow(VelOtherx1y1.y, 2)+ MathF.Pow(VelOtherx1y1.x, 2)) * mOther / 2 + (MathF.Pow(VelSelfx1y1.y, 2) + MathF.Pow(VelSelfx1y1.x, 2)) * mSelf;

        float ExAfter = (MathF.Pow(VelOtherx1y1.x, 2)) * mOther / 2 + (MathF.Pow(VelSelfx1y1.x, 2)) * mSelf;

        float Edif = E - ExAfter;

        float a = (mSelf/2+MathF.Pow(mSelf,2)/(2*mOther));

        float b = mSelf * Py1 / mOther;

        float c = MathF.Pow(Py1,2)/(2*mOther)-Edif;

        float D = MathF.Pow(b, 2) - 4 * a * c;


        if (MathF.Abs((-b + Mathf.Sqrt(D)) / (2 * a) - VelOtherx1y1.y) >= MathF.Abs((-b - Mathf.Sqrt(D)) / (2 * a) - VelOtherx1y1.y))
        {
            VelOtherx1y1.y = (-b + Mathf.Sqrt(D)) / (2 * a);

        }
        else
        {
            VelOtherx1y1.y = (-b - Mathf.Sqrt(D)) / (2 * a);
        }


        Rigidbody.linearVelocity = ReverseNormalAndPerpVelocity(VelOtherx1y1, deltapos);

        Debug.Log("A collider has made contact with the DoorObject Collider");
        Debug.Log(VelOtherx1y1.y);
        //Rigidbody.AddForce(other.collider.attachedRigidbody.linearVelocity * _kickPower);
    }

    void OnMouseDown()
    {
        _mousePosition = Input.mousePosition - GetMousePosition();
    }

    void OnMouseDrag()
    {
        if (IsDraggable)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - _mousePosition);
        }
    }

    private void OnMouseUp()
    {
        if (IsDraggable){
            _velocity = GetCircularMotionVelocity(Transform.position, _constant1);
        }
    }

    Vector3 GetMousePosition()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    public Vector2 NormalAndPerpVelocity(Vector2 v,Vector2 deltapos)
    {
        float l = GetDistance(deltapos);
        float Vx1=v.x*(deltapos.y/l)-v.y*(deltapos.x/l);
        float Vy1 = v.x * (deltapos.x / l) + v.y * (deltapos.y / l);
        return new Vector2(Vx1, Vy1);// Vx1 is pependicular 
    }

    public Vector2 ReverseNormalAndPerpVelocity(Vector2 v, Vector2 deltapos)
    {
        float l = GetDistance(deltapos);
        float Vx1 = v.x * (deltapos.y / l) + v.y * (deltapos.x / l);
        float Vy1 = - v.x * (deltapos.x / l) + v.y * (deltapos.y / l);
        return new Vector2(Vx1, Vy1); 
    }

    public float GetDistance(Vector2 objectPosition){
        return MathF.Sqrt(MathF.Pow(objectPosition.x, 2) + MathF.Pow(objectPosition.y, 2));
    }

    public float GetDistanceFromGravitationalPoint(Vector2 objectPosition, Vector2 gravitationalPointPosition){
        return GetDistance(new Vector2(objectPosition.x - gravitationalPointPosition.y, objectPosition.y - gravitationalPointPosition.y));
    }

    public Vector2 GetCircularMotionVelocity(Vector2 position, float constant1){
        float distance = GetDistance(position);
         
        float velocityX = -MathF.Sqrt(constant1/distance) * position.y / distance;
        float velocityY = MathF.Sqrt(constant1/distance) * position.x / distance;

        if (_clockwise) { velocityX *= -1; velocityY *= -1; }

        return new Vector2(velocityX, velocityY);
    }

    void UpdateKValue(float value)
    {
        KValue = value;
    }

    void UpdateConservedEnergy(string value)
    {
        ConservedEnergy = float.Parse(value);
    }
}
