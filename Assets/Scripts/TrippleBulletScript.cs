using UnityEngine;

public class TrippleBulletScript : MonoBehaviour
{
    [SerializeField] private GameObject childBulletPrefaps;
    [SerializeField] private float sideOffset = 0.6f;

    void Start()
    {
        if (childBulletPrefaps == null)
        {
            Debug.LogWarning("childBulletPrefaps is null!");
            return;
        }

        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;

        Instantiate(childBulletPrefaps, pos + Vector3.down * sideOffset, rot);
        Instantiate(childBulletPrefaps, pos, rot);
        Instantiate(childBulletPrefaps, pos + Vector3.down * sideOffset, rot);

        Destroy(gameObject);
    }
}