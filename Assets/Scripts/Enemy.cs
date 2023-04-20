using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kan
{
    public class Enemy : MonoBehaviour
    {
        public float speed;
        public int health;

        SpriteRenderer spriteRenderer;
        Rigidbody2D rigid;
        public Sprite[] sprites;
    }
}