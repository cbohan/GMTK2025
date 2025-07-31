using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance { get; private set; }

    public RacePoint[] Points;
    public RaceCat PlayerCat;

    private List<RaceCat> _raceCats = new List<RaceCat>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _raceCats.Clear();
        _raceCats.Add(PlayerCat);
        // TODO: Add other cats to list of race cats

        foreach (RaceCat raceCat in _raceCats)
        {
            raceCat.LastPoint = Points[0];
            raceCat.NextPoint = Points[1];
            raceCat.transform.position = raceCat.LastPoint.Position;
        }
    }

    private void Update()
    {
        foreach (RaceCat raceCat in _raceCats)
        {
            float distanceMovedThisFrame = raceCat.CurrentSpeed * Time.deltaTime;
            if (distanceMovedThisFrame > Vector3.Distance(raceCat.Position, raceCat.NextPoint.Position))
            {
                raceCat.LastPoint = raceCat.NextPoint;
                raceCat.NextPoint = GetNextRacePoint(raceCat.LastPoint);
            }

            raceCat.Position = Vector3.MoveTowards(
                    raceCat.Position,
                    raceCat.NextPoint.Position,
                    distanceMovedThisFrame);
        }
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
}
