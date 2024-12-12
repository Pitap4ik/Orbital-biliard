using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private Slider _kValueSlider;
    [SerializeField] private Button _quitButton;
    [SerializeField] private TMP_InputField _energyInputField;
    public Slider KValueSlider { get => _kValueSlider; private set => _kValueSlider = value; }
    public TMP_InputField EnergyInputField { get => _energyInputField; set => _energyInputField = value; }

    void Start()
    {
        _quitButton.onClick.AddListener(QuitGame);
    }

    void QuitGame(){
        Application.Quit();
    }
}
