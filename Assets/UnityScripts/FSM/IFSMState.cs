using System;
public interface IFSMState <TOwner, TStateID, TFSMMessage>   {	
	TStateID StateID{
		get;
	}

	//IFSMState(TFSM fsm);
	void Enter (TFSMMessage msg);//进入状态.	
	void Execute ();//周期性执行状态.
	void UpdateState(TFSMMessage msg);//相同的消息到达时，根据状态机消息，更新内部状态. 
	bool Transiton(TFSMMessage msg, out TStateID stateID);//根据消息计算下个状态.	
	void Exit();//退出状态.
}