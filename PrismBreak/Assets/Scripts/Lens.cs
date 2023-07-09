using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Lens : MonoBehaviour
{
    [SerializeField] private LineRenderer[] m_childBeams;

    // Start is called before the first frame update
    protected virtual void Start() {
        
        if (m_childBeams.Length == 0) {
            
            m_childBeams = GetComponentsInChildren<LineRenderer>();
        }

        ActivateAllBeams(false);
    }

    public void ActivateAllBeams(bool value) {
        
        for (int i = 0; i < m_childBeams.Length; i++) {
            
            if (m_childBeams[i] == null) continue;

            m_childBeams[i].gameObject.SetActive(value);
        }
    }
}
