using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMover : MonoBehaviour
{
    private Human human;

    private void Awake(){
        InitializeFields();
        SubscribeMovingToNextLevelOrRestart();
    }

    private void InitializeFields(){
        human = FindObjectOfType<Human>();
    }

    private protected void SubscribeMovingToNextLevelOrRestart(){ // On lose - restart, on win - restart and score++
        human.onHumanWin += () => {
            StartCoroutine(NextLevelOrRestartDelay());
        };

        human.onHumanLose += () => {
            StartCoroutine(NextLevelOrRestartDelay());
        };
    }

    private IEnumerator NextLevelOrRestartDelay(){
        yield return new WaitForSeconds(3f);{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
