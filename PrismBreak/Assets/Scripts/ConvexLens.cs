using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvexLens : Lens 
{
    [SerializeField] private LineRenderer[] m_triColorBeams;
    [SerializeField] private Transform m_triColorJoinTF;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        AlignTricolorBeams();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            ActivateAllBeams(true);
        }
    }

    private void AlignTricolorBeams() {

        for (int i = 0; i < m_triColorBeams.Length; i++) {
            m_triColorBeams[i].SetPosition(1, m_triColorJoinTF.position);
        }

    }
}
