using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBG : MonoBehaviour
{
    [SerializeField]
    private float _speedFraction = 10f;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,1,0);
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null) {
            Debug.LogError("ERROR: Player is Null!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        BackgroundMovement();
    }

    private void BackgroundMovement() 
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if(!_player.GetSpeedBoostActive()) {
            transform.Translate(new Vector3(
            dtVelocity((_player.GetSpeed() / (_speedFraction - 1f)) * -horizontalInput),
            dtVelocity((_player.GetSpeed() / _speedFraction) * -verticalInput),
            0f));
        } else {
            transform.Translate(new Vector3(
            dtVelocity((_player.GetSpeed() / (_speedFraction - 1f)) * -horizontalInput * _player.GetSpeedMult()),
            dtVelocity((_player.GetSpeed() / _speedFraction) * -verticalInput * _player.GetSpeedMult()),
            0f));
        }

        if (transform.position.y <= .3f) {
            transform.position = new Vector3(transform.position.x, .3f, 0);
        } else if (transform.position.y > 1.6f) {
            transform.position = new Vector3(transform.position.x, 1.6f, 0);
        }
        if (transform.position.x <= -1.2f) {
            transform.position = new Vector3(-1.2f, transform.position.y, 0);
        } else if (transform.position.x > 1.2f) {
            transform.position = new Vector3(1.2f, transform.position.y, 0);
        }
    }

    private float dtVelocity(float s) 
    {
        return (s * Time.deltaTime);
    }
}
