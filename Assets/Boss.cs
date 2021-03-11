using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float randomX;
    public float moveTimer;
    public float yDistanceFromPlayer;
    public float shootTimer;
    public EnemyHealth health;

    public RectTransform rect;

    private Vector3 whereShouldIBe;
    
    [SerializeField] public float moveRate = 2f;
    [SerializeField] public float shootRate = 5f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootWidth;

    private GameObject cam;
    
    // Start is called before the first frame update
    void Start()
    {
        whereShouldIBe = transform.position;
        cam = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        moveTimer += Time.deltaTime;
        shootTimer += Time.deltaTime;

        if ( moveTimer > moveRate )
        {
            randomX = ((maxX-minX) * Random.value) + minX;
            whereShouldIBe.x = randomX;
            moveTimer = 0;
        }

        if ( shootTimer > shootRate )
        {
            shootTimer = 0;
            Shoot();
        }

        rect.sizeDelta = new Vector2(( 204.8f / health.maxHealth ) * health.GetHealth(), 20.56f);

        
        transform.position = new Vector3(transform.position.x, cam.transform.position.y + yDistanceFromPlayer, transform.position.z);
        whereShouldIBe.y = transform.position.y;
        gameObject.transform.position = Vector3.Lerp(transform.position, whereShouldIBe, moveTimer/moveRate);
    }

    private void Shoot()
    {
        GameObject temp = Instantiate(bullet);
        temp.transform.position = transform.position + Vector3.right * shootWidth;
        temp.transform.rotation = Quaternion.Euler(0,0,180);
        temp = Instantiate(bullet);
        temp.transform.position = transform.position - Vector3.right * shootWidth;
        temp.transform.rotation = Quaternion.Euler(0,0,180);
    }
}
