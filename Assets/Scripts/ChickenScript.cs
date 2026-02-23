using System.Collections;
using UnityEngine;

public class ChickenScript : MonoBehaviour
{
    [SerializeField] private GameObject EggPrefaps;
    [SerializeField] private int score;
    [SerializeField] private GameObject ChicckenLegPrefaps;
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
            yield return new WaitForSeconds(Random.Range(4, 20));
            Instantiate(EggPrefaps, transform.position, Quaternion.identity);

            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            ScoreController.instance.GetScore(score);

            Instantiate(ChicckenLegPrefaps, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Spawner.Instance.DecreaChicken();
    }
}
