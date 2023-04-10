using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuilding : MonoBehaviour
{
    public GameObject buildingPrefab;
    public Transform playerTransform;
    public KeyCode placeBuildingKey = KeyCode.T;

    void Update()
    {
        // Sprawdź, czy gracz nacisnął przycisk "T"
        if (Input.GetKeyDown(placeBuildingKey))
        {
            // Oblicz odległość przed graczem, w której ma zostać postawiony obiekt
            float distance = 2.0f;

            // Oblicz pozycję, w której ma zostać postawiony obiekt
            Vector3 spawnPosition = playerTransform.position + playerTransform.forward * distance;

            // Stwórz obiekt "building" w pozycji spawnPosition
            spawnPosition.z = 0;
            GameObject building = Instantiate(buildingPrefab, spawnPosition, Quaternion.identity);

            // Ustaw rotację obiektu "building" zgodnie z rotacją gracza
            building.transform.rotation = playerTransform.rotation;
        }
    }
}