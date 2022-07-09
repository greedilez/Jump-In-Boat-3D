using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Update(){
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            StartByTap(touch);
        }
    }

    private protected void StartByTap(Touch touch){
        if(touch.phase == TouchPhase.Began){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
