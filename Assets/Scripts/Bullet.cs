using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kan
{
    public class Bullet : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "BorderBullet")
            {
                Destroy(gameObject);
            }
        }
    }
}