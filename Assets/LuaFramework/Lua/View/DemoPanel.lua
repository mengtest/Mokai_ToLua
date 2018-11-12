local transform;
local gameObject;

DemoPanel = {}
local this = DemoPanel;

--这里obj是DemoPanel的根节点的gameObject对象
--Awake的分工是，接入gameObject和transform，
--其实init也可以一并做了，分成两个函数无非就是看起来规整一些
function DemoPanel.Awake(obj)
    gameObject = obj;
    transform = obj.transform;

    this.InitPanel();
    log("DemoPanel::Awake"..obj.name);
end

--这里主要是找到各种sub控件的gameObject
--和幻灵的思路是一致的
function DemoPanel.InitPanel()
    local obj_title = transform:Find("PlayerName");
    --注意 :Find 不是.Find 注意: Find返回结果是 transform不是gameObject
    --这些在C#中很容易发现问题，但是在Lua里面，就的记住了:(
    this.obj_setName = transform:Find("ChangeName").gameObject;
    this.lbl_title = obj_title:GetComponent("Text");
end

--暂时没卵用，先不要管他
function DemoPanel.OnDestroy()
    logWarn("DemoPanel::OnDestroy");
end
