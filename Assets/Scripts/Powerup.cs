using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerupID; //0=TripleShot, 1=Speed, 2=Shields

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <-4.5f) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player") {

            Player player = other.transform.GetComponent<Player>();

            if (player != null) {
                switch (_powerupID) {

                    case 0: //Triple Shot
                        //Debug.Log("Triple Shot");
                        player.TrippleShotActive();
                        break;

                    case 1: // Speed
                        //Debug.Log("Speed Boost");
                        player.SpeedBoostActive();
                        break;

                    case 2: // Shields
                        //Debug.Log("Shields");
                        player.ShieldsActive();
                        break;

                    default: // No Powerup
                        Debug.Log("Default");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
