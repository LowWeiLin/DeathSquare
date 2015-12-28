using UnityEngine;
using System.Collections;

public class Team : MonoBehaviour {
	
	public int team = -1;
	
	public bool IsNoTeam() {
		return team == -1;
	}
	
	public bool IsSameTeam(int team) {
		return !IsNoTeam() && this.team == team;
	}
	
	public bool IsSameTeam(Team team) {
		return !IsNoTeam() && this.team == team.team;
	}

	public bool IsEnemyTeam(Team team) {
		return !IsSameTeam(team);
	}
	
}