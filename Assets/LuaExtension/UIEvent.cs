using UnityEngine;
using System.Collections;
using LuaInterface;
using UnityEngine.UI;

public class UIEvent
{
    public static void AddButtonClick(GameObject go, LuaFunction luafunc)
    {
        if (go == null || luafunc == null)
            return;

        Button btn = go.GetComponent<Button>();
        if (btn == null)
            return;

        Debug.LogWarning(" Call Add Listener ");
        btn.onClick.AddListener(
                delegate () 
                {
                    Debug.LogWarning(" Call Delegate ");
                    luafunc.Call(go);
                }
            );
    }

    public static void ClearButtonClick(GameObject go, LuaFunction luafunc)
    {
        if (go == null)
            return;

        Button btn = go.GetComponent<Button>();
        if (btn == null)
            return;

        btn.onClick.RemoveAllListeners();
    }
}
