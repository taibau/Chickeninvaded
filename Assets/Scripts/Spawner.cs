using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float gridSize = 1;
    private Vector3 spawnPos;
    [SerializeField] private GameObject ChickenPrfaps;
    [SerializeField] private Transform GridChicken;
    [SerializeField] private GameObject Boss;

    public static Spawner Instance;
    private int ChickenCurrent;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;

        spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));

        spawnPos.x += ((gridSize / 2 + (width / 4)));
        spawnPos.y -= gridSize;
        spawnPos.z = 0;
        SpawnChicken(Mathf.FloorToInt(height / 2 / gridSize), Mathf.FloorToInt(width / gridSize / 1.5f));
    }
    void SpawnChicken(int row, int numberChicken)
    {
        float x = spawnPos.x;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < numberChicken; j++)
            {
                spawnPos.x = spawnPos.x + gridSize;
                GameObject Chicken = Instantiate(ChickenPrfaps, spawnPos, Quaternion.identity);
                Chicken.transform.parent = GridChicken;
                ChickenCurrent++;
            }
            spawnPos.x = x;
            spawnPos.y -= gridSize;
        }
    }

    public void DecreaChicken()
    {
        ChickenCurrent--;
        if (ChickenCurrent <= 0)
        {
            Boss.gameObject.SetActive(true);
        }
    }
}
