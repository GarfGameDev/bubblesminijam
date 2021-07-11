using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    [SerializeField]
    private int _powerUpId;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player _player = other.GetComponent<Player>();

            switch (_powerUpId)
            {
                case 0:
                    _player.HealPlayer();
                    break;
                case 1:
                    _player.CanFireHomingBubble();
                    break;
                default:
                    Debug.Log("No Powerup ID detected");
                    break;
            }
            Destroy(this.gameObject);
        }    
    }
}
