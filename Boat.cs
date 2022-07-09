using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Boat : MonoBehaviour
{
    [SerializeField] private Transform seatPlace;

    private float speed = 3f;

    private AudioSource source;

    public Transform SeatPlace{ get => seatPlace; }

    private GameObject boatFinishPlace;

    private enum BoatType{ Wood, Scout, Speed, Fisher}

    [SerializeField] private BoatType boatType;

    private void Awake() => InitializeFields();

    private void Update(){
        MoveToFinish();
        DestroyOnFinish();
    }

    private protected void InitializeFields(){
        boatFinishPlace = GameObject.Find("BoatFinishPlace");
        source = GetComponent<AudioSource>();
        AdjustSpeedByType();
        AdjustSoundBySpeed();
    }

    private protected void AdjustSpeedByType(){
        switch(boatType){
            case BoatType.Wood:
            speed = Random.Range(4f, 4.35f);
            break;

            case BoatType.Scout:
            speed = Random.Range(4f, 5f);
            break;

            case BoatType.Speed:
            speed = Random.Range(4.85f, 5.45f);
            break;

            case BoatType.Fisher:
            speed = Random.Range(4.15f, 5f);
            break;
        }
    }

    private protected void AdjustSoundBySpeed(){
        float pitch = speed / Random.Range(2f, 2.7f);
        source.pitch = pitch;
    }

    private protected void DestroyOnFinish(){
        if(Vector3.Distance(transform.position, boatFinishPlace.transform.position) < 3f){
            Destroy(gameObject);
            Debug.Log($"{gameObject.name} is destroyed on finish");
        }
    }

    private protected void MoveToFinish() => transform.localPosition = Vector3.MoveTowards(transform.localPosition, boatFinishPlace.transform.position, Time.smoothDeltaTime * speed);

    public Vector3 GetSeatPlacePosition() => seatPlace.position;
}
