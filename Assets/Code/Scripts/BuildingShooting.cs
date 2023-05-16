using UnityEngine;
using TMPro;

public class BuildingShooting : MonoBehaviour
{
    public float range = 5f; // zasięg działania wieży
    public float fireRate = 0.5f; // szybkość strzelania (czas pomiędzy strzałami)
    public GameObject bulletPrefab; // prefabrykat pocisku
    public Transform[] firePoints; // pozycje początkowe pocisku
    public GameObject timeObject;

    private float lastFireTime; // czas ostatniego strzału

    public float SecondsToDestroy;
    float SecondsElapsed = 0;
    TextMeshPro timeText;

    private void Start()
    {
        timeText = timeObject.gameObject.GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        // Jeśli czas minie zniszcz
        SecondsElapsed += Time.deltaTime;
        if(SecondsElapsed > SecondsToDestroy)
        {
            Destroy(gameObject);
        }

        // Zaktualizuj timer
        timeText.SetText((SecondsToDestroy - SecondsElapsed).ToString("0.0"));

        // szukaj wrogów w zasięgu wieży
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Enemy")
            {
                Transform nearestFirePoint = null;
                float nearestDistance = float.MaxValue;
                foreach (Transform firePoint in firePoints)
                {
                    // oblicz odległość między firePoint a wrogiem
                    float distance = Vector2.Distance(firePoint.position, hitCollider.transform.position);

                    // jeśli w zasięgu wieży jest firePoint, z którego może strzelać, wybierz najbliższy
                    if (distance <= range)
                    {
                        if (distance < nearestDistance)
                        {
                            nearestFirePoint = firePoint;
                            nearestDistance = distance;
                        }
                    }
                }

                // jeśli znaleziono firePoint z którego można strzelać i minął czas od ostatniego strzału, strzelaj
                if (nearestFirePoint != null && Time.time - lastFireTime > fireRate)
                {
                    ShootAtEnemy(hitCollider.gameObject, nearestFirePoint);
                    lastFireTime = Time.time;
                }
            }
        }
    }

    private void ShootAtEnemy(GameObject enemy, Transform firePoint)
    {
        // stwórz pocisk i wystrzel w kierunku wroga z wybranego firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<TowerBullet>().SetTarget(enemy.transform);
    }
}