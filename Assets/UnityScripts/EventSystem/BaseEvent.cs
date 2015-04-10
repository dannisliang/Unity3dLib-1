using UnityEngine;
using System.Collections;
namespace Engine.Core
{
	public class BaseEvent {

		EventType _type;

		public EventType type {
			get {
				return _type;
			}
			set {
				_type = value;
			}
		}

		Object _arg;

		public Object arg {
			get {
				return _arg;
			}
			set {
				_arg = value;
			}
		}
	}
}
