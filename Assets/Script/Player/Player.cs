using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 10f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float verticalAxis = Input.GetAxisRaw("Vertical");
        float HorizontalAxis = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(HorizontalAxis, verticalAxis) * speed;
    }
}
