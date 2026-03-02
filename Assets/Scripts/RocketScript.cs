using UnityEngine;

public class RocketScript : MonoBehaviour
{
    [SerializeField] private float Speed = 8f;
    [SerializeField] private int damage = 5;
    [SerializeField] private float explosionRadius = 2f; // bán kính nổ
    [SerializeField] private GameObject VFX;
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * Speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
            return;

        // Va chạm gà hoặc boss thì nổ
        if (collision.CompareTag("Chicken") || collision.name == "Boss")
        {
            Explode();
        }
    }

    void Explode()
    {
        if (VFX != null)
        {
            GameObject vfx = Instantiate(VFX, transform.position, Quaternion.identity);
            Destroy(vfx, 1f); // tự hủy sau 1s (tùy theo thời gian animation)
        }
        // Tìm tất cả collider trong bán kính nổ
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            explosionRadius
        );

        foreach (var hit in hits)
        {
            // Gây damage cho gà
            if (hit.CompareTag("Chicken"))
            {
                ChickenScript Chicken = hit.GetComponent<ChickenScript>();
                if (Chicken != null)
                {
                    Chicken.TakeDamage(damage);
                }
            }

            // Gây damage cho boss
            if (hit.name == "Boss")
            {
                if (Boss.Instance != null)
                    Boss.Instance.PutDamge(damage);
            }
        }


        Destroy(gameObject);
    }

    // Vẽ vòng tròn nổ trong Scene view để debug
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}