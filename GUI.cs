using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUI : MonoBehaviour
{
    private Animator textAnimator;

    [SerializeField] private TextMeshProUGUI winOrLoseText;

    private Human human;

    private void Awake(){
        InitializeField();
        SubscribeTurningTextOnWin();
        SubscribeTurningTextOnLose();
    }

    private protected void InitializeField(){
        human = FindObjectOfType<Human>();
        textAnimator = winOrLoseText.GetComponent<Animator>();
    }

    private protected void SubscribeTurningTextOnWin(){
        human.onHumanWin += () => {
            textAnimator.SetTrigger("TextMove");
            RandomWinText();
        };
    }

    private protected void RandomWinText(){
        int state = Random.Range(0, 1);
        switch(state){
            case 0:
            winOrLoseText.text = "Nice, you win! Next level";
            break;

            case 1:
            winOrLoseText.text = "Excellent! Next level";
            break;
        }
    }


    private protected void SubscribeTurningTextOnLose(){
        human.onHumanLose += () => {
            textAnimator.SetTrigger("TextMove");
            RandomLoseText();
        };
    }

    private protected void RandomLoseText(){
        int state = Random.Range(0, 1);
        switch(state){
            case 0:
            winOrLoseText.text = "You lose, restart!";
            break;

            case 1:
            winOrLoseText.text = "Try again, restart!";
            break;
        }
    }
}
