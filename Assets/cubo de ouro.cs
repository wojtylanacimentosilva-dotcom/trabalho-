using UnityEngine;

public class cubodeouro : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider col;

    public float gravityMultiplier = 3f;
    public float jumpForce = 5f;
    public float moveSpeed = 5f;
    public float runSpeed = 9f;

    public float jumpCooldown = 2f;
    private float lastJumpTime = -2f;

    public float runDuration = 4f;
    public float runCooldown = 2f;

    private float runTimer = 0f;
    private float lastRunTime = -10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        col = GetComponent<BoxCollider>();
        if (col == null)
        {
            col = gameObject.AddComponent<BoxCollider>();
        }

        PhysicsMaterial noFriction = new PhysicsMaterial();
        noFriction.dynamicFriction = 0f;
        noFriction.staticFriction = 0f;
        noFriction.frictionCombine = PhysicsMaterialCombine.Minimum;

        col.material = noFriction;

        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void Update()
    {
        float move = 0;
        float currentSpeed = moveSpeed;

        bool canRun = Time.time >= lastRunTime + runCooldown;

        if (Input.GetKey(KeyCode.LeftShift) && canRun && runTimer < runDuration)
        {
            currentSpeed = runSpeed;
            runTimer += Time.deltaTime;
        }

        if (runTimer >= runDuration)
        {
            lastRunTime = Time.time;
            runTimer = 0f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            move = 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            move = -1;
        }

        rb.linearVelocity = new Vector3(move * currentSpeed, rb.linearVelocity.y, 0);

        if (Input.GetKeyDown(KeyCode.W) && Time.time >= lastJumpTime + jumpCooldown)
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            lastJumpTime = Time.time;
        }
    }

    void FixedUpdate()
    {
        if (rb != null && gravityMultiplier != 1f)
        {
            Vector3 extraGravity = Physics.gravity * (gravityMultiplier - 1f) * rb.mass;
            rb.AddForce(extraGravity, ForceMode.Force);
        }
    }
}