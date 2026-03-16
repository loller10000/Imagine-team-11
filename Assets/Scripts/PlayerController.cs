using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 10f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 10f;
    //[SerializeField, Range(0f, 1f)] private float bounciness = 0.5f;
    //[SerializeField] private Rect allowedArea = new Rect(-5f, -5f, 10f, 10f);
    
    private Vector3 velocity;

    private void Update()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        //playerInput.Normalize();
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        
        var desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        var maxSpeedChange = maxAcceleration * Time.deltaTime;
        
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
        
        var displacement = velocity * Time.deltaTime;
        var newPosition = transform.localPosition + displacement;
        
        // if (newPosition.x < allowedArea.xMin) 
        // {
        //     newPosition.x = allowedArea.xMin;
        //     velocity.x = -velocity.x * bounciness;
        // }
        // else if (newPosition.x > allowedArea.xMax) 
        // {
        //     newPosition.x = allowedArea.xMax;
        //     velocity.x = -velocity.x * bounciness;
        // }
        // if (newPosition.z < allowedArea.yMin) 
        // {
        //     newPosition.z = allowedArea.yMin;
        //     velocity.z = -velocity.z * bounciness;
        // }
        // else if (newPosition.z > allowedArea.yMax) 
        // {
        //     newPosition.z = allowedArea.yMax;
        //     velocity.z = -velocity.z * bounciness;
        // }
        
        transform.localPosition = newPosition;
    }

    private void Move()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        // playerInput.Normalize(); // More limited movement. 
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        
        var desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        var maxSpeedChange = maxAcceleration * Time.deltaTime;
        
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
        
        var displacement = velocity * Time.deltaTime;
        var newPosition = transform.localPosition + displacement;
        
        //transform.localPosition = new Vector3(playerInput.x, 0.5f, playerInput.y);
        
        transform.localPosition = newPosition;
    }
}
