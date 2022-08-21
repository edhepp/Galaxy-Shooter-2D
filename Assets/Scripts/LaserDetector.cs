using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDetector : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            return;
        }
        if (other.name == "Laser(Clone)")
        {
            int _chanceToDodge = Random.Range(0, 4);
            if (this.transform.position.x < other.transform.position.x && _chanceToDodge == 2)
            {
                this.GetComponentInParent<Enemy>().DodgeLaser(-1.5f);
            }
            if (this.transform.position.x > other.transform.position.x && _chanceToDodge == 2)
            {
                this.GetComponentInParent<Enemy>().DodgeLaser(1.5f);
            }
        }
    }
}
