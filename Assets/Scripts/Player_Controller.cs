using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float speed = 4f;
    public float jumpPower = 9f;
    public LayerMask groundLayer; // Landable layer

    private const string animStand = "Player_Stand";
    private const string animRun = "Player_Run";
    public const string animJump = "Player_Jump";
    public const string animFall = "Player_Fall";
#nullable enable
    private string? lockAnim = null;
#nullable restore
    private string curAnim = animStand;
    private Animator animator;

    private Rigidbody2D rbody;
    private float axisH = 0.0f;
    private int onGround = 0;
    private bool lastJump = false;
    private int wantToJump = 0;
    private const int GRACE = 5;
    private bool frozen = false;
    public bool Grounded() => onGround == GRACE;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.freezeRotation = true;
        animator = GetComponent<Animator>();
        frozen = true;
    }

    private void Update()
    {
        frozen = !GameManager.Instance().Playing();
        var anim = animRun;
        // Move
        if (!frozen) axisH = Input.GetAxisRaw("Horizontal");
        else axisH = 0f;
        if (axisH > 0.0f) transform.localScale = new Vector3(1f, 1f, 1f);
        else if (axisH < 0.0f) transform.localScale = new Vector3(-1f, 1f, 1f);
        else anim = animStand;

        // Jump
        if (Input.GetButton("Jump"))
        {
            if (!lastJump) wantToJump = GRACE; // early jump grace time
            lastJump = true;
        }
        else lastJump = false;

        // Animation
        if (onGround < 1)
        {
            if (rbody.velocity.y > 0.0f) anim = animJump;
            else anim = animFall;
        }
        anim = lockAnim ?? anim;
        if (curAnim != anim)
        {
            curAnim = anim;
            animator.Play(anim);
        }

    }

    private void FixedUpdate()
    {
        if (frozen) return;
        if (Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer))
            onGround = GRACE;
        else if (onGround > 0) onGround--;
        float velX = rbody.velocity.x;
        velX = Mathf.Lerp(velX, axisH * speed, Grounded() ? 1f : 0.1f); // allow airbone control
        rbody.velocity = new Vector2(velX, rbody.velocity.y);

        if (wantToJump > 0)
        {
            wantToJump--;
            if (onGround > 0) // coyote time
            {
                wantToJump = 0;
                rbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            Win();
        }
        else if (collision.gameObject.CompareTag("Kill"))
        {
            Die();
        }
    }

    private void Die()
    {
        lockAnim = "Player_Hurt";
        GameManager.Instance().GameOver();

        GetComponent<CapsuleCollider2D>().enabled = false;
        rbody.velocity = Vector2.zero;
        rbody.AddForce(Vector2.up * 15f, ForceMode2D.Impulse);
    }

    private void Win()
    {
        lockAnim = "Player_Pose";
        GameManager.Instance().GameWin();
    }
}
