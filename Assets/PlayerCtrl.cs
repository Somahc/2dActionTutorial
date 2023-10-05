using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 900f;
    public LayerMask groundLayer;
    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer spRenderer;
    private bool isGround;

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

        anim.SetFloat("Speed", Mathf.Abs(x * speed)); // 歩きアニメーション

        // キャラの左右の向きを変える
        if(x<0){
            spRenderer.flipX = true;
        }else if(x>0){
            spRenderer.flipX = false;
        }

        rb2d.AddForce( Vector2.right * x * speed );

        if(Input.GetButtonDown("Jump") && isGround){
            anim.SetBool("IsJump", true);
            rb2d.AddForce( Vector2.up * jumpForce );
        }

        if(isGround){ // 地面についているときはジャンプと落下モーションをfalseにする
            anim.SetBool("IsJump", false);
            anim.SetBool("IsFall", false);
        }

        float velX = rb2d.velocity.x;
        float velY = rb2d.velocity.y;

        if(velY > 0.5f){ // velY上向でジャンプ中
            anim.SetBool("IsJump", true);
        }
        if(velY < -0.5f){ // velY下向で落下中
            anim.SetBool("IsFall", true);
        }

        if(Mathf.Abs(velX) > 5 ){
            if(velX > 5.0f){
                rb2d.velocity = new Vector2(5.0f, velY);
            }else if(velX < -5.0f){
                rb2d.velocity = new Vector2(-5.0f, velY);
            }
        }
    }

    private void FixedUpdate() {
        isGround = false;

        Vector2 groundPos = 
            new Vector2(transform.position.x, transform.position.y);

        Vector2 groundArea = new Vector2(0.5f, 0.5f);

        Debug.DrawLine(groundPos + groundArea, groundPos - groundArea, Color.red);

        isGround =
            Physics2D.OverlapArea(
                groundPos + groundArea,
                groundPos - groundArea,
                groundLayer
            );

            Debug.Log(isGround);
    }
}
