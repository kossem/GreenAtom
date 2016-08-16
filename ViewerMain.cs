using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class ViewerMain : MonoBehaviour
{
    private Button info, back, close;
    private int width = Screen.width;
    private readonly float width33 = Screen.width / 3;
    private Vector2 scrollPosition = Vector2.zero;
    private Vector2 scrollPosition2 = Vector2.zero;
    private int height = Screen.height;
    private string name = "equipmentExample.xml";
    Dictionary<string, Queue<Element>> dic = new Dictionary<string, Queue<Element>>();
    
    XmlParse.Menu set = new XmlParse.Menu("Assets\\xml\\menu variant2.xml");
    void Start()
    {
        dic.Add("equipmentExample.xml", LayoutPars.Xml("Assets\\xml\\equipmentExample.xml"));
        ViewerContent.init();
        initButtons();
    }

    private void drawLeftPanel(){
        GUILayout.BeginArea(new Rect(10f, height / 10, width33, height - height / 9), GUI.skin.box);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false);
        GUILayout.BeginVertical(GUI.skin.box);
        foreach (XmlParse.Category cat in set.category)
        {
            GUILayout.TextArea(cat.title + ":" + cat.subtitle + ":" + cat.icon);
            foreach (XmlParse.Action act in cat.ActionList)
            {
                if (GUILayout.Toggle(false, act.title, GUI.skin.button, GUILayout.ExpandWidth(true)))
                    if (act.type == "Specification")
                    {
                        name = act.screenUrl;
                        if (!dic.ContainsKey(act.screenUrl))
                            dic.Add(name, LayoutPars.Xml("Assets\\xml\\" + name));
                    }
            }
        }
        GUILayout.EndVertical();
        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    private void drawRightPanel(){
        GUILayout.BeginArea(new Rect(width33 + 20, height/10, width - width33 - 30, height - height / 9), GUI.skin.box);
        scrollPosition2 = GUILayout.BeginScrollView(scrollPosition2, false, false);
        GUILayout.BeginVertical(GUI.skin.box);
        foreach (Element elem in dic[name])
        {
            if (elem.type == "text")
                GUILayout.Label(elem.txt);
            else
                GUILayout.Button(Resources.Load<Texture>(elem.title));
        }
        GUILayout.EndVertical();
        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    void OnGUI()
    {
        drawLeftPanel();
        drawRightPanel();
    }

    private void initButtons()
    {
        info = GameObject.Find("InfoWhite").GetComponent<Button>();
        info.onClick.AddListener(delegate { SceneManager.LoadScene("aboutCompany_5x4"); });
        back = GameObject.Find("BackWhite").GetComponent<Button>();
        back.onClick.AddListener(delegate { SceneManager.LoadScene("mainWindow_5x4"); });
        close = GameObject.Find("CloseWhite").GetComponent<Button>();
        close.onClick.AddListener(Application.Quit);
    }
}
