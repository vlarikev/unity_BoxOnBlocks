using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] private float point1Offset;
    [SerializeField] private float point2Offset;

    private float point1;
    private float point2;

    private bool isSwitch = true;

    private void Start()
    {
        point1 = transform.position.x - point1Offset;
        point2 = transform.position.x + point2Offset;

        InvokeRepeating("SwitchDelay", 3, 3);
    }
    private void Update()
    {
        if (isSwitch && point1 < transform.position.x)
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), 2 * Time.deltaTime);
        if (!isSwitch && point2 > transform.position.x)
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), 2 * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.transform.SetParent(transform);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.transform.SetParent(null);
    }
    private void SwitchDelay()
    {
        isSwitch = !isSwitch;
    }
}
