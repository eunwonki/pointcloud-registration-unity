using UnityEngine;
using UnityEditor;
using UnityEditor.Scripting.Python;
using Python.Runtime;
using System;

public class PythonModule
{
    private dynamic np = null;
    private dynamic o3d = null;

    public void Initialize()
    {
        PythonRunner.EnsureInitialized();
        using (Py.GIL())
        {
            try
            {
                o3d = PythonEngine.ImportModule("open3d");
                np = PythonEngine.ImportModule("numpy");
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }
        }
    }

    public void Deinitialize()
    {
        PythonEngine.Shutdown();
    }

    public void Test()
    {
        using (Py.GIL())
        {
            try
            {
                Debug.Log($"{np.sin(5)}");
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }
        }
    }

    public dynamic TriangleMesh(string path)
    {
        dynamic mesh = null;
        using (Py.GIL())
        {
            try
            {
                mesh = o3d.io.read_triangle_mesh(path);
                Debug.Log($"{mesh.vertices}");
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }
        }
        return mesh;
    }

    public dynamic DownSampling(dynamic pcd, double voxelSize)
    {
        using (Py.GIL())
        {
            try
            {
                var pcd_down = pcd.voxel_down_sampling(voxelSize);

                var radius_normal = voxelSize * 2;
                pcd_down.estimate_normals();
                pcd_down.estimate_noramals(o3d.geometry.KDTreeSearchParamHybrid(radius_normal, 30));

                return pcd_down;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }
        }

        return null;
    }
}



