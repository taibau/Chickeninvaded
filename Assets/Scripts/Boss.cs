using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject EggPreFap;
    [SerializeField] private int health = 100;
    [SerializeField] private GameObject VFX;
    [SerializeField] private GameObject bossHpBar;
    public int CurrentHealth => health;
    public int MaxHealth => maxHealth;

    private int maxHealth;
    public static Boss Instance;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        StartCoroutine(SpawnEgg());
        StartCoroutine(MoveBossToRandomPoint());
        bossHpBar.SetActive(true);
        maxHealth = health;
    }

    public void PutDamge(int damge)
    {
        health -= damge;
        float healthPercent = (float)health / maxHealth;
        if (health <= 0)
        {
            bossHpBar.SetActive(false);
            Destroy(gameObject);
            var vfx = Instantiate(VFX, transform.position, Quaternion.identity);
            Destroy(vfx, 1);
        }
    }

    IEnumerator SpawnEgg()
    {
        while (true)
        {
            Instantiate(EggPreFap, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(0.0f, 1.0f));
        }
    }

    IEnumerator MoveBossToRandomPoint()
    {
        Vector3 point = GetRandomPoint();
        while (transform.position != point)
        {
            transform.position = Vector3.MoveTowards(transform.position, point, 0.1f);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        StartCoroutine(MoveBossToRandomPoint());
    }
    Vector3 GetRandomPoint()
    {
        Vector3 posRandom = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), Random.Range(0.5f, 1)));
        posRandom.z = 0;
        return posRandom;
    }
}
