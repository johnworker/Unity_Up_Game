using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kan
{
    public class Item : MonoBehaviour
    {
        [Header("Ãþ«¬")]
        public string type;
        Rigidbody2D rigid;

        void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            rigid.velocity = Vector2.down * 0.1f;
        }
    }
}