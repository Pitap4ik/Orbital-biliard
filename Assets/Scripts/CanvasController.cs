using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private Slider _kValueSlider;
    [SerializeField] private Button _quitButton;
    public Slider KValueSlider { get => _kValueSlider; private set => _kValueSlider = value; }

    void Start()
    {
        _quitButton.onClick.AddListener(QuitGame);
    }

    void QuitGame(){
        Application.Quit();
    }
}
