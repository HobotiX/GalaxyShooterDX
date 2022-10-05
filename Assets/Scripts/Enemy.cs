using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private Player _player;
    private Animator _anim;

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
    }

    // Update is called once per frame
    void Update()
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
                _anim.SetTrigger("OnEnemyDeath");
                Destroy(this.gameObject, 2.8f);
                _speed = 0;
                if (_player != null) {
                    _player.damage();
                }
                break;

            case "Laser":
                Destroy(other.gameObject);
                if (_player != null) {
                    _player.IncreaseScore();
                }
                _anim.SetTrigger("OnEnemyDeath");
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
