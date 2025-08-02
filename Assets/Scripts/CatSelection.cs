using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CatSelection : MonoBehaviour
{
    public CatData Data;
    public CatData[] AiDatas;

    [SerializeField] private RawImage _image;
    [SerializeField] private ImageLookup _imageLookup;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Button _selectButton;
    [SerializeField] private RawImage[] _levels = new RawImage[4];

    public void Init(CatData catData, CatData[] aiCats)
    {
        Data = catData;
        AiDatas = aiCats;

        _image.texture = _imageLookup.GetNonRaceTexture(Data.Image);
        _imageLookup.GetLevelTexture(Data.Image, _levels);
        _name.text = catData.Name;
        _description.text = catData.Description;

        switch (catData.Level)
        {
            case 0:
                _levels[0].color = Color.black;
                goto case 1;
            case 1:
                _levels[1].color = Color.black;
                goto case 2;
            case 2:
                _levels[2].color = Color.black;
                goto case 3;
            case 3:
                _levels[3].color = Color.black;
                goto default;
            default:
                break;                
        }

        if (catData.Level == 0)
        {
            _image.color = Color.black;
            _name.text = "???????";
            _description.text = "???????";
            _selectButton.interactable = false;
        }
    }

    public void Click()
    {
        InterSceneData.PlayerCat = Data;
        InterSceneData.AiCats = AiDatas;

        SceneManager.LoadScene("RacingScene");
    }
}
