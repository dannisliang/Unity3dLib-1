using System.Collections.Generic;
using System;
/*
 * 状态机接口定义.
 */
public interface IFiniteStateMachine <TOwner,TStateID,TFSMMessage>  {

	TStateID CurrentStateID //当前状态ID
	{
		get;
	}
	IFSMState<TOwner,TStateID,TFSMMessage> CurrentState //当前状态
	{
		get;
	}
	//IFiniteStateMachine(TOwner owner);
	void  Update(); //周期性驱动状态机.

	//void ProcessFSMMsg();
	//bool Transition(TFSMMessage FSMMsg, out TStateID nextStateID);//	
	IFSMState<TOwner,TStateID,TFSMMessage> GetState(TStateID id);//根据ID获取状态,如果状态未注册，则返回空
	void RegisterState(IFSMState<TOwner,TStateID,TFSMMessage> state);//注册状态.
	void PushFsmMessage(TFSMMessage msg, bool bProcessNow);//向状态机增加消息,bProcessNow表示是否立即处理该消息.
	void Cleanup();//清理状态机.

};