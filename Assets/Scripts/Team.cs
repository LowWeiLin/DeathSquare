using UnityEngine;
using System.Collections;

public class Team : MonoBehaviour {

	public int team;
	
	// Use this for initialization
	void Start () {
		team = -1;
	}

	public bool IsNoTeam() {
		return team == -1;
	}

	public bool IsSameTeam(int team) {
		return !IsNoTeam() && this.team == team;
	}

	public bool IsSameTeam(Team team) {
		return !IsNoTeam() && this.team == team.team;
	}

}