using UnityEngine;

public class DestroyIfReachDistance : MonoBehaviour
{
    [SerializeField] private float DistanceDestroy;
    void Start()
    {
        
    }
    void Update()
    {
        DestroyIfTrue();

    }
    void DestroyIfTrue()
    {
        Vector3 CenterScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2), 0);
        if (Vector3.Distance(CenterScreen, transform.position) > DistanceDestroy)
        {
            Destroy(this.gameObject);
        }
    }
}
