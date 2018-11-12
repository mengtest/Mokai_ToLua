require "Common/Define"

DemoCtrl = {}
local this = DemoCtrl;

local transfrom;
local gameObject;
local luaBehaviour;

function DemoCtrl.New()
    return this;
end

function DemoCtrl.Awake()
    --这里调用的C#函数，一会去分析C#是怎么创建新的Panel的。
    panelMgr:CreatePanel( 'Demo', this.OnCreate );
end

function DemoCtrl.OnCreate(obj)
    gameObject = obj;
    transfrom = obj.transform;

    luaBehaviour = gameObject:GetComponent("LuaBehaviour");
    log("[lua]DemoCtrl::OnCreate"..gameObject.name);

    luaBehaviour:AddClick( DemoPanel.obj_setName, this.OnClick )
end

function DemoCtrl.OnClick(go)
    log("[Lua]DemoCtrl::OnClick"..DemoPanel.lbl_title.name);
    --這裡注意是lbl_title.text 不是lbl_title.Text。Wrap的API還是記住的好
    DemoPanel.lbl_title.text = "HawkWang";
end