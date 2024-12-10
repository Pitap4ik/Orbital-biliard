using UnityEngine;

public class CueController2D : MonoBehaviour
{
    public Transform targetObject; 
    public float returnSpeed = 5f; 
    public float minX = -2f; 
    public float maxX = 2f; 
    public float shotForce = 10f; 

    private Vector3 initialPosition; 
    private bool isDragging = false; 
    private bool isReturning = false; 
    private float offsetX; 
    private bool shotMade = false;

    void Start()
    {
        // Сохраняем начальную позицию
        initialPosition = transform.position;
    }

    void Update()
    {
        // При нажатии мыши
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; 

            // Проверяем, была ли мышь нажата на кию
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
            if (hitCollider != null && hitCollider.gameObject == gameObject)
            {
                offsetX = mousePosition.x - transform.position.x; 
                isDragging = true;
                isReturning = false;
                shotMade = false; 
            }
        }

        // Если мышь зажата, двигаем кий
        if (isDragging && Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Убираем глубину для 2D сцены

            // Ограничиваем движение вдоль только оси X
            float newX = mousePosition.x - offsetX;

            // Ограничиваем движение в заданных пределах
            newX = Mathf.Clamp(newX, minX, maxX);

            // Обновляем позицию кия
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }

        // Когда отпускаем кнопку мыши
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            isReturning = true;
            shotMade = true; 
        }

        // Возвращаем кий в начальную позицию
        if (isReturning && !shotMade)
        {
            transform.position = Vector3.Lerp(transform.position, initialPosition, Time.deltaTime * returnSpeed);

            if (Vector3.Distance(transform.position, initialPosition) < 0.01f)
            {
                transform.position = initialPosition;
                isReturning = false;
            }
        }

        // Целимся в мяч или другой объект
        if (targetObject != null)
        {
            // Вычисляем направление от кия к объекту
            Vector3 direction = targetObject.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Поворачиваем кий в направлении мяча
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // Когда удар был совершен, применяем силу к мячу
            if (shotMade)
            {
                Rigidbody2D targetRb = targetObject.GetComponent<Rigidbody2D>();
                if (targetRb != null)
                {
                    // Применяем силу в направлении кия
                    Vector2 forceDirection = direction.normalized; // Направление удара
                    targetRb.AddForce(forceDirection * shotForce, ForceMode2D.Impulse);
                }

                // Начинаем возвращение кия после удара
                isReturning = true;
            }
        }
    }
}