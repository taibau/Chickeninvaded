using UnityEngine;
using UnityEngine.UI;

public class BossHpBarScript : MonoBehaviour
{
    private Image hpImage;
    private void Awake()
    {
        hpImage = GetComponent<Image>();
    }
    void Start()
    {

    }


    void Update()
    {
        if (Boss.Instance == null) return;
        float percent = (float)Boss.Instance.CurrentHealth / Boss.Instance.MaxHealth;
        hpImage.fillAmount = percent;
    }
}
