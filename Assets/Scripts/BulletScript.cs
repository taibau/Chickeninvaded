using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private int damage; // sát thương của viên đạn (gán riêng cho từng prefab)

    public int Damage => damage;

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * Speed);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
            return;

        // nếu va chạm boss => dùng damage thay vì số cứng
        if (collision.name == "Boss")
        {
            if (Boss.Instance != null)
                Boss.Instance.PutDamge(damage);
        }
    }
}
