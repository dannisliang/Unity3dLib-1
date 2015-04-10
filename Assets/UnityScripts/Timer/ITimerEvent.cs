using UnityEngine;
using System.Collections;

public interface ITimerEvent  {
	void Create(float timer, TimerEventType type, System.Action action);//设置多长时间后超时
	void Consume(float delta); //时间消耗
	bool Expired();//是否超时
	void Execute();//执行任务
	void AfterExecution(ITimerEventManager manager);//任务执行完后续处理
}
