using System.IO;
using UnityEngine;

public class PythonTester : MonoBehaviour
{
    PythonModule module = new PythonModule();

    // Start is called before the first frame update
    private void Awake()
    {
        module.Initialize();
        module.Test();
    }

    private void OnApplicationQuit()
    {
        module.Deinitialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Button1()
    {
        var path = Path.Combine(Application.dataPath, "model.obj");
        var mesh = module.TriangleMesh(path);
    }
}
