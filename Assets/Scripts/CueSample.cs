using UnityEngine;

public class CueController2D : MonoBehaviour
{
    public float maxPullBackDistance = 2f; // Максимальное натяжение
    public float returnSpeed = 5f; // Скорость возврата
    public float minX = -2f; // Минимальная позиция
    public float maxX = 2f; // Максимальная позиция

    private Vector3 initialPosition; // Начальная позиция кия
    private bool isDragging = false; // Флаг перетаскивания
    private bool isReturning = false; // Флаг возврата
    private float offsetX; // Смещение между мышью и начальной позицией
    private float currentPullBack = 0f; // Текущее натяжение

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // При нажатии мыши начинаем перетаскивание
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offsetX = mousePosition.x - transform.position.x; 
            isDragging = true;
            isReturning = false;
        }

        // Перетаскиваем кий, если кнопка мыши зажата
        if (isDragging && Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float newX = mousePosition.x - offsetX;
            newX = Mathf.Clamp(newX, minX, maxX);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);

            // Рассчитываем текущее натяжение на основе перемещения по оси X
            currentPullBack = Mathf.Abs(newX - initialPosition.x);
        }

        // Когда кнопка мыши отпускается, начинаем возвращать кий в начальную позицию
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            isReturning = true;
        }

        // Плавный возврат кия в начальную позицию
        if (isReturning)
        {
            transform.position = Vector3.Lerp(transform.position, initialPosition, Time.deltaTime * returnSpeed);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);

            if (Vector3.Distance(transform.position, initialPosition) < 0.01f)
            {
                transform.position = initialPosition;
                isReturning = false;
            }
        }
    }
}