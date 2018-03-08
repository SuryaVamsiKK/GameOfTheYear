using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Database", menuName = "Parts Database")]
public class PartsDatabaseHolder : ScriptableObject {

	[HideInInspector]
	public int currentTab;

	public List<Parts> Part = new List<Parts>();
	//add this now !! this is where you left AND clean all the codes they are untidey, change the character build from int to part names or IDs.

}
