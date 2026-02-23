using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EggScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(CheckEggPosition());
    }

    IEnumerator CheckEggPosition()
    {
        while (true)
        {
            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
            if (viewPos.y < 0.05)
            {
                _animator.SetTrigger("break");
                _rb.bodyType = RigidbodyType2D.Static;
                Destroy(gameObject, 1);
                break;
            }
            yield return null;
        }
    }

}
