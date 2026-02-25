using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    [SerializeField] private float Speed = 5f;
    [SerializeField] private GameObject[] BulletList;
    [SerializeField] private int CurrentTierBullet;
    [SerializeField] private GameObject VFX;
    [SerializeField] private GameObject Shield;
    [SerializeField] private int ScoreOfChickenLeg;

    // 🔥 Fire rate system
    private float baseFireRate = 0.3f;
    private float currentFireRate;
    private float nextFireTime;

    private Coroutine fireRateRoutine;
    private Coroutine shieldRoutine;

    void Start()
    {
        currentFireRate = baseFireRate;

        if (Shield != null)
            Shield.SetActive(false);

        ActivateShield(); //  vào game có khiên 5s
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

        Vector3 topPoint = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, 0));

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -topPoint.x, topPoint.x),
            Mathf.Clamp(transform.position.y, -topPoint.y, topPoint.y),
            0);
    }

    void Fire()
    {
        if (Input.GetMouseButton(0) && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + currentFireRate;

            if (BulletList != null &&
                BulletList.Length > 0 &&
                CurrentTierBullet < BulletList.Length &&
                BulletList[CurrentTierBullet] != null)
            {
                Instantiate(
                    BulletList[CurrentTierBullet],
                    transform.position + Vector3.up * 0.8f,
                    Quaternion.identity);
            }
        }
    }

    // FIRE RATE POWER (ANTI STACK)
    public void ActivateFireRate()
    {
        if (fireRateRoutine != null)
            StopCoroutine(fireRateRoutine);

        fireRateRoutine = StartCoroutine(IncreaseFireRate());
    }

    IEnumerator IncreaseFireRate()
    {
        currentFireRate = 0.1f;

        yield return new WaitForSeconds(5f);

        currentFireRate = baseFireRate;
        fireRateRoutine = null;
    }


    // SHIELD POWER (ANTI STACK)
    public void ActivateShield()
    {
        if (shieldRoutine != null)
            StopCoroutine(shieldRoutine);

        shieldRoutine = StartCoroutine(ShieldCoroutine());
    }

    IEnumerator ShieldCoroutine()
    {
        if (Shield != null)
            Shield.SetActive(true);

        yield return new WaitForSeconds(5f);

        if (Shield != null)
            Shield.SetActive(false);

        shieldRoutine = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //  Chết nếu không có khiên
        if ((collision.CompareTag("Chicken") || collision.CompareTag("Egg")))
        {
            if (Shield != null && Shield.activeSelf)
            {
                return; // có khiên thì không chết
            }

            Destroy(gameObject);
        }
        //  Ăn đùi gà
        else if (collision.CompareTag("chicken leg"))
        {
            Destroy(collision.gameObject);
            ScoreController.instance.GetScore(ScoreOfChickenLeg);
        }
        //  Update Bullet
        else if (collision.CompareTag("Updatebullet"))
        {
            Destroy(collision.gameObject);

            List<int> valid = new List<int>();
            for (int i = 0; i < BulletList.Length; i++)
            {
                if (BulletList[i] != null)
                    valid.Add(i);
            }

            if (valid.Count > 0)
            {
                int chosenIndex = valid[Random.Range(0, valid.Count)];
                CurrentTierBullet = chosenIndex;
            }
        }
        //  Power tăng tốc bắn
        else if (collision.CompareTag("FireRate"))
        {
            Destroy(collision.gameObject);
            ActivateFireRate();
        }
        //  Power khiên
        else if (collision.CompareTag("ShieldPower"))
        {
            Destroy(collision.gameObject);
            ActivateShield();
        }
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            if (VFX != null)
            {
                var vfx = Instantiate(VFX, transform.position, Quaternion.identity);
                Destroy(vfx, 1f);
            }

            ShipController.Instance.SpawnShip();
        }
    }
}