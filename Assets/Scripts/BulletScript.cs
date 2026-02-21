using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float DistanceDestroy;
    void Start()
    {
        DistanceDestroy = 10;
    }


    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * Speed);
        DestroyIfReachDistance();

    }
    void DestroyIfReachDistance()
    {
        Vector3 CenterScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2),0);
        if (Vector3.Distance(CenterScreen, transform.position) > DistanceDestroy)
        {
            Destroy(this.gameObject);
        }
    }
}
