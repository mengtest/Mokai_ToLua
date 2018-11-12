ModularTest = require("ModularTest");
TankCmp = require("LuaComponents/APCControllerCmp");

local APC = nil;
local panel_demo = nil;

--主入口函数。从这里开始lua逻辑
function Main()					
	print("logic start")	 		
	ModularTest.Test();
	
	
	GameUtil.SayHello();

	local intArray = {1,2,3,4,5,6,7,8,9,10};
	local sum_of_table = GameUtil.TestFloat( intArray );
	print("Sum form 1 to 10 is "..sum_of_table);

	LuaHelper = LuaFramework.LuaHelper;
	resMgr = LuaHelper.GetResManager();
	resMgr:LoadPrefab('vehicles',{'apc_01_a'},OnLoadFinish);

	resMgr:LoadPrefab('lobbyUI',{'PanelDemo'},OnLoadUIFinish);	
end

function OnLoadFinish(objs)
	APC = UnityEngine.GameObject.Instantiate(objs[0]);
	APC.transform.position = Vector3.New(0,0,2);

	LuaFramework.Util.Log("Load APC Finish Hp is "..TankCmp.Hp);	
	local apc_ctrl = LuaComponent.Add( APC, TankCmp );
end

--[[
function  OnLoadUIFinish(objs)
	panel_demo = UnityEngine.GameObject.Instantiate( objs[0] );
	local parent = UnityEngine.GameObject.Find("Canvas");
	panel_demo.transform:SetParent(parent.transform);
	panel_demo.transform.localScale = Vector3.one;
	panel_demo.transform.localPosition = Vector3.zero;

	local btn = panel_demo.transform:Find("ChangeName").gameObject
	print("btn is "..btn.name);
	UIEvent.AddButtonClick( btn, OnClick );
end
]]

function OnClick()
	print("触发按钮事件");
end


--场景切换通知
function OnLevelWasLoaded(level)
	collectgarbage("collect")
	Time.timeSinceLevelLoad = 0
end

function OnApplicationQuit()
end