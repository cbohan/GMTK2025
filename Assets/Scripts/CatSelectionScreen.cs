using UnityEngine;

public class CatSelectionScreen : MonoBehaviour
{
    [SerializeField] private CatSelection _catSelectionPrefab;

    private void Awake()
    {
        AddCatSelectionButton(
            InterSceneData.HoneyLoops, 
            new CatData[] { InterSceneData.ChonkerBoi, InterSceneData.DefinitelyACat, InterSceneData.GreenFrootLoop });

        AddCatSelectionButton(
            InterSceneData.ChonkerBoi,
            new CatData[] { InterSceneData.HoneyLoops, InterSceneData.DefinitelyACat, InterSceneData.GreenFrootLoop });

        AddCatSelectionButton(
            InterSceneData.DefinitelyACat,
            new CatData[] { InterSceneData.HoneyLoops, InterSceneData.ChonkerBoi, InterSceneData.GreenFrootLoop });

        AddCatSelectionButton(
            InterSceneData.GreenFrootLoop,
            new CatData[] { InterSceneData.HoneyLoops, InterSceneData.ChonkerBoi, InterSceneData.DefinitelyACat });
    }

    private void AddCatSelectionButton(CatData catData, CatData[] aiCats)
    {
        CatSelection catSelection = Instantiate(_catSelectionPrefab, transform);
        catSelection.Init(catData, aiCats);
    }
}
