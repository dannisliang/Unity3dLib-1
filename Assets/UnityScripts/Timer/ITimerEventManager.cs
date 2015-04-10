using UnityEngine;
using System.Collections;

public interface ITimerEventManager {

	void Init();
	void AddTimerEvent(ITimerEvent e);
	void DelTimerEvent(ITimerEvent e);
	void ProcessTimerEvent(float delta);
	void Cleanup();
}
