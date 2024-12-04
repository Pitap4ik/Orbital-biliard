using UnityEngine;

public class QuitScript : MonoBehaviour
{
    public void Start () {
        Application.targetFrameRate = 60;
    }
    
    public void QuitGame(){
        Application.Quit();
    }
}
