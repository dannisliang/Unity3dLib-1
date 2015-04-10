using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/**
 * 状态机模板，该状体通过消息驱动内部状态改变，内部的状态切换逻辑隐藏在每个状态的Transition函数中.本质上是一个状态切换矩阵.
 * TOwner表示状体机的所属类
 * TStateID表示状态机中每个状态的ID
 * TFSMMessage 驱动状态改变的消息类，可自己随意定义。用户需要实现ConvertFsmMsgToStateID函数，该函数
 * 主要是实现默认的状态切换。
 */ 
public class FiniteStateMachine<TOwner,TStateID,TFSMMessage> : 
								IFiniteStateMachine <TOwner,TStateID,TFSMMessage>  
{
	IFSMState<TOwner,TStateID,TFSMMessage> _currentState; //当前状态.
	Dictionary<TStateID, IFSMState<TOwner,TStateID,TFSMMessage>> _states 
		= new Dictionary<TStateID, IFSMState<TOwner, TStateID, TFSMMessage>>(); //状态ID到状态本身的映射，当前状态机所有状态集合.
	List<TFSMMessage> _msgList = new List<TFSMMessage>();//消息缓存队列.
	#region IFiniteStateMachine implementation

	public virtual void Update ()
	{
		ProcessMsg();
		if(_currentState != null)
			_currentState.Execute();
	}

	public virtual IFSMState<TOwner, TStateID, TFSMMessage> GetState (TStateID id)
	{
		if(_states.ContainsKey(id))
			return _states[id];
		return null;
	}

	public virtual void RegisterState (IFSMState<TOwner, TStateID, TFSMMessage> state)
	{
		if(_states.ContainsKey(state.StateID))
			return ;
		_states.Add(state.StateID, state);
	}

	public virtual void PushFsmMessage (TFSMMessage msg, bool bProcessNow)
	{
		_msgList.Add(msg);
	}

	public virtual void Cleanup ()
	{
		_msgList.Clear();
		_states.Clear();
	}
	//当前状态ID.
	public TStateID CurrentStateID {
		get {
			if(_currentState == null)
				return default(TStateID);
			return _currentState.StateID;
		}
	}
	//当前状态.
	public IFSMState<TOwner, TStateID, TFSMMessage> CurrentState {
		get {
			return _currentState;
		}
	}

	#endregion

	//处理状态机消息，是状态机的核心驱动部分。.
	protected virtual void ProcessMsg()
	{
		if(_msgList.Count == 0)
			return;
		foreach(TFSMMessage msg in  _msgList)
		{
			TStateID nextStateID;
			if(Transition(msg, out nextStateID))
			{
				if(_currentState != null)
					_currentState.Exit();
				_currentState = GetState(nextStateID);
				_currentState.Enter(msg);
				OnChangeStateOK(msg);

			}
			else
			{
				if(_currentState != null)
					_currentState.UpdateState(msg);
				OnChangeStateFail(msg);
			}
		}
		_msgList.Clear();
	}
	//状态切换.
	protected virtual bool Transition(TFSMMessage FSMMsg, out TStateID nextStateID)
	{
		nextStateID = default(TStateID);
		//先尝试能否从当前状态中计算下个状态.
		if( _currentState != null && _currentState.Transiton(FSMMsg, out nextStateID))
		{
			return true;
		}
		//如果第一次尝试失败，则从全局切换逻辑中计算下个状态ID.
		return ConvertFsmMsgToStateID( FSMMsg, out nextStateID);

	}
	//该函数必须由用户实现，以实现根据消息计算下个状态的逻辑..
	protected virtual bool ConvertFsmMsgToStateID(TFSMMessage FSMMsg, out TStateID nextStateID)
	{
		throw new System.NotImplementedException ();
		return false;
	}

	protected virtual void OnChangeStateOK(TFSMMessage msg)
	{
		return ;
	}

	protected virtual void OnChangeStateFail(TFSMMessage msg)
	{
		return ;
	}
}
