using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamEmitter : MonoBehaviour
{
    // Properties
    private LineRenderer beam;
    private bool goalHit = false;
    private bool goalEventCompleted = false;

    [SerializeField]
    private float maxBeamDistance = 300;

    public event EventHandler OnGoalHit;
    public event EventHandler OnGoalLost;

    public enum Color { Red, Blue, Green, White}
    public Color color;

    // Start is called before the first frame update
    void Start()
    {
        // Get Components
        beam = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CastBeam();
    }

    void CastBeam()
    {
        Vector3 position = transform.position;
        Vector3 direction = transform.forward;
        bool hitMirror = false;
        
        beam.positionCount = 1;

        beam.SetPosition(0, position);

        do
        {
            beam.positionCount++;
            Ray ray = new Ray(position, direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                position = hit.point;
                direction = Vector3.Reflect(direction, hit.normal);
                beam.SetPosition(beam.positionCount - 1, hit.point);
            }
            else
            {
                beam.SetPosition(beam.positionCount - 1, direction * maxBeamDistance);
            }

            hitMirror = hit.transform && hit.transform.tag == "Mirror";

            goalHit = hit.transform && hit.transform.tag == "Goal";

        }while (hitMirror);

        if (goalHit && !goalEventCompleted)
        {
            OnGoalHit?.Invoke(this, EventArgs.Empty);
            goalEventCompleted = true;
        }
        
        if (!goalHit && goalEventCompleted)
        {
            goalEventCompleted = false;
            OnGoalLost?.Invoke(this, EventArgs.Empty);
        }
    }
}
