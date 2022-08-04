using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [field: SerializeField]
    public bool PlayerDetected { get; private set; }
    public Vector2 DirectionToTarget => target.transform.position - detectorOrigin.position;
    public bool PlayerStayNear => DirectionToTarget.x > -1.5f && DirectionToTarget.x < 1.5f && DirectionToTarget.y < 1f;

    [Header("OverlapBox parameters")]
    [SerializeField] Transform detectorOrigin;
    [SerializeField] Vector2 detectorSize = Vector2.one;
    [SerializeField] Vector2 detectorOriginOffset = Vector2.zero;
    [SerializeField] float detectionDelay = 0.3f;
    [SerializeField] LayerMask detectorLayerMask;

    [Header("Gizmo parameters")]
    [SerializeField] Color gizmoIdleColor = Color.green;
    [SerializeField] Color gizmoDetectedColor = Color.red;
    [SerializeField] bool showGizmos = true; 

    private GameObject target;

    public GameObject Target
    {
        get => target;
        private set
        {
            target = value;
            PlayerDetected = target != null;
        }
    }

    private void Start()
    {
        StartCoroutine(DetectionCoroutine());
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionDelay);
        PerformeDetection();
        StartCoroutine(DetectionCoroutine());
    }
    
    private void PerformeDetection()
    {
        Collider2D collider =
            Physics2D.OverlapBox(
                (Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize, 0, detectorLayerMask);
        if(collider != null)
        {
            Target = collider.gameObject;
        }
        else
        {
            Target = null;
        }
    }

    private void OnDrawGizmos()
    {
        if(showGizmos && detectorOrigin != null)
        {
            Gizmos.color = gizmoIdleColor;
            if (PlayerDetected)
                Gizmos.color = gizmoDetectedColor;
            Gizmos.DrawCube((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize);
        }
    }
}
