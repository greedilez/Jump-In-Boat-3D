using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(AudioSource))]
public class Human : MonoBehaviour
{
    private AudioSource source;

    [SerializeField] private AudioClip landOnBoatSound, splashOnWaterSound;

    private Animator animator;

    private bool isFlying, isLanded, isJumped;

    public bool IsFlying{ get => isFlying; }

    public bool IsLanded{ get => isLanded; }

    public delegate void methodContainer();

    public event methodContainer onHumanWin, onHumanLose;

    private void Awake() => InitializeFields();


    private void InitializeFields(){
        Application.targetFrameRate = 60;
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    private void Update(){
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            MoveByTouch(touch);
            ClampPosition();
        }
    }

    private protected void MoveByTouch(Touch touch){
        if(touch.phase == TouchPhase.Moved && !isFlying && !isJumped){
            transform.Translate(new Vector3((touch.deltaPosition.x) / 3, 0, 0) * Time.smoothDeltaTime, Space.World);
        }
    }

    private protected void ClampPosition(){
        float clampedX = transform.position.x;
        clampedX = Mathf.Clamp(clampedX, -2.55f, 1.5f);
        Vector3 targetPosition = new Vector3(clampedX, transform.position.y, transform.position.z);
        transform.position = targetPosition;
    }

    public void Jump(float zForce){
        if(!isJumped && !isFlying){
            isFlying = true;
            isJumped = true;
            animator.SetTrigger("Jump");
            StartCoroutine(AddForceDelay(zForce));
        }
    }

    private IEnumerator AddForceDelay(float zForce){
        yield return new WaitForSeconds(0.595f);{
            transform.position += transform.forward * zForce;
        }
    }

    private void OnTriggerEnter(Collider col){
        switch(col.tag){
            case "Boat":
            LandOnBoat(col);
            break;

            case "Sea":
            LandOnSea(col);
            break;
        }
    }

    private protected void LandOnSea(Collider other){
        if(!isLanded){
            isLanded = true;
            isFlying = false;
            Debug.Log("Game over!");
            animator.SetTrigger("LandOnWater");
            source.PlayOneShot(splashOnWaterSound);
            onHumanLose();
        }
    }

    private protected void LandOnBoat(Collider boat){
        if(!isLanded){
            isLanded = true;
            isFlying = false;
            Debug.Log("Win!");
            SyncPositionWithBoat(boat);
            animator.SetTrigger("Land");
            source.PlayOneShot(landOnBoatSound);
            onHumanWin();
        }
    }

    private protected void SyncPositionWithBoat(Collider boat){
        transform.SetParent(boat.transform.GetChild(0).transform);
        transform.position = boat.GetComponent<Boat>().GetSeatPlacePosition();
    }
}
