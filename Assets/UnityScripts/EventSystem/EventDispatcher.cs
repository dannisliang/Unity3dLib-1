using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EventType=Engine.Core.EventType;
using EventDispatcher=Engine.Core.EventDispatcher;
namespace Engine.Core
{
	public delegate void EventListener(BaseEvent e);
	public class EventDispatcher
	{
		EventListener []  _eventListeners = new EventListener[(int)EventType.MaxCount];
		static EventDispatcher instance;

		public static EventDispatcher Instance {
			get {
				if(instance == null)
					instance = new EventDispatcher();
				return instance;
			}
		}

		public void DispatchEvent(BaseEvent e)
		{
			EventListener listener = _eventListeners[(int)e.type];
			if(listener == null)
				return;
			listener(e);
		}
		public void AddEventListener(EventType type, EventListener listener)
		{
			//Debug.LogError ("-------------------------------------AddEventListener " + type + " " + listener + " "+ _eventListeners[(int)type]);

			if(_eventListeners[(int)type] == null)
				_eventListeners[(int)type]  = listener;
			else
				_eventListeners[(int)type] += listener;
		}
		public void RemoveEventListener(EventType type, EventListener listener)
		{
			if(_eventListeners[(int)type] == null)
				return;
			_eventListeners[(int)type] -= listener;
		}
		/*
		Dictionary<EventType, EventListener> _eventListenerList = new Dictionary<EventType, EventListener>();
		public void DispatchEvent(BaseEvent e)
		{
			if(_eventListenerList.ContainsKey(e.type) == false)
				return;
			EventListener listener = _eventListenerList[e.type];
			listener(e);

		}
		public void AddEventListener(EventType type, EventListener listener)
		{
			if(_eventListenerList.ContainsKey(type) == true)
			{
				_eventListenerList[type] += listener;
			}
			else
			{
				_eventListenerList.Add(type, listener);
			}
		}

		public void RemoveEventListener(EventType type, EventListener listener)
		{
			if(_eventListenerList.ContainsKey(type) == true)
			{
				_eventListenerList[type] -= listener;
			}

		}*/
	}
	//TODO HOW TO fix this?
	/*
	class  Base
	{}
	class derive<T> : Base where T: BaseEvent
	{
		System.Action<T> del;
	}
	class C
	{
		void test<T>() where T: BaseEvent
		{
		}
	}
	*/
}


