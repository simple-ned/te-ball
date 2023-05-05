using UnityEngine;
using UnityEngine.EventSystems;

public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ballPrefab;
    [SerializeField]
    private GameObject ballTemplatePrefab;

    [SerializeField]
    private Color[] ballColors;
    private int ballCount = 0;
    private GameObject ballTemplate;

    private readonly Vector2[] directions = new Vector2[]
    {
        Vector2.up,
        //Vector2.down,
        //Vector2.left,
        Vector2.right
    };

    private void OnMouseUpAsButton()
    {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        if (GameManager.Instance.IsGameLost) {
            return;
        }

        var position = GetMouseWorldPosition();

        var ball = Instantiate(ballPrefab, position, Quaternion.identity).GetComponent<Ball>();
        var direction = directions[ballCount % directions.Length];
        var color = ballColors[ballCount % ballColors.Length];
        ball.Initialize(++ballCount, color, direction);
        GameManager.Instance.Stats.Score++;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        CreateBallTemplate();
    }

    private void OnMouseOver()
    {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        if (GameManager.Instance.IsGameLost) {
            return;
        }

        if (ballTemplate == null) {
            CreateBallTemplate();
        }

        ballTemplate.transform.position = GetMouseWorldPosition();
    }

    private void OnMouseExit()
    {
        DestroyBallTemplate();
    }

    private Vector3 GetMouseWorldPosition()
    {
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        return position;
    }

    private void CreateBallTemplate()
    {
        ballTemplate = Instantiate(ballTemplatePrefab);
        var direction = directions[ballCount % directions.Length];
        var velocityOverLifeTime = ballTemplate.GetComponentInChildren<ParticleSystem>().velocityOverLifetime;
        velocityOverLifeTime.x = - direction.x * 5f;
        velocityOverLifeTime.y = - direction.y * 5f;
        velocityOverLifeTime.enabled = true;
    }

    private void DestroyBallTemplate()
    {
        if (ballTemplate != null) {
            Destroy(ballTemplate);
            ballTemplate = null;
        }
    }

    public void Finish()
    {
        DestroyBallTemplate();
    }
}
