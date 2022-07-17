using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    // Start is called before the first frame update
    void Start()
    {
        startbtn.onClick.AddListener(loadLevelOne);
        startbtn2.onClick.AddListener(loadCustomlevel);
        levelSelectBtn.onClick.AddListener(switchToSelect);
        mainCanvas.gameObject.SetActive(true);
        levelSelect.gameObject.SetActive(false);
    }
    void loadLevelOne()
    {
        Load(1);
    }
    void loadCustomlevel()
    {

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
