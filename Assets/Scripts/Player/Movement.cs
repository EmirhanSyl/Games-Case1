using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 500f;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;

    private Touch touch;

    private Vector3 touchDown;
    private Vector3 touchUp;

    private bool dragStarted;
    private bool isMoving;

    private AnimationManager animationManager;
    
    void Start()
    {
        animationManager = GetComponent<AnimationManager>();
    }

    
    void Update()
    {
        if (Health.instance.isDead) return;

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                dragStarted = true;
                isMoving = true;

                touchDown = touch.position;
                touchUp = touch.position;
            }
        }

        if (dragStarted)
        {
            if (touch.phase == TouchPhase.Moved)
            {
                touchDown = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touchDown = touch.position;
                isMoving = false;
                dragStarted = false;
            }

            transform.rotation = Quaternion.RotateTowards(transform.rotation, CalculateTempRotation(), rotationSpeed * Time.deltaTime);
            transform.Translate(Time.deltaTime * movementSpeed * Vector3.forward);
        }

        ClampPosition();
        SetMoveState();
    }

    public void SetMoveState()
    {
        if (isMoving)
        {
            animationManager.animationStatesDropdown = AnimationManager.animationStates.walk;
        }
        else
        {
            animationManager.animationStatesDropdown = AnimationManager.animationStates.idle;
        }

    }

    void ClampPosition()
    {
        float x = Mathf.Clamp(transform.position.x, minX, maxX);
        float z = Mathf.Clamp(transform.position.z, minZ, maxZ);
        float y = transform.position.y;

        transform.position = new Vector3(x, y, z);
    }

    Quaternion CalculateTempRotation()
    {
        Vector3 tempVec = (touchDown - touchUp).normalized;
        tempVec.z = tempVec.y;
        tempVec.y = 0;

        Quaternion temp = Quaternion.LookRotation(tempVec, Vector3.up);
        return temp;
    }
}
