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
    private float _kValueBuffer;
    public bool isPaused = false;

    void Start()
    {
        _quitButton.onClick.AddListener(QuitGame);   
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Tab)){
            if (!isPaused){
                PausePhysics();
            }
            else {
                UnpausePhysics();
            }
        }
    }

    void QuitGame(){
        Application.Quit();
    }

    public void PausePhysics(){
        if (!isPaused){
            _kValueBuffer = KValueSlider.value;
            KValueSlider.value = 0f;
            isPaused = true;
        }
    }

    public void UnpausePhysics(){
        if (isPaused){
            KValueSlider.value = _kValueBuffer;
            isPaused = false;
        }
    } 
}
