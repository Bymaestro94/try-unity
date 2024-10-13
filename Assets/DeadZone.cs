using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Target" || collision.tag == "Player") {
            Time.timeScale = 0;
        }
    }
}
