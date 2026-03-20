using UnityEngine;

/// <summary>
/// WIP script following https://catlikecoding.com/unity/tutorials/prototypes/runner-2/
/// Since it's a tutorial for a 2D Game, i'm attempting to follow allong
/// Converting vector2's to Vector3 and such hoping to get it compatible in 3D on the fly.
/// The original snippet / logic is commented out unless it's indifferent to 2D vs. 3D context.
/// Another Note to self, every X-axis movement has probably been altered to Z.
/// </summary>

public class Runner : MonoBehaviour
{
    [SerializeField] private Light pointLight;
    [SerializeField] private ParticleSystem trailSystem;
    // [SerializeField] private ParticleSystem explosionSystem;

    // [SerializeField, Min(0f)] private float startSpeedX = 5f;
    [SerializeField, Min(0f)] private float startSpeedZ = 5f;

    private MeshRenderer meshRenderer;

    private Vector3 position;
    public Vector3 Position => position;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
        pointLight.enabled = false;
    }

    public void StartNewGame()
    {
        position = Vector3.zero; 
        transform.localPosition = position;
        meshRenderer.enabled = true;
        pointLight.enabled = true;
        trailSystem.Clear();
        trailSystem.Play();

        // explosionSystem.Clear();
        SetTrailEmission(true);
    }


    public void Explode()
    {
        meshRenderer.enabled = false;
        pointLight.enabled = false;
        transform.localPosition = position;

        SetTrailEmission(false);
        //explosionSystem.Emit(explosionSystem.main.maxParticles);
    }

    public void SetTrailEmission(bool enabled)
    {
        ParticleSystem.EmissionModule emission = trailSystem.emission;
        emission.enabled = enabled;
    }

    public bool Run(float dt)
    {
        if(position.z > 25f)
        {
            Explode();
            return false;
        }

        position.z += startSpeedZ * dt;
        return true;
    }

    public void UpdateVisualization()
    {
        transform.localPosition = position;
    }
}
