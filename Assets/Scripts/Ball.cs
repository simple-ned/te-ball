using UnityEngine;

public class Ball : MonoBehaviour
{
    private const string EnvironmentTag = "Environment";
    private const string BallTag = "Ball";

    public int Id { get; private set; }

    private Vector2 direction;
    private Color color;
    private bool isDead;

    [SerializeField]
    private float movementSpeed = 5f;
    [SerializeField]
    private Collider2D colliderSelf;
    [SerializeField]
    private SpriteRenderer rendererSelf;
    [SerializeField]
    private Rigidbody2D rigitbody;

    [Header("VFX Fields")]
    [SerializeField]
    private ParticleSystem movementParticles;
    [SerializeField]
    private ParticleSystem destroyParticles;

    private void FixedUpdate()
    {
        rigitbody.MovePosition(rigitbody.position + direction * Time.fixedDeltaTime * movementSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals(BallTag)) {
            var otherBall = other.GetComponent<Ball>();
            GameManager.Instance.RegisterCollision(Id, otherBall.Id);
            Die();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals(EnvironmentTag)) {
            transform.Translate(-direction * (other.bounds.size + colliderSelf.bounds.size));
        }
    }

    private void SetParticleColor(ParticleSystem particles, Color color)
    {
        var particalSettings = movementParticles.main;
        particalSettings.startColor = color;
    }

    public void Die()
    {
        // To avoid executing Die method twice
        if (isDead) {
            return;
        }

        isDead = true;
        var effect = Instantiate(destroyParticles, transform.position, Quaternion.identity);
        SetParticleColor(effect, color);
        Destroy(gameObject);
    }

    public void Initialize(int id, Color color, Vector2 direction) {
        Id = id;
        this.color = color;
        this.direction = direction;

        rendererSelf.color = color;
        SetParticleColor(movementParticles, color);
    }
}
