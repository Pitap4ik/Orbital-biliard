using System.Collections;
using UnityEngine;

public class SampleActorManager : MonoBehaviour
{
    [SerializeField] private float _velocityMultiplier;
    public Transform Transform { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public float VelocityX { get; private set; }
    public float VelocityY { get; private set; }
    public float VelocityMultiplier { get { return _velocityMultiplier; } set { _velocityMultiplier = value; } }

    void Start()
    {
        Transform = GetComponent<Transform>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private IEnumerator ChangeVelocity(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            SetRandomVelocity(-0.5f, 0.5f);
        }
    }

    void FixedUpdate()
    {
        SetRandomVelocity(-0.5f, 0.5f);
    }

    private void SetRandomVelocity(float minValue, float maxValue)
    {
        VelocityX = Rigidbody.linearVelocity.x + Random.Range(minValue, maxValue) * VelocityMultiplier;
        VelocityY = Rigidbody.linearVelocity.y + Random.Range(minValue, maxValue) * VelocityMultiplier;
        Rigidbody.linearVelocity = new Vector2(VelocityX, VelocityY);
    }
}
