using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    public float speed = 10f; // prędkość pocisku
    private Transform target; // cel pocisku
    [SerializeField]
    double Damage = 3;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        if (target != null)
        {
            // obracaj pocisk w kierunku celu
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // poruszaj pocisk w kierunku celu
            float distanceThisFrame = speed * Time.deltaTime;
            transform.Translate(direction.normalized * distanceThisFrame, Space.World);

        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Status>(out Status status))
        {
            status.TakeDamage(Damage);
            Debug.Log($"Damage dealt to {collision.gameObject.name}: {Damage}");
        }
        Destroy(gameObject);
    }
}