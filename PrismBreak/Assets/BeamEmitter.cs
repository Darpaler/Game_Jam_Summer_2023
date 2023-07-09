using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamEmitter : MonoBehaviour
{
    // Properties
    private LineRenderer beam;

    public enum BeamColorType {
        White,
        Red,
        Green,
        Blue
    }
    [SerializeField] private BeamColorType m_beamColor = BeamColorType.White;
    public BeamColorType BeamColor { get { return m_beamColor; } }


    [SerializeField]
    private float maxBeamDistance = 300;

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

        } while (hitMirror);
    }
}
