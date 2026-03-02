using System.Collections;
using UnityEngine;

public class ChickenScript : MonoBehaviour
{
    [SerializeField] private GameObject EggPrefaps;
    [SerializeField] private int score;
    [SerializeField] private GameObject ChicckenLegPrefaps;
    [SerializeField] private GameObject UpdateBulletPrefaps;
    [SerializeField, Range(0f, 1f)] private float updateDropChance = 0.05f;
    [SerializeField] private GameObject FireRatePrefabs;
    [SerializeField] private GameObject ShieldPrefabs;
    [SerializeField, Range(0f, 1f)] private float powerDropChance = 0.05f;
    [SerializeField] private GameObject GiftPrefabs;
    [SerializeField, Range(0f, 1f)] private float giftDropChance = 0.1f;
   

    [SerializeField] private int health = 20;
    [SerializeField, Range(0f, 1f)] private float eggDropChance = 0.4f;

    private bool isDead = false;

    private void Awake()
    {
        StartCoroutine(SpawmEgg());
    }

    IEnumerator SpawmEgg()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5f, 12f));

            if (Random.value <= eggDropChance)
            {
                if (EggPrefaps != null)
                    Instantiate(EggPrefaps, transform.position, Quaternion.identity);
            }
        }
    }

    // 🔥 Nhận damage từ Rocket AOE
    public void TakeDamage(int amount)
    {
        if (isDead) return;

        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;
        if (collision == null) return;

        // Chỉ xử lý Bullet thường ở đây
        if (collision.CompareTag("Bullet"))
        {
            int damage = 1;

            var bulletScript = collision.GetComponent<BulletScript>();
            if (bulletScript != null)
                damage = bulletScript.Damage;

            Destroy(collision.gameObject);

            TakeDamage(damage);
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        if (ScoreController.instance != null)
            ScoreController.instance.GetScore(score);

        // VFX nổ
        

        // Rơi đùi gà
        if (ChicckenLegPrefaps != null)
            Instantiate(ChicckenLegPrefaps, transform.position, Quaternion.identity);

        // Update Bullet
        if (UpdateBulletPrefaps != null && Random.value <= updateDropChance)
            Instantiate(UpdateBulletPrefaps, transform.position, Quaternion.identity);

        // Shield hoặc FireRate
        if (Random.value <= powerDropChance)
        {
            int random = Random.Range(0, 2);

            if (random == 0 && FireRatePrefabs != null)
                Instantiate(FireRatePrefabs, transform.position, Quaternion.identity);

            if (random == 1 && ShieldPrefabs != null)
                Instantiate(ShieldPrefabs, transform.position, Quaternion.identity);
        }

        // Gift văng ra
        if (GiftPrefabs != null && Random.value <= giftDropChance)
        {
            GameObject gift = Instantiate(GiftPrefabs, transform.position, Quaternion.identity);

            Rigidbody2D rb = gift.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 force = new Vector2(
                    Random.Range(-2f, 2f),
                    Random.Range(3f, 5f)
                );

                rb.AddForce(force, ForceMode2D.Impulse);
            }
        }

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (Spawner.Instance != null)
            Spawner.Instance.DecreaChicken();
    }
}