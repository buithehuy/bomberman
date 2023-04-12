using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour {

    public float speed = 5f;

    private Rigidbody2D rb;

    public Animator animator; 

     private bool isAlive = true;


    void Start() {
        
            rb = GetComponent<Rigidbody2D>();
        
            animator = GetComponent<Animator>();
        
    }

    void FixedUpdate() {

        if (isAlive) {

            if(rb.velocity.magnitude < 0.1f) { // Check if the balloon is nearly stationary
                Vector2 movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                rb.velocity = movement * speed;
            } else {
                float x = 0, y = 0;

                // Chọn hướng di chuyển cố định
                if (Mathf.Abs(rb.velocity.x) < Mathf.Abs(rb.velocity.y)) {
                    // Di chuyển theo trục y
                    y = Mathf.Sign(rb.velocity.y) * speed;
                    //animator.SetTrigger("GhostIdle");
                } else {
                    // Di chuyển theo trục x
                    x = Mathf.Sign(rb.velocity.x) * speed;
                        if (Mathf.Sign(rb.velocity.x) > 0) {
                        animator.SetTrigger("GhostRight");
                    } else {
                        animator.SetTrigger("GhostLeft");
                    }
                }
                rb.velocity = new Vector2(x, y);
            }

        } else {
            rb.velocity = Vector2.zero;
        }

    }



    void OnCollisionEnter2D(Collision2D collision) {
        // Nếu balloon va chạm với layer Stage và chưa va chạm với Stage lần nào trước đó thì chọn hướng di chuyển ngẫu nhiên
        if (collision.gameObject.CompareTag("Block") 
        || collision.gameObject.CompareTag("Brick")
        || collision.gameObject.CompareTag("Item") ) {
            Vector2 movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            rb.velocity = movement * speed;
        }

        
    }

    void Update() {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Death") && stateInfo.normalizedTime >= 1.0f) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Explosion") && isAlive) {
                isAlive = false;
                animator.SetTrigger("Death");
            }
        }
    
}
