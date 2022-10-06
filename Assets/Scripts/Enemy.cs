using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate = 3.0f;
    private float _canFire = -1f;
    private bool _enemyDestroyed = false;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null) {
            Debug.LogError("ERROR: Player is Null!");
        }
        _anim = GetComponent<Animator>();
        if (_anim == null) {
            Debug.LogError("ERROR: Animator is Null!");
        }
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null) {
            Debug.LogError("ERROR: Enemy Audio Source is Null!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Time.time > _canFire && !_enemyDestroyed) {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++) {
                lasers[i].IsEnemyLaser();
            }
        }
    }

    void CalculateMovement() 
    {
        transform.Translate(Vector3.down * dtVelocity(_speed));

        if (transform.position.y < -6f) {
            float randoX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randoX, 8, 0);
            //Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        switch (other.tag) {
            case "Player":
                _audioSource.Play();
                _anim.SetTrigger("OnEnemyDeath");
                Destroy(GetComponent<Collider2D>());
                _enemyDestroyed = true;
                Destroy(this.gameObject, 2.8f);
                _speed = 0;
                if (_player != null) {
                    _player.damage();
                }
                break;

            case "Laser":
                _audioSource.Play();
                Destroy(other.gameObject);
                if (_player != null) {
                    _player.IncreaseScore();
                }
                _anim.SetTrigger("OnEnemyDeath");
                Destroy(GetComponent<Collider2D>());
                _enemyDestroyed = true;
                Destroy(this.gameObject, 2.8f);
                _speed = _speed /2;
                break;

            default:
                break;
        }
    }

    private float dtVelocity(float s) 
    {
        return (s * Time.deltaTime);
    }
}
