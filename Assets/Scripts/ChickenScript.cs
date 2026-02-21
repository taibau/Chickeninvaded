using System.Collections;
using UnityEngine;

public class ChickenScript : MonoBehaviour
{
    [SerializeField] private GameObject EggPrefaps;
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
            Instantiate(EggPrefaps, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(2, 7));
        }
    }
}
