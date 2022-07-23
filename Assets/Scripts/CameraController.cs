using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject deadZone;
    private float startPosY;

    private void Start()
    {
        startPosY = transform.position.y;
    }

    private void Update()
    {
        if (player.GetComponent<Rigidbody2D>().velocity == new Vector2(0, 0))
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, player.transform.position.y + 5, -10f), .25f);

        if (player.transform.position.y < transform.position.y - 7 && !(deadZone.transform.position.y + 12 >= transform.position.y))
        {
            if(transform.position.y - 14 < startPosY)
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, startPosY, -10f), 1f);
            else if (transform.position.y > deadZone.transform.position.y + 20)
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y - 14, -10f), 1f);
        }
    }
}
