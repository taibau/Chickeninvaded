using UnityEngine;

public class ShieldPower : MonoBehaviour
{
    public float fallSpeed = 2f;

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<ShipScript>().ActivateShield();
            Destroy(gameObject);
        }
    }
}