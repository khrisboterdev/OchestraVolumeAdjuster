using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickGrabber : MonoBehaviour
{
    [SerializeField] Vector2 offset = Vector2.zero;
    GameObject baton = null;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            LookForBaton(Input.mousePosition);
        else if (Input.GetMouseButton(0) && baton != null)
            UpdateBatonPosition(Input.mousePosition);
        if (Input.GetMouseButtonUp(0))
            DropBaton();

    }

    void LookForBaton(Vector2 mousePosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                baton = hit.collider.gameObject;
                baton.GetComponent<Baton>().SetGrabbed();
                VolumeAdjuster.Instance.StartMusicPlay();
            }
        }
    }

    void UpdateBatonPosition(Vector2 mousePosition)
    {
        Vector3 batonPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        batonPosition.x += offset.x;
        batonPosition.y += offset.y;
        batonPosition.z = 0;
        baton.transform.position = batonPosition;
    }

    void DropBaton()
    {
        if (baton == null) return;
        VolumeAdjuster.Instance.StopMusicPlay();
        baton.GetComponent<Baton>().ReturnToStartPosition();
        baton = null;        
    }
}
