using UnityEngine;

public class CatSelectionScreen : MonoBehaviour
{
    [SerializeField] private CatSelection _catSelectionPrefab;

    private void Awake()
    {
        AddCatSelectionButton(
            InterSceneData.Honey, 
            new CatData[] { InterSceneData.Loop, InterSceneData.Oreo, InterSceneData.Apple });

        AddCatSelectionButton(
            InterSceneData.Oreo,
            new CatData[] { InterSceneData.Honey, InterSceneData.Loop, InterSceneData.Apple });

        AddCatSelectionButton(
            InterSceneData.Apple,
            new CatData[] { InterSceneData.Honey, InterSceneData.Loop, InterSceneData.Oreo });

        AddCatSelectionButton(
            InterSceneData.Loop,
            new CatData[] { InterSceneData.Honey, InterSceneData.Oreo, InterSceneData.Apple });
    }

    private void AddCatSelectionButton(CatData catData, CatData[] aiCats)
    {
        CatSelection catSelection = Instantiate(_catSelectionPrefab, transform);
        catSelection.Init(catData, aiCats);
    }
}
