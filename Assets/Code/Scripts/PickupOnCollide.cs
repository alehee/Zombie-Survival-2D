using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupOnCollide : MonoBehaviour
{
    public int scoreIncreaseAmount = 1;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Sprawdź, czy gracz zderzył się z obiektem o tagu "Pickupable"
        if (collision.gameObject.CompareTag("Pickupable"))
        {
            // Dodaj punkty i zniszcz obiekt
            Destroy(collision.gameObject);
            
            //Mniej więcej tak wyobrażam sobię późniejsze dodawanie ilości materiału do
            //Game managera. 
            
            // GameManager.instance.AddScore(scoreIncreaseAmount);
        }
    }
}