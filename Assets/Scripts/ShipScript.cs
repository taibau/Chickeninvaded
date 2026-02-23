using System.Collections;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private GameObject[] BulletList;
    [SerializeField] private int CurrentTierBullet;
    [SerializeField] private GameObject VFX;
    [SerializeField] private GameObject Shield;
    [SerializeField] private int ScoreOfChickenLeg;
    void Start()
    {
        StartCoroutine(DisableShield());
    }


    void Update()
    {

        Move();
        Fire();
    }
    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(x, y, 0);

        transform.position += direction.normalized * Speed * Time.deltaTime;

        Vector3 TopLepPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, TopLepPoint.x * -1, TopLepPoint.x), Mathf.Clamp(transform.position.y, TopLepPoint.y * -1, TopLepPoint.y));
    }
    void Fire()
    {
        if (Input.GetMouseButtonDown(0))
            Instantiate(BulletList[CurrentTierBullet], transform.position, Quaternion.identity);
    }

    IEnumerator DisableShield()
    {
        yield return new WaitForSeconds(8);
        Shield.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Shield.activeSelf && (collision.CompareTag("Chicken") || collision.CompareTag("Egg")))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("chicken leg"))
        {
            Destroy(collision.gameObject);
            ScoreController.instance.GetScore(ScoreOfChickenLeg);
        }
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            var vfx = Instantiate(VFX, transform.position, Quaternion.identity);
            Destroy(vfx, 1f);
            ShipController.Instance.SpawnShip();
        }
    }
}
