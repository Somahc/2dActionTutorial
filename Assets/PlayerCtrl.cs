using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer spRenderer;

    // Start is called before the first frame update
    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        this.anim = GetComponent<Animator>();
        this.spRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal"); // 左入力で-1、右入力で1、入力なしで0

        anim.SetFloat("Speed", Mathf.Abs(x * speed));

        // キャラの左右の向きを変える
        if(x<0){
            spRenderer.flipX = true;
        }else if(x>0){
            spRenderer.flipX = false;
        }

        rb2d.AddForce( Vector2.right * x * speed );

    }
}
