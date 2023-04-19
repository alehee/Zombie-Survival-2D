using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuilding : MonoBehaviour
{
    [SerializeField]
    GameObject buildingPrefab;
    [SerializeField]
    float buildingDistance = 2.0f;
    Transform playerTransform;
    KeyCode placeBuildingKey = KeyCode.T;
    private PlayerStatus status;

    void Start()
    {
        status = FindObjectOfType<PlayerStatus>();
        playerTransform = gameObject.transform;    
    }

    void Update()
    {
        if (Input.GetKeyDown(placeBuildingKey))
        {
            if (status.Sticks >= 1)
            {
                status.SetSticks(status.Sticks - 1);
            // Oblicz pozycję, w której ma zostać postawiony obiekt
            Vector3 spawnPosition = playerTransform.position + playerTransform.forward * buildingDistance;
            spawnPosition.z = 0;

            // Stwórz obiekt "building" w pozycji spawnPosition
            GameObject building = Instantiate(buildingPrefab, spawnPosition, Quaternion.identity);

            // Ustaw rotację obiektu "building" zgodnie z rotacją gracza
            building.transform.rotation = playerTransform.rotation;
            Debug.Log($"Building successfully placed!");
            }
            else
            {
                Debug.Log($"Building cannot be placed, insufficient amount of sticks!");
            }
        }
    }
}