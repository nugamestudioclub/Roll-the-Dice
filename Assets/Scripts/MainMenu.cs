using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public Canvas mainCanvas;
    public Canvas[] destOriginCanvases;
    public Canvas levelSelect;
    [SerializeField]
    private Button levelSelectBtn;
    [SerializeField]
    private Button startbtn;
    [SerializeField]
    private Button nextlevelSelect;
    [SerializeField]
    private Button prevlevelSelect;
    [SerializeField]
    private Button startbtn2;

    [SerializeField]
    private SimpleLevel[] levelsData;
    private int levelCount;

    public GameObject blockPref;

    // Start is called before the first frame update
    void Start()
    {
        startbtn.onClick.AddListener(loadLevelOne);
        startbtn2.onClick.AddListener(loadCustomlevel);
        levelSelectBtn.onClick.AddListener(switchToSelect);
        mainCanvas.gameObject.SetActive(true);
        levelSelect.gameObject.SetActive(false);
        nextlevelSelect.onClick.AddListener(SelRight);
        prevlevelSelect.onClick.AddListener(SelLeft);
    }
    void loadLevelOne()
    {
        Load(1);
    }
    void loadCustomlevel()
    {
        SceneManager.LoadScene("Level" + (levelCount + 1));
    }

    void SelLeft()
    {
        levelCount = (levelCount + 1) % (levelsData.Length);
    }
    void SelRight()
    {
        if (levelCount == 0)
        {
            levelCount = levelsData.Length + 1;
        }
        levelCount = (levelCount - 1) % (levelsData.Length);
    }
    void switchToSelect()
    {
        mainCanvas.gameObject.SetActive(false);
        foreach(Canvas c in destOriginCanvases)
        {
            c.gameObject.SetActive(false);
        }
        levelSelect.gameObject.SetActive(true);
    }
    void Load(int level)
    {
        SceneManager.LoadScene("Level"+level);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public struct SimpleLevel
{
    public string name;
    public Vector2 goal;
    public Vector2[] reds;
    public Vector2[] blocks;
}