using System.Collections;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private GameObject[] BulletList;
    [SerializeField] private int CurrentTierBullet;
    [SerializeField] private GameObject VFX;
    [SerializeField] private GameObject Shield;
    [SerializeField] private int ScoreOfChickenLeg;

    
    private int _startingScore;

    private void Awake()
    {
        
        CurrentTierBullet = 0;
    }

    void Start()
    {
        StartCoroutine(DisableShield());

        if (ScoreController.instance != null)
        {
            // set baseline so this ship upgrades only after earning 1200 points during this life
            _startingScore = ScoreController.instance.Score;
            ScoreController.instance.OnScoreChanged += HandleScoreChanged;
            // do not call HandleScoreChanged(ScoreController.instance.Score) here
        }
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

        Vector3 TopLepPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, TopLepPoint.x * -1, TopLepPoint.x), Mathf.Clamp(transform.position.y, TopLepPoint.y * -1, TopLepPoint.y));
    }

    void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ensure index is safe
            int index = Mathf.Clamp(CurrentTierBullet, 0, (BulletList != null ? BulletList.Length - 1 : 0));
            if (BulletList != null && BulletList.Length > 0)
                Instantiate(BulletList[index], transform.position, Quaternion.identity);
        }
    }

    IEnumerator DisableShield()
    {
        yield return new WaitForSeconds(3);
        Shield.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Shield.activeSelf && (collision.CompareTag("Chicken") || collision.CompareTag("Egg")))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("chicken leg"))
        {
            Destroy(collision.gameObject);
            ScoreController.instance.GetScore(ScoreOfChickenLeg);
        }
    }

    private void OnDestroy()
    {
        // unsubscribe to avoid dangling subscription
        if (ScoreController.instance != null)
            ScoreController.instance.OnScoreChanged -= HandleScoreChanged;

        if (gameObject.scene.isLoaded)
        {
            var vfx = Instantiate(VFX, transform.position, Quaternion.identity);
            Destroy(vfx, 1f);
            ShipController.Instance.SpawnShip();
        }
    }

    private void HandleScoreChanged(int score)
    {
        if (BulletList == null || BulletList.Length == 0)
            return;

        int delta = score - _startingScore;
        if (delta < 0) delta = 0;

        int tier = Mathf.Clamp(delta / 1200, 0, BulletList.Length - 1);
        if (tier != CurrentTierBullet)
        {
            CurrentTierBullet = tier;
            
        }
    }

    
    public void ResetToDefaultBulletTier()
    {
        CurrentTierBullet = 0;
        _startingScore = ScoreController.instance != null ? ScoreController.instance.Score : 0;
    }
}
