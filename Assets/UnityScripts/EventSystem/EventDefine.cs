using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//核心事件定义放在此文件
namespace Engine.Core
{
	public enum EventType  {

		None,
		//ResourceEvent
		OnPatchingFinish,//补丁完成.
		OnResourceReady,//资源就绪.

		//Character Event
		OnCharacterCreated, //角色创建
		OnCharacterDie,//角色死亡
		OnCharacterRevive,//角色重生
		OnCharacterUseSkill,//角色使用技能
		OnCharacterAttackCombo,//角色连击
		OnCharacterLevelUp, //角色升级.
		//Monster Event
		OnMonsterCreated, //怪物创建
		OnMonsterDie,//怪物死亡
		OnMonsterDieing,//怪物即将死亡
		OnMonsterUseSkill, //怪物使用技能
		//StageEvent
		OnEnterStage,//进入副本事件
		OnStageReward,//通关奖励事件
		OnExitStage,//退出副本

		//Town主城事件.
		OnEnterTown,
		OnExitTown,

		//Mission.
		OnAcceptTask, //接受任务事件.
		OnCommitTask, //提交任务事件.
        OnCanAcceptTask,//可接受任务事件.
        OnCanCommitTask,//可提交任务事件.
        OnCanExecutionTask,//可执行任务事件.

		//TODO @everyone  Add event here

		OnObjectHit,//受击事件

		OnFightBlackScreen,	//触发战斗黑屏特写事件.

		//背包事件.
		OnBagSort,				//触发背包排序事件.
		OnBagEquipChange,		//背包装备变动事件.
		OnBagWearableEvent,		//背包装备变动事件.

		//镶嵌系统事件.
		OnChangeEquipSuccess,	//成功穿上装备.
		OnInlayRubySuccess,		//成功镶嵌宝石.
		OnComposeSuccess,		//成功合成(升级)宝石.

		//技能系统事件.
		OnRefreshSkilllUiCell,		//刷新技能界面上的某个UI键位.			
		OnUpdateSkillProficiency,	//更新技能熟练度.
		OnUpdateSkillLv,			//技能进阶(升级).

		//宠物系统.
		OnPetEquipSuccess,      //宠物装备穿戴成功.

		//战斗UI。.
		OnPlayerSkillBegin, //主角技能开始生效. 如果不是最后技能，则UI变成下个图标，并变灰，否则倒计时.
		OnPlayerNextSkillAvail, //可以输入下个技能，此时图标由灰变亮.
		OnPlayerSkillFinish,//当前技能播放完成.

		//Camera shake.
		OnCameraShake, //相机震动事件.

		//GameProcess.
		OnNetworkEvent, //网络事件.

	
		//世界BOSS系统事件.	
		OnWorldBossCountDownBeginTime,		//挑战开始倒计时更新事件.
		OnWorldBossCountDownEndTime,		//挑战结束倒计时更新事件.
		OnWorldBossPlayerDie,				//玩家死亡退出战斗场景.
		OnWorldBossMonsterDie,				//世界BOSS死亡事件.
		OnWorldBossNextStage,				//进入下一阶段事件.
		OnWorldBossRefreshChartsUI,			//刷新UI排行榜事件.
		OnWorldBossRefreshBossHpUI,			//刷新UIBOSS血量事件.
		OnWorldBossFinish,					//世界BOSS任务完成事件.

		OnFrameTime,//时间轴专用

		//物品相关.
		OnPickUpItem, //获取物品.
		OnConsumeItem,//消耗物品.

		//装备事件.
		OnEquipUpgrade, //装备升级.

		//NPC对白.
		OnNpcTalkTask, //接受npc对话任务事件.
		OnTalkFinish, //对白完成事件.

		//脚本执行事件.
		OnProcedureStart, //脚本启动事件.
		OnProcedureFinish,//脚本结束事件.

		//战斗波数变化
		OnFightMNumChange,


        OnScaleTimeStart,//定帧开始.
        CurGroupMosterAllDie,//当前排次怪物已死光.


		//Boss事件.
		OnDungeonBossBorn, //Boss出现.
		OnDungeonBossDestroy, //Boss死亡.

		//SystemUnlock事件.
		OnSystemUnlock, //发生系统解锁时.


		//Max Event number
		MaxCount

      

	}


	//打补丁完成.
	public class PatchFinishEvent : BaseEvent
	{
		public Dictionary<string, string> resouceList = null;
		public PatchFinishEvent()
		{
			type = EventType.OnPatchingFinish;
		}
	}

	public class ResourceReadyEvent : BaseEvent
	{
		public ResourceReadyEvent()
		{
			type = EventType.OnResourceReady;
		}
	}


	//角色创建
	public class CharacterCreatedEvent : BaseEvent
	{
		public CharacterCreatedEvent()
		{
			type = EventType.OnCharacterCreated;
		}
	}

	//TODO @zjm Add more events here

}