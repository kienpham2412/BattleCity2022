using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class GetFiles : MonoBehaviour
{
    private string[] files;
    private List<string> filenames;
    private TMP_Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        filenames = new List<string>();

        files = Directory.GetFiles(Application.dataPath + "/Maps", "*.map", SearchOption.AllDirectories);
        foreach (string aFile in files)
        {
            filenames.Add(Path.GetFileName(aFile));
        }

        dropdown = this.gameObject.GetComponent<TMP_Dropdown>();
        dropdown.AddOptions(filenames);
    }
}
