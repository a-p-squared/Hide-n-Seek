using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpotScript : MonoBehaviour
{
    public GameObject[] peakSpots;
   

    public GameObject getRandomPeakSpot()
    {
        return peakSpots[Random.Range(0, peakSpots.Length)];
    }
}
