using UnityEngine;
using System.Collections;

public class SceneConst
{
    public const string scene_Login = "login";
    public const string scene_myCity = "myHome";
    public const string scene_map01 = "map01";
    public const string scene_map02 = "level01";
    public const string scene_pvpScene = "pvpScene";
}

public class UINameConst{
    public const string ui_login = "UI/login/ui_login";
    public const string ui_selectHome = "UI/common/selectHome";
    public const string ui_homeview = "UI/common/homeView";
    public const string ui_uititle = "UI/common/ui_title";
   
    
    public const string ui_BattlerView = "UI/profab/battleUI/ui_battle";
    public const string ui_fightstop = "UI/profab/battleUI/ui_fightstop";
    public const string ui_fightWin = "UI/profab/battleUI/ui_fightWin";
    public const string ui_battle = "UI/profab/battleUI/ui_battle";
    public const string ui_battleData = "UI/profab/battleUI/ui_battleData";
    public const string ui_pvp = "UI/profab/ui_pvp";
    public const string ui_Loading = "UI/common/ui_loadingView";
    public const string ui_Center = "UI/common/ControlCenterview";

    public const string ui_heroview = "UI/hero/ui_heroview";
    public const string ui_radarView = "UI/hero/ui_radarView";
    public const string ui_heroinfo = "UI/hero/ui_heroinfo"; 
    
}

public class ParticalConst
{
public const string  FireRain = "particleEffect/P_fire_rain/P_fire_rain";
}

public class AnimatorConst
{
    public static readonly string IdleState = "dapaoidle";
    public static readonly string AtkState = "attack";
}