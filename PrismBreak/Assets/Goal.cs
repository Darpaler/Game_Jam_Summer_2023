using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private bool completed = false;

    [SerializeField]
    private BeamEmitter[] beamEmitters;

    [SerializeField]
    private int requiredRed = 1;
    private int currentRed = 0;
    
    [SerializeField]
    private int requiredBlue = 1;
    private int currentBlue = 0;

    [SerializeField]
    private int requiredGreen = 1;
    private int currentGreen = 0;

    [SerializeField]
    private GameObject goalBeam;

    private AudioSource audioSource;

    [SerializeField]
    private float goalSoundTime = 2f;

    [SerializeField]
    private string nextLevel;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        goalBeam.SetActive(false);

        foreach(BeamEmitter beam in beamEmitters)
        {
            beam.OnGoalHit += Beam_OnGoalHit;
            beam.OnGoalLost += Beam_OnGoalLost;
        }
    }

    private void Beam_OnGoalLost(object sender, System.EventArgs e)
    {
        BeamEmitter beam = (BeamEmitter)sender;

        if (beam.color == BeamEmitter.Color.Red)
            currentRed--;
        if (beam.color == BeamEmitter.Color.Green)
            currentGreen--;
        if (beam.color == BeamEmitter.Color.Blue)
            currentBlue--;
    }

    private void Beam_OnGoalHit(object sender, System.EventArgs e)
    {
        BeamEmitter beam = (BeamEmitter)sender;

        if (beam.color == BeamEmitter.Color.Red)
            currentRed++;
        if (beam.color == BeamEmitter.Color.Green)
            currentGreen++;
        if (beam.color == BeamEmitter.Color.Blue)
            currentBlue++;

        CheckGoal();
    }

    // Update is called once per frame
    void Update()
    {
        if (!completed && audioSource.time >= goalSoundTime)
        {
            completed = true;
            goalBeam.SetActive(true);
        }
        if(completed && !audioSource.isPlaying)
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
    
    void CheckGoal() 
    {
        if (requiredRed == currentRed && requiredGreen == currentGreen && requiredBlue == currentBlue)
        {
            audioSource.Play();
        }
    }
}
