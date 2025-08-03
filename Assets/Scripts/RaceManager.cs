using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance { get; private set; }

    [HideInInspector] public bool IsRaceFinished;

    public RacePoint[] Points;
    public RaceCat PlayerCat;
    public RaceCat[] AICats;

    [SerializeField] private CanvasGroup _placeTextCanvasGroup;
    [SerializeField] private TMP_Text _placeText;
    [SerializeField] private CanvasGroup _drawingCanvasGroup;

    private List<RaceCat> _raceCats = new List<RaceCat>();
    private int _aiCatsThatHaveFinishedTheRace = 0;
    private float _aiCatSpeedMult = 1f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Add cats to single list
        _raceCats.Clear();
        _raceCats.Add(PlayerCat);
        PlayerCat.SetData(InterSceneData.PlayerCat);


        for (int i = 0; i < 3; i++)
        {
            AICats[i].SetData(InterSceneData.AiCats[i]);
            AICats[i].Difficulty = i;
            _raceCats.Add(AICats[i]);
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
            raceCat.Init();
        }
    }

    public void StickyHoney()
    {
        foreach (RaceCat raceCat in _raceCats)
        {
            if (raceCat.IsPlayerControlled) continue;
            raceCat.CurrentSpeed *= .35f;
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
                if (!cat.FinishedRace && cat.IsPlayerControlled)
                {
                    InterSceneData.PlayerRacePlacement = _aiCatsThatHaveFinishedTheRace;
                    cat.PlayFinishVoiceLine();
                    StartCoroutine(FadeOutDrawingCanvas());
                }
                else if (!cat.FinishedRace && !cat.IsPlayerControlled)
                {
                    _aiCatsThatHaveFinishedTheRace++;
                }

                cat.AfterFinishMoveForward();

                bool allCatsHaveFinished = true;
                foreach (RaceCat checkCat in _raceCats)
                {
                    allCatsHaveFinished &= checkCat.FinishedRace;
                }

                if (allCatsHaveFinished && !IsRaceFinished)
                {
                    IsRaceFinished = true;
                    StartCoroutine(ShowPlaceText());
                }
            }
            else
            {
                cat.LastPoint = cat.NextPoint;
                cat.NextPoint = GetNextRacePoint(cat.LastPoint);
            }
        }

        float distanceMult = cat.ChungusMultiplier;
        if (cat.IsPlayerControlled)
        {
            distanceMult *= InterSceneData.PlayerCat.Ability == AbilityType.SizeAndSpeedBoost ?
                Mathf.Pow(1.15f, InterSceneData.PlayerCat.Level) :
                1f;
        }
        else if (PlayerCat.FinishedRace)
        {
            _aiCatSpeedMult += Time.deltaTime;
            _aiCatSpeedMult = Mathf.Clamp(_aiCatSpeedMult, 0f, 2f);
            distanceMult *= _aiCatSpeedMult;
        }

        cat.Position = Vector3.MoveTowards(
                cat.Position,
                cat.NextPoint.GetPosition(cat.Index),
                distance * distanceMult);
    }

    private IEnumerator FadeOutDrawingCanvas()
    {
        _drawingCanvasGroup.blocksRaycasts = false;
        _drawingCanvasGroup.interactable = false;

        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * 3f;
            _drawingCanvasGroup.alpha = 1 - alpha;

            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator ShowPlaceText()
    {
        _placeText.text = GetPlacementText();
        _placeText.gameObject.SetActive(true);

        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * 3f;
            _placeTextCanvasGroup.alpha = alpha;

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1.5f);

        InterSceneData.Pulls += 4 - InterSceneData.PlayerRacePlacement;
        SceneManager.LoadScene("MainMenuScene");
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

    private string GetPlacementText()
    {
        switch (InterSceneData.PlayerRacePlacement)
        {
            case 0:
                return "1st Place!";
            case 1:
                return "2nd Place";
            case 2:
                return "3rd Place";
            default:
                return "Better Luck Next Time :(";
        }
    }
}
