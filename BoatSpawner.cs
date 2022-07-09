using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSpawner : MonoBehaviour
{
    [SerializeField] private Transform boatSpawnPlace;

    [SerializeField] private GameObject[] boatVariations = new GameObject[5];

    private Human human = null;

    private void Awake(){
        human = FindObjectOfType<Human>();
        SpawnBoat();
        StartCoroutine(BoatSpawnDelay());
    }

    private IEnumerator BoatSpawnDelay(){
        yield return new WaitForSeconds(10f);{
            if(!human.IsLanded){
                SpawnBoat();
            }
            StartCoroutine(BoatSpawnDelay());
        }
    }

    private protected void SpawnBoat(){
        int boatIndex = Random.Range(0, boatVariations.Length);
        Vector3 spawnPosition = new Vector3(boatSpawnPlace.position.x + Random.Range(-4f, 4f), boatVariations[boatIndex].transform.position.y - 0.1f, boatSpawnPlace.position.z);
        Instantiate(boatVariations[boatIndex], spawnPosition, Quaternion.Euler(0, 90, -4.5f));
    }
}
