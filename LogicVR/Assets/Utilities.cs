using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities {

	public enum NodeIdentifier{
		Input = 0,
		Output = 1,
		Bidirectional = 2,
	}
	
}

public interface Tickable{
	void Tick();
}
