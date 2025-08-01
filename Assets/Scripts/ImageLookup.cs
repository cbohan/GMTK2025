using UnityEngine;

[CreateAssetMenu(fileName = "ImageLookup", menuName = "Scriptable Objects/ImageLookup")]
public class ImageLookup : ScriptableObject
{
    [Header("Race Textures")]
    public Texture HoneyLoopsRace;
    public Texture TEMP_ChonkerBoiRace;
    public Texture TEMP_DefinitelyACatRace;
    public Texture TEMP_GreenFrootLoopRace;

    [Header("Non Race Textures")]
    public Texture HoneyLoops;
    public Texture TEMP_ChonkerBoi;
    public Texture TEMP_DefinitelyACat;
    public Texture TEMP_GreenFrootLoop;

    public Texture GetRaceTexture(CatImage image)
    {
        switch (image)
        {
            case CatImage.HoneyLoops:
                return HoneyLoopsRace;
            case CatImage.TEMP_ChonkerBoi:
                return TEMP_ChonkerBoiRace;
            case CatImage.TEMP_DefinitelyACat:
                return TEMP_DefinitelyACatRace;
            case CatImage.TEMP_GreenFrootLoop:
            default:
                return TEMP_GreenFrootLoopRace;
        }
    }

    public Texture GetNonRaceTexture(CatImage image)
    {
        switch (image)
        {
            case CatImage.HoneyLoops:
                return HoneyLoops;
            case CatImage.TEMP_ChonkerBoi:
                return TEMP_ChonkerBoi;
            case CatImage.TEMP_DefinitelyACat:
                return TEMP_DefinitelyACat;
            case CatImage.TEMP_GreenFrootLoop:
            default:
                return TEMP_GreenFrootLoop;
        }
    }
}
