using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance { get; private set; }

    public RacePoint[] Points;
    public RaceCat PlayerCat;
    public RaceCat[] AICats;

    private List<RaceCat> _raceCats = new List<RaceCat>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Add cats to single list
        _raceCats.Clear();
        _raceCats.Add(PlayerCat);
        foreach (RaceCat aiCat in AICats)
        {
            _raceCats.Add(aiCat);
        }

        // Assign race numbers
        for (int i = 0; i < _raceCats.Count; i++)
        {
            _raceCats[i].Index = i;
        }

        // Set at start line
        foreach (RaceCat raceCat in _raceCats)
        {
            raceCat.LastPoint = Points[0];
            raceCat.NextPoint = Points[1];
            raceCat.transform.position = raceCat.LastPoint.GetPosition(raceCat.Index);
        }

        // Move cats further outside forward
        for (int i = 0; i < _raceCats.Count; i++)
        {
            MoveForward(CalcuateDistancePenalty(i), _raceCats[i]);
        }

        StartRace();
    }

    public void StartRace()
    {
        foreach (RaceCat raceCat in _raceCats)
        {
            raceCat.CurrentSpeed = raceCat.MaxSpeed * .75f;
        }
    }

    private void Update()
    {
        foreach (RaceCat raceCat in _raceCats)
        {
            float distanceMovedThisFrame = raceCat.CurrentSpeed * Time.deltaTime;
            MoveForward(distanceMovedThisFrame, raceCat);
        }
    }

    private void MoveForward(float distance, RaceCat cat)
    {
        if (distance > Vector3.Distance(cat.Position, cat.NextPoint.GetPosition(cat.Index)))
        {
            if (cat.NextPoint == Points.Last())
            {
                cat.AfterFinishMoveForward();
            }
            else
            {
                cat.LastPoint = cat.NextPoint;
                cat.NextPoint = GetNextRacePoint(cat.LastPoint);
            }
        }

        cat.Position = Vector3.MoveTowards(
                cat.Position,
                cat.NextPoint.GetPosition(cat.Index),
                distance);
    }

    private RacePoint GetNextRacePoint(RacePoint point)
    {
        for (int i = 0; i < Points.Length - 1; i++)
        {
            if (point == Points[i])
            {
                return Points[i + 1];
            }
        }

        return Points.Last();
    }

    private float CalcuateDistancePenalty(int index)
    {
        float baseDistance = 0;
        float myDistance = 0;

        for (int i = 0; i < Points.Length - 1; i++)
        { 
            baseDistance += Vector3.Distance(Points[i].GetPosition(0), Points[i + 1].GetPosition(0));
            myDistance += Vector3.Distance(Points[i].GetPosition(index), Points[i + 1].GetPosition(index));
        }

        return myDistance - baseDistance;
    }
}
