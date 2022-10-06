using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    private float _speedMultiplier = 2.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = .15f;
    private float _canFire = 1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;

    [SerializeField]
    private bool _trippleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;

    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private GameObject _rightShipDamage;
    [SerializeField]
    private GameObject _leftShipDamage;

    [SerializeField]
    private int _score = 0;

    private UIManager _uiManager;

    [SerializeField]
    private AudioClip _laserSoundAudio;
    private AudioSource _audioSource;
    


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,0,0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null) {
            Debug.LogError("ERROR: Spawn Manager is Null!");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null) {
            Debug.LogError("ERROR: UI Manager is Null!");
        }
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null) {
            Debug.LogError("ERROR: Player Audio Source is Null!");
        } else {
            _audioSource.clip = _laserSoundAudio;
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

        if(!_isSpeedBoostActive) {
            transform.Translate(new Vector3(
            dtVelocity(_speed * horizontalInput),
            dtVelocity(_speed * verticalInput),
            0f));
        } else {
            transform.Translate(new Vector3(
            dtVelocity(_speed * horizontalInput * _speedMultiplier),
            dtVelocity(_speed * verticalInput * _speedMultiplier),
            0f));
        }
        

        // Keeps player in play area
        // if (transform.position.y <= -5f) {
        //     transform.position = new Vector3(transform.position.x, 7f, 0);
        // } else if (transform.position.y > 7f) {
        //     transform.position = new Vector3(transform.position.x, -5f, 0);
        // }
        // if (transform.position.x <= -10f) {
        //     transform.position = new Vector3(10f, transform.position.y, 0);
        // } else if (transform.position.x > 10f) {
        //     transform.position = new Vector3(-10f, transform.position.y, 0);
        // }
        if (transform.position.y <= -5f) {
            transform.position = new Vector3(transform.position.x, -5f, 0);
        } else if (transform.position.y > 7f) {
            transform.position = new Vector3(transform.position.x, 7f, 0);
        }
        if (transform.position.x <= -10f) {
            transform.position = new Vector3(-10f, transform.position.y, 0);
        } else if (transform.position.x > 10f) {
            transform.position = new Vector3(10f, transform.position.y, 0);
        }
    }

    void fireLaser()
    {
        _canFire = Time.time + _fireRate;
        
        if (!_trippleShotActive) {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, .8f, 0), Quaternion.identity);
        } else {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }

        _audioSource.Play();
        
    }

    private float dtVelocity(float s) 
    {
        return (s * Time.deltaTime);
    }

    public void damage()
    {
        if (!_isShieldActive) {
            _lives--;

            if (Random.Range(0,2) == 0 && _lives > 1) {
                _leftShipDamage.SetActive(true);
            } else if (_rightShipDamage.activeInHierarchy) {
                _leftShipDamage.SetActive(true);
            } else {
                _rightShipDamage.SetActive(true);
            }

            _uiManager.UpdateHealth(_lives);
            if (_lives < 1) {
                _spawnManager.onPlayerDeath();
                Destroy(this.gameObject);
            }
        } else {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
        }
    }

    public void TrippleShotActive()
    {
        _trippleShotActive = true;
        StartCoroutine(TrippleShotPowerDown());
    }

    IEnumerator TrippleShotPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        _trippleShotActive = false;
    }

    public void SpeedBoostActive() 
    {
        _isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDown());
    }

    IEnumerator SpeedBoostPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
    }

    public bool GetSpeedBoostActive()
    {
        return _isSpeedBoostActive;
    }

    public float GetSpeed() 
    {
        return _speed;
    }

    public float GetSpeedMult() 
    {
        return _speedMultiplier;
    }

    public void ShieldsActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void IncreaseScore() 
    {
        _score += 10;
        _uiManager.UpdateScore(_score);
    }
}
