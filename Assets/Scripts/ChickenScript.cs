using System.Collections;
using UnityEngine;

public class ChickenScript : MonoBehaviour
{
    [SerializeField] private GameObject EggPrefaps;
    [SerializeField] private int score;
    [SerializeField] private GameObject ChicckenLegPrefaps;
    [SerializeField] private GameObject UpdateBulletPrefaps;
    [SerializeField, Range(0f, 1f)] private float updateDropChance = 0.05f; // tỉ lệ rơi (0..1)
    [SerializeField] private GameObject FireRatePrefabs;
    [SerializeField] private GameObject ShieldPrefabs;
    [SerializeField, Range(0f, 1f)] private float powerDropChance = 0.05f;

    [SerializeField] private int health = 20; // máu gà
    // xác suất đẻ trứng
    [SerializeField, Range(0f, 1f)]
    private float eggDropChance = 0.4f;
    private void Awake()
    {
        StartCoroutine(SpawmEgg());
    }

    void Update()
    {

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
            return;

        if (collision.CompareTag("Bullet"))
        {
            // lấy sát thương từ script viên đạn (nếu có)
            int damage = 1;
            var bulletScript = collision.GetComponent<BulletScript>();
            if (bulletScript != null)
                damage = bulletScript.Damage;

            // hủy viên đạn ngay
            Destroy(collision.gameObject);

            // trừ máu
            health -= damage;

            // nếu chết mới cho điểm, rơi đồ và destroy gà
            if (health <= 0)
            {
                if (ScoreController.instance != null)
                    ScoreController.instance.GetScore(score);

                if (ChicckenLegPrefaps != null)
                    Instantiate(ChicckenLegPrefaps, transform.position, Quaternion.identity);

                // rơi update bullet theo tỉ lệ
                if (UpdateBulletPrefaps != null && Random.value <= updateDropChance)
                    Instantiate(UpdateBulletPrefaps, transform.position, Quaternion.identity);
                // rơi khiên hoặc tăng tốc độ bắn
                if (Random.value <= powerDropChance)
                {
                    int random = Random.Range(0, 2);

                    if (random == 0 && FireRatePrefabs != null)
                        Instantiate(FireRatePrefabs, transform.position, Quaternion.identity);

                    if (random == 1 && ShieldPrefabs != null)
                        Instantiate(ShieldPrefabs, transform.position, Quaternion.identity);
                }

                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        if (Spawner.Instance != null)
            Spawner.Instance.DecreaChicken();
    }
}
