using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    
    public GameObject droppedItemPrefab;
    public List<Loot> lootList = new List<Loot>();


    List<Loot> GetDroppedItems()
    {
        int randomNumber = Random.Range(1,101); // 1-100
        List<Loot> possibleItems = new List<Loot>();
        foreach (Loot item in lootList)
        {
            if(randomNumber <= item.dropChance)
            {
                possibleItems.Add(item);
            }
        }
        if (possibleItems.Count > 0) 
        {
            return possibleItems;
        }
        Debug.Log("No loot dropped");
        return null;
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
        List<Loot> droppedItems = GetDroppedItems();
        if(droppedItems != null)
        {
            foreach (Loot item in droppedItems)
            {
                GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
                lootGameObject.GetComponent<SpriteRenderer>().sprite = item.lootSprite;

                //można to odkomentować i loot będzie poruszał się w którąś stronę,
                //problem w tym że ciągle się porusza i nie chce się zatrzymać po czasie

                // float dropForce = 15f;
                // Vector2 dropDirection = new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f));
                // lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);

                if (item.lootName == "Exp") 
                {
                    lootGameObject.tag = "Exp";
                    Debug.Log("Dropped Exp");
                } 
                else if (item.lootName == "Coin") 
                {
                    lootGameObject.tag = "Coin";
                     Debug.Log("Dropped Coin");
                }
            }
        }
    }

}