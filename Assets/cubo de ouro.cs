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

    public Vector3 respawnPosition = new Vector3(-14f, 2f, 4f);

    // ADM
    private bool modoADM = false;
    public float flySpeed = 6f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>();

        col = GetComponent<BoxCollider>();
        if (col == null)
            col = gameObject.AddComponent<BoxCollider>();

        PhysicsMaterial noFriction = new PhysicsMaterial();
        noFriction.dynamicFriction = 0f;
        noFriction.staticFriction = 0f;
        noFriction.frictionCombine = PhysicsMaterialCombine.Minimum;

        col.material = noFriction;

        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        gameObject.tag = "Player";
    }

    void Update()
    {
        // ATIVAR / DESATIVAR ADM COM M
        if (Input.GetKeyDown(KeyCode.M))
        {
            modoADM = !modoADM;

            if (modoADM)
            {
                rb.useGravity = false;
                col.enabled = false;
            }
            else
            {
                rb.useGravity = true;
                col.enabled = true;
            }
        }

        // MODO ADM (VOAR)
        if (modoADM)
        {
            float x = 0;
            float y = 0;

            if (Input.GetKey(KeyCode.D)) x = 1;
            if (Input.GetKey(KeyCode.A)) x = -1;
            if (Input.GetKey(KeyCode.W)) y = 1;
            if (Input.GetKey(KeyCode.S)) y = -1;

            transform.position += new Vector3(x, y, 0) * flySpeed * Time.deltaTime;
            return;
        }

        // MOVIMENTO NORMAL
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

        if (Input.GetKey(KeyCode.D)) move = 1;
        if (Input.GetKey(KeyCode.A)) move = -1;

        rb.linearVelocity = new Vector3(move * currentSpeed, rb.linearVelocity.y, 0);

        if (Input.GetKeyDown(KeyCode.W) && Time.time >= lastJumpTime + jumpCooldown)
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            lastJumpTime = Time.time;
        }
    }

    void FixedUpdate()
    {
        if (!modoADM && gravityMultiplier != 1f)
        {
            Vector3 extraGravity = Physics.gravity * (gravityMultiplier - 1f) * rb.mass;
            rb.AddForce(extraGravity, ForceMode.Force);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Perigo"))
        {
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = respawnPosition;
        rb.linearVelocity = Vector3.zero;
    }
}