using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuilding : MonoBehaviour
{
    [SerializeField]
    GameObject towerPrefab;
    [SerializeField]
    GameObject wallPrefab;
    [SerializeField]
    GameObject lowerWallPrefab;
    [SerializeField]
    float buildingDistance = 2.0f;
    Transform playerTransform;
    KeyCode placeTowerKey = KeyCode.Alpha1;
    KeyCode placeWallKey = KeyCode.Alpha2;
    KeyCode placeLowerWallKey = KeyCode.Alpha3;
    private PlayerStatus status;

    void Start()
    {
        status = FindObjectOfType<PlayerStatus>();
        playerTransform = gameObject.transform;    
    }

    void Update()
    {
        if (Input.GetKeyDown(placeTowerKey))
        {
            if (status.GetSticks() >= 2 && status.GetStones() >= 5)
            {
                status.SetSticks(status.GetSticks() - 2);
                status.SetStones(status.GetStones() - 5);
                // Oblicz pozycję, w której ma zostać postawiony obiekt
                Vector3 spawnPosition = playerTransform.position + playerTransform.forward * buildingDistance;
                spawnPosition.z = 0;

                // Stwórz obiekt "building" w pozycji spawnPosition
                GameObject building = Instantiate(towerPrefab, spawnPosition, Quaternion.identity);
                status.UpdateSticksCounter();
                // Ustaw rotację obiektu "building" zgodnie z rotacją gracza
                building.transform.rotation = playerTransform.rotation;
                Debug.Log($"Tower successfully placed!");
            }
            else
            {
                Debug.Log($"Tower cannot be placed, insufficient amount of sticks!");
            }
        }

        if (Input.GetKeyDown(placeWallKey))
        {
            if (status.GetSticks() >= 2)
            {
                status.SetSticks(status.GetSticks() - 2);

                Vector3 spawnPosition = playerTransform.position + playerTransform.forward * buildingDistance;
                spawnPosition.z = 0;


                GameObject wall = Instantiate(wallPrefab, spawnPosition, Quaternion.identity);
                status.UpdateSticksCounter();

                wall.transform.rotation = playerTransform.rotation;
                Debug.Log($"Wall successfully placed!");
            }
            else
            {
                Debug.Log($"Wall cannot be placed, insufficient amount of sticks!");
            }
        }

        if (Input.GetKeyDown(placeLowerWallKey))
        {
            if (status.GetSticks() >= 1)
            {
                status.SetSticks(status.GetSticks() - 1);

                Vector3 spawnPosition = playerTransform.position + playerTransform.forward * buildingDistance;
                spawnPosition.z = 0;

                GameObject lowerWall = Instantiate(lowerWallPrefab, spawnPosition, Quaternion.identity);
                status.UpdateSticksCounter();

                lowerWall.transform.rotation = playerTransform.rotation;
                Debug.Log($"Lower wall successfully placed!");
            }
            else
            {
                Debug.Log($"Lower wall cannot be placed, insufficient amount of sticks!");
            }
        }
    }
}