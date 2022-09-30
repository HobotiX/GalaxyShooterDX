using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = .15f;
    private float _canFire = 1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null) {
            Debug.LogError("ERROR: Spawn Manager is Null!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire) {
            fireLaser();
        }
    }

    void playerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(
            dtVelocity(_speed * horizontalInput),
            dtVelocity(_speed * verticalInput),
            0f));

        // Keeps player in play area
        if (transform.position.y <= -5f) {
            transform.position = new Vector3(transform.position.x, 7f, 0);
        } else if (transform.position.y > 7f) {
            transform.position = new Vector3(transform.position.x, -5f, 0);
        }
        if (transform.position.x <= -10f) {
            transform.position = new Vector3(10f, transform.position.y, 0);
        } else if (transform.position.x > 10f) {
            transform.position = new Vector3(-10f, transform.position.y, 0);
        }
    }

    void fireLaser()
    {
        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + new Vector3(0, .8f, 0), Quaternion.identity);
    }

    private float dtVelocity(float s) 
    {
        return (s * Time.deltaTime);
    }

    public void damage()
    {
        _lives--;
        if (_lives < 1) {
            _spawnManager.onPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}
