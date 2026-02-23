using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float Speed;

    void Start()
    {
        
    }


    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * Speed);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Boss")
        {
            Boss.Instance.PutDamge(10);
            Destroy(gameObject);
        }
    }
}
