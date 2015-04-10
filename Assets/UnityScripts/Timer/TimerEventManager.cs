using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/**
 * 定时器管理器，需要每一帧调用ProcessTimerEvent
 */
public class TimerEventManager : ITimerEventManager  {
	List<ITimerEvent> _tempTimerEvents = new List<ITimerEvent>(); //该成员存在的作用是为了防止在执行event过程中，如果又增加定时器，则会产生异常的问题.
	List<ITimerEvent> _timerEvents = new List<ITimerEvent>();
	List<ITimerEvent> _expiredTimerEvents = new List<ITimerEvent>();
	bool bInLoop = false;
	bool DebugMsg
	{
		get{return false;}
	}
	#region ITimerEventManager implementation
	public void Init()
	{
		_tempTimerEvents.Clear();
		_timerEvents.Clear();
		_expiredTimerEvents.Clear();
	}
	public void AddTimerEvent (ITimerEvent e)
	{
		if(DebugMsg)
			Debug.Log("[TimerEventManager][AddTimerEvent]" + e.GetHashCode());
		if(_tempTimerEvents.Contains(e) || _timerEvents.Contains(e))
			return;
		_tempTimerEvents.Add(e);
	}

	public void DelTimerEvent (ITimerEvent e)
	{
		if(DebugMsg)
			Debug.Log("[TimerEventManager][DelTimerEvent]" +  e.GetHashCode());
		if(e == null)
			return ;
		if(_timerEvents.Contains(e) == false && _tempTimerEvents.Contains(e) == false)
			return ;
		_expiredTimerEvents.Add(e);
	}

	public void ProcessTimerEvent (float delta)
	{
		bInLoop = true;
		//清理过期.
		if(_expiredTimerEvents.Count > 0)
		{
			foreach(ITimerEvent e in _expiredTimerEvents)
			{
				if(_timerEvents.Contains(e))
					_timerEvents.Remove(e);
				if(_tempTimerEvents.Contains(e))
					_tempTimerEvents.Remove(e);
			}
			_expiredTimerEvents.Clear();
		}


		//拷贝临时列表.
		if(_tempTimerEvents.Count > 0)
		{
			_timerEvents.AddRange(_tempTimerEvents);
			_tempTimerEvents.Clear();
		}

		//执行定时器.
		if(_timerEvents.Count > 0)
		{
			foreach(ITimerEvent e in _timerEvents)
			{
				e.Consume(delta);
				if(e.Expired() == false)
					continue;
				e.Execute();
				e.AfterExecution(this);

			}
		}
		bInLoop = false;

	}
	public void Cleanup()
	{
		if(DebugMsg)
			Debug.Log("[TimerEventManager][Cleanup]");
		if(bInLoop == true)
		{
			Debug.LogError("[TimerEventManager][Cleanup] Cannot cleanup in loop!!!!!");
			return ;
		}
		_tempTimerEvents.Clear();
		_timerEvents.Clear();
		_expiredTimerEvents.Clear();
	}

	#endregion



}
