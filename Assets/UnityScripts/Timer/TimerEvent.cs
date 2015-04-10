using UnityEngine;
using System.Collections;
public enum TimerEventType
{
	None,
	Once, //定时器只执行一次
	Loop,//定时器循环执行
}
//定时器事件
public class TimerEvent: ITimerEvent{
	float _timer; //定时器时间，多久后超时
	float _consumer;//_timer的替身，用来每次递减时间
	TimerEventType _type;// 定时器类型
	System.Action  _action;//实际

	#region ITimerEvent implementation

	public void Create (float timer, TimerEventType type, System.Action action)
	{
		_timer = timer;
		_consumer = timer;
		_type = type;
		_action = action;
	}

	public void Consume (float delta)
	{
		_consumer -= delta;
	}

	public bool Expired ()
	{
		return _consumer <= 0;
	}

	public void Execute ()
	{
		if(_action != null)
			_action();
	}

	public void AfterExecution (ITimerEventManager manager)
	{
		//循环还是删除
		if(_type == TimerEventType.Once)
			manager.DelTimerEvent(this);
		else if (_type == TimerEventType.Loop)
		{
			_consumer = _timer;
		}
		else
		{
		}
	}

	#endregion




}
