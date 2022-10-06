using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    private bool _isEnemyLaser = false;

    // Update is called once per frame
    void Update()
    {
        if (!_isEnemyLaser) {
            PlayerLaser();
        } else {
            EnemyLaser();
        }
    }

    void PlayerLaser()
    {
        transform.Translate(Vector3.up * _speed * 1.5f * Time.deltaTime);
        if (transform.position.y > 8f) {
            if (transform.parent != null) {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    void EnemyLaser()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -8f) {
            if (transform.parent != null) {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void IsEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player" && _isEnemyLaser == true) {
            Player player = other.GetComponent<Player>();
            if (player != null) {
                Destroy(this.gameObject);
                player.damage();
            } else {
                Debug.LogError("ERROR: Laser->Player is Null!");
            }
        }
    }
}
