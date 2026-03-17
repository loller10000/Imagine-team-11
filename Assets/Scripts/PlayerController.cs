using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 10f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 10f;
    [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 1f;
    [SerializeField, Range(0f, 10f)] private float jumpHeight = 2f;
    [SerializeField, Range(0f, 5f)] private int maxAirJumps = 0;
    [SerializeField, Range(0f, 90f)] private float maxGroundAngle = 25f;
    
    private Vector3 velocity;
    private Vector3 desiredVelocity;
    private Vector3 contactNormal;
    private Rigidbody body;
    
    [SerializeField] private bool desiredJump;
    [SerializeField] private bool onGround;
    [SerializeField] private int jumpPhase;
    private float minGroundDotProduct;

    private void OnValidate()
    {
        minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
    }
    
    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        OnValidate();
    }

    private void Update()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        
        desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        desiredJump |= Input.GetButtonDown("Jump");
    }

    private void FixedUpdate()
    {
        UpdateState();
        AdjustVelocity();
        
        if (desiredJump)
        {
            desiredJump = false;
            Jump();
        }
        
        body.linearVelocity = velocity;
        onGround = false;
    }
    
    private void UpdateState()
    {
        velocity = body.linearVelocity;

        if (onGround)
        {
            jumpPhase = 0;
        }
        
        else
        {
            contactNormal = Vector3.up;
        }
    }
    
    private void Jump()
    {
        if (onGround || jumpPhase < maxAirJumps)
        {
            jumpPhase += 1;
            
            var jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
            var alignedSpeed = Vector3.Dot(velocity, contactNormal);
            
            if (alignedSpeed > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed = alignedSpeed, 0f);
            }
            
            //velocity.y += jumpSpeed;
            velocity += contactNormal * jumpSpeed;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        EvaluateCollision(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        EvaluateCollision(collision);
    }

    private void EvaluateCollision(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.GetContact(i).normal;
            
            if (normal.y >= minGroundDotProduct)
            {
                onGround = true;
                contactNormal = normal;
            }
        }
    }

    private Vector3 ProjectOnContactPlane(Vector3 vector)
    {
        return vector - contactNormal * Vector3.Dot(vector, contactNormal);
    }

    private void AdjustVelocity()
    {
        var xAxis = ProjectOnContactPlane(Vector3.right).normalized;
        var zAxis = ProjectOnContactPlane(Vector3.forward).normalized;
        
        var currentX = Vector3.Dot(velocity, xAxis);
        var currentZ = Vector3.Dot(velocity, zAxis);

        var acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        var maxSpeedChange = acceleration * Time.deltaTime;
        
        var newX = Mathf.MoveTowards(currentX, desiredVelocity.x, maxSpeedChange);
        var newZ = Mathf.MoveTowards(currentZ, desiredVelocity.z, maxSpeedChange);
        
        velocity += xAxis * (newX - currentX) + zAxis * (newZ - currentZ);
    }

    private void Move()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        // playerInput.Normalize(); // More limited movement. 
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        
        desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        var maxSpeedChange = maxAcceleration * Time.deltaTime;
        
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
        
        var displacement = velocity * Time.deltaTime;
        var newPosition = transform.localPosition + displacement;
        
        //transform.localPosition = new Vector3(playerInput.x, 0.5f, playerInput.y);
        
        transform.localPosition = newPosition;
    }
}
