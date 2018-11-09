ModularTest = require("ModularTest");
TankCmp = require("LuaComponents/APCControllerCmp");
CSVManager = require("Common/CSVManager");


local APC = nil;

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
end

function OnLoadFinish(objs)
	APC = UnityEngine.GameObject.Instantiate(objs[0]);
	APC.transform.position = Vector3.New(0,0,2);

	LuaFramework.Util.Log("Load APC Finish Hp is "..TankCmp.Hp);	
	local apc_ctrl = LuaComponent.Add( APC, TankCmp );
end

--场景切换通知
function OnLevelWasLoaded(level)
	collectgarbage("collect")
	Time.timeSinceLevelLoad = 0
end

function OnApplicationQuit()
end