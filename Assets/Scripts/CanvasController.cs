using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private Slider _kValueSlider;
    [SerializeField] private Button _quitButton;

    private void Start()
    {
        _kValueSlider.onValueChanged.AddListener(UpdateKValue);
        _quitButton.onClick.AddListener(QuitGame);
    }

    private void UpdateKValue(float value){
        Debug.Log(value);
    }

    private void QuitGame(){
        Application.Quit();
    }
}
