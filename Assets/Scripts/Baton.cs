using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baton : MonoBehaviour
{
    [SerializeField] Vector2 startPosition = Vector3.zero;
    [SerializeField] float returnSpeed = 5;

    bool returningHome = false;
    bool isGrabbed = false;

    Vector2 lastPosition;
    float currentSpeed = 0;

    void Awake()
    {
        lastPosition = transform.position;
    }

    public void SetGrabbed() => isGrabbed = true;

    public void ReturnToStartPosition()
    {
        isGrabbed = false;
        returningHome = true;
    }

    void Update()
    {
        if (returningHome)
        {
            Vector2 goalPosition = startPosition;
            transform.position = Vector2.Lerp(transform.position, goalPosition, Time.deltaTime * returnSpeed);
            if (Vector2.Distance(transform.position, goalPosition) <= 0.1f)
                returningHome = false;
        }

        if (isGrabbed)
            DetectSpeed();
    }

    void DetectSpeed()
    {
        currentSpeed = Vector2.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        VolumeAdjuster.Instance.AdjustVolume(currentSpeed);
    }
}
