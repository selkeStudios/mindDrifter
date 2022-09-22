using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBehaviour : MonoBehaviour
{
    public KeyCode pause = KeyCode.Escape;
    public bool toggleSprint = false;
    public bool toggleCrouch = false;

    public List<string> keyNames = new List<string>() { "Forward", "Backward", "Left", "Right", "Sprint", "Crouch", "Jump", "Interact",
                                                        "Pause", "PickUp", "Create", "ShapeSwap", "ScaleUp", "ScaleDown"};
    public List<KeyCode> keys = new List<KeyCode>() { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.LeftControl, KeyCode.LeftShift, 
                                                      KeyCode.Space, KeyCode.Q, KeyCode.Escape, KeyCode.V, KeyCode.C, KeyCode.B, 
                                                      KeyCode.RightBracket, KeyCode.LeftBracket};
    public List<KeyCode> defControls;

    public List<Text> labels;

    public string sens = "Sensitivity";
    public Slider sensitivitySlider;
    public Text sensObj;
    public float sensitivity;

    public GameObject[] setKeys;

    public GameObject[] menus;

    public Event keyEvent;

    public int menu = -1;

    public bool settingKeys = false;

    public bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        defControls = keys;
        KeyCheck();
        SensitivityChange();
    }

    // Update is called once per frame
    void Update()
    {
        if (!settingKeys)
        {
            if (Input.GetKeyDown(pause))
            {
                Back();
            }
        }
    }

    void KeyCheck()
    {
        for (int i = 0; i < keyNames.Count; i++)
        {
            int key = PlayerPrefs.GetInt(keyNames[i], -1);

            if (key != -1)
            {
                keys[i] = (KeyCode)key;
                labels[i].text = keys[i].ToString();
            }
        }

        float se = PlayerPrefs.GetFloat(sens);

        if (se > 0)
        {
            sensitivity = se;
            sensObj.text = sensitivity.ToString();
        }
    }

    public void SetKeyStart(GameObject obj)
    {
        StartCoroutine(SetKey(obj.name, obj.transform.Find("Key").gameObject.GetComponent<Text>()));
    }

    private void OnGUI()
    {
        keyEvent = Event.current;
    }

    IEnumerator SetKey(string keytype, Text txt)
    {

        int mouse = 0;
        bool mouseUp = false;

        while (mouse < 2 && keyEvent.keyCode == KeyCode.None)
        {

            if (keyEvent.type == EventType.MouseDown || keyEvent.type == EventType.MouseDrag)
            {

                if (mouseUp)
                {
                    mouse++;
                }
                else if (mouse == 1)
                {
                    mouse = 1;
                }
                else
                {
                    mouse = 0;
                }

                mouseUp = false;

            }


            else if (keyEvent.type == EventType.MouseUp)
            {
                mouse = 1;
                mouseUp = true;

            }

            yield return null;

        }

        int index = keyNames.IndexOf(keytype);

        if (keyEvent.keyCode != KeyCode.None && !keyEvent.isMouse)
        {
            keys[index] = keyEvent.keyCode;
            txt.text = keys[index].ToString();
        }
        else
        {
            keys[index] = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Mouse" + keyEvent.button);
            txt.text = keys[index].ToString();
        }
    }

    public void SaveControls()
    {
        for (int i = 0; i < keyNames.Count; i++)
        {
            PlayerPrefs.SetInt(keyNames[i], (int)keys[i]);
            PlayerPrefs.SetFloat(sens, sensitivity);
        }
    }

    public void Pause()
    {
        if (menu <= 0)
        {
            paused = true;
            menus[0].SetActive(true);
            Cursor.lockState = CursorLockMode.None;

            menu = 0;
        }
        else if (menu == 0)
        {
            paused = false;
            menus[0].SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;

            menu = -1;
        }

        if (paused == false)
        {
            for (int x = 1; x < menus.Length; x++)
            {
                menus[x].SetActive(false);
            }
        }
    }

    public void MenuDown(GameObject obj)
    {

    }

    public void MenuUp()
    {

    }

    public void Back()
    {
        switch (menu)
        {
            case 0:
            case 1:
                menus[1].SetActive(false);
                Pause();
                break;

            case 2:
            case 3:
                menus[2].SetActive(false);
                
                break;

            default:
                Pause();
                break;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SensitivityChange()
    {
        sensitivity = sensitivitySlider.value;
        sensObj.text = sensitivity.ToString();
    }
}
