using UnityEngine;
using LuaInterface;
using LuaFramework;

public class LuaComponent : MonoBehaviour {

    //lua表，其实就是对应一个Lua脚本文件...吧。
    public LuaTable table;

    public static LuaTable Add(GameObject go, LuaTable tableClass)
    {
        LuaFunction fun = tableClass.GetLuaFunction("New");
        if (fun == null)
            return null;

        //object[] rets = fun.Call(tableClass);
        LuaTable temp_table = fun.Invoke<LuaTable, LuaTable>(tableClass);
        if (temp_table != null)
        {
            LuaComponent cmp = go.AddComponent<LuaComponent>();
            cmp.table = temp_table;
            cmp.CallAwake();
            return cmp.table;
        }
        return null;
    }

    public static LuaTable Get(GameObject go, LuaTable table)
    {
        LuaComponent[] cmps = go.GetComponents<LuaComponent>();
        foreach (LuaComponent cmp in cmps)
        {
            string mat1 = table.ToString();
            string mat2 = cmp.table.GetMetaTable().ToString();
            if (mat1 == mat2)
            {
                return cmp.table;
            }
        }
        return null;
    }

    void CallAwake()
    {
        LuaFunction fun = table.GetLuaFunction("Awake");
        if (fun != null)
            fun.Call(table, this.gameObject);
    }
}
