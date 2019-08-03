using UnityEngine;
using System.Collections;

public class RotateSample : MonoBehaviour
{	
	void Start(){
		iTween.RotateBy(gameObject, iTween.Hash("z", .25, "easeType", "easeInOutBack", "delay", .4));
	}
}

