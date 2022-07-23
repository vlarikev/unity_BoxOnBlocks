using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject borders;
    [SerializeField] private float power;
    [SerializeField] private float maxDarg;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LineRenderer lr;

    [SerializeField] private GameObject playerActiveSkin;
    [SerializeField] private GameObject playerInactiveSkin;

    Vector3 dragStartPos;
    Touch touch;

    private int jumpCount;
    private bool isPlay = false;
    private bool isOnce = false;

    private bool isTouchLock = true;
    private bool isZeroJump = true;

    bool pauseChecker = false;

    private void Start()
    {
        Application.targetFrameRate = 180;
        InvokeRepeating("IdleBounce", 1, 1.5f);
        isPlay = false;
        SetSkinActive(true);
        lr.positionCount = 0;
    }
    private void Update()
    {
        if (isPlay)
        {
            if (!isOnce)
            {
                isOnce = true;
                CancelInvoke();
            }
        }

        if (Input.GetMouseButtonDown(0) && !isZeroJump)
            isTouchLock = false;

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && !isZeroJump)
                isTouchLock = false;
        }

        if (Time.timeScale != 1f)
            lr.positionCount = 0;

        if (Input.touchCount > 0 && isPlay && Time.timeScale == 1f && !isTouchLock)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                DragStart();
            if (touch.phase == TouchPhase.Moved)
                Dragging();
            if (touch.phase == TouchPhase.Ended)
                DragEnd();
        }

        // Touches for mouse. Delete when release for android.
        if (isPlay && Time.timeScale == 1f && !isTouchLock)
            ForMouse();
    }

    // Touches for mouse. Delete when release for android.
    private void ForMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                pauseChecker = true;
            if (!pauseChecker)
            {
                dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                dragStartPos.z = 0f;
                lr.positionCount = 1;
                lr.SetPosition(0, dragStartPos);
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (!pauseChecker)
            {
                Vector3 draggingPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                draggingPos.z = 0f;
                lr.positionCount = 2;
                lr.SetPosition(1, draggingPos);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (!pauseChecker)
            {
                lr.positionCount = 0;

                Vector3 dragEndedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                dragEndedPos.z = 0f;

                if (jumpCount != 0)
                {
                    jumpCount--;
                    if (jumpCount == 0)
                    {
                        isTouchLock = true;
                        isZeroJump = true;
                        SetSkinActive(false);
                    }  
                
                    Vector3 force = dragStartPos - dragEndedPos;
                    Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDarg) * power;
                    rb.AddForce(clampedForce, ForceMode2D.Impulse);
                }
            }
            if (pauseChecker)
                pauseChecker = false;
        }
    }

    private void DragStart()
    {
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            pauseChecker = true;
        if (!pauseChecker)
        {
            dragStartPos = Camera.main.ScreenToWorldPoint(touch.position);
            dragStartPos.z = 0f;
            lr.positionCount = 1;
            lr.SetPosition(0, dragStartPos);
        }
    }
    private void Dragging()
    {
        if (!pauseChecker)
        {
            Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
            draggingPos.z = 0f;
            lr.positionCount = 2;
            lr.SetPosition(1, draggingPos);
        }
    }
    private void DragEnd()
    {
        if (!pauseChecker)
        {
            lr.positionCount = 0;

            Vector3 dragEndedPos = Camera.main.ScreenToWorldPoint(touch.position);
            dragEndedPos.z = 0f;

            if (jumpCount != 0)
            {
                jumpCount--;
                if (jumpCount == 0)
                {
                    isTouchLock = true;
                    isZeroJump = true;
                    SetSkinActive(false);
                }

                Vector3 force = dragStartPos - dragEndedPos;
                Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDarg) * power;
                rb.AddForce(clampedForce, ForceMode2D.Impulse);
            }
        }
        if (pauseChecker)
            pauseChecker = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && rb.velocity == new Vector2(0, 0))
        {
            borders.transform.position = new Vector3(borders.transform.position.x, transform.position.y + 6, borders.transform.position.z);
            isZeroJump = false;
            jumpCount = 2;
            SetSkinActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DeadZone")
            StartCoroutine(nameof(RestartDelay));
    }
    private IEnumerator RestartDelay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameScene");
    }

    private void IdleBounce()
    {
        rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
    }

    public void EventPlayGame()
    {
        jumpCount = 0;
        isPlay = true;
    }

    private void SetSkinActive(bool value)
    {
        if (value)
        {
            playerActiveSkin.SetActive(true);
            playerInactiveSkin.SetActive(false);
        }
        else
        {
            playerInactiveSkin.SetActive(true);
            playerActiveSkin.SetActive(false);
        }
    }
}
