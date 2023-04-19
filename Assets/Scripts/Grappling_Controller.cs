using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grappling_Controller : MonoBehaviour
{
    LineRenderer lineRenderer;

    [SerializeField]
    LayerMask grapplableMask;
    [SerializeField]
    float maxDistance = 10f;
    [SerializeField]
    float grappleSpeed = 10f;
    [SerializeField]
    float grappleShootSpeed = 10f;
    [SerializeField]
    float grappleStrength = 5f;

    bool isGrappling = false;

    Vector2 target;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed && !isGrappling)
        {
            StartGrapple();
        }
        else if (context.canceled && isGrappling)
        {
            StopGrapple();
        }
    }

    private void StartGrapple()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grapplableMask);

        if (hit.collider != null)
        {
            Debug.Log("Start grapple");
            isGrappling = true;
            target = hit.point;
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, target);

            StartCoroutine(Grapple());
        }
    }

    private void StopGrapple()
    {
      //  Debug.Log("Stop");
        isGrappling = false;
        lineRenderer.enabled = false;
    }

    IEnumerator Grapple()
    {
        float t = 0;
        float time = Vector2.Distance(transform.position, target) / grappleStrength;

        while (isGrappling)
        {
           // Debug.Log("isGrappling");
            t += Time.deltaTime;
            Vector2 newPos = Vector2.MoveTowards(transform.position, target, grappleStrength * Time.deltaTime);
            transform.position = newPos;
            lineRenderer.SetPosition(0, transform.position);
            if (Vector2.Distance(transform.position, target) < 1f)
            {
                t = time;
            }
            yield return null;
        }
    }
}
