using UnityEngine;
using System.Collections;

public class Team : MonoBehaviour {
	
	public int team = -1;

	public void AllyWith(Team other) {
		this.team = other.team;
	}

	public bool HasNoTeam() {
		return team == -1;
	}
	
	public bool HasTeam() {
		return !HasNoTeam();
	}

	public bool IsAlly(int otherTeam) {
		return !HasNoTeam() && this.team == otherTeam;
	}
	
	public bool IsAlly(Team otherTeam) {
		return !HasNoTeam() && this.team == otherTeam.team;
	}

	public bool IsEnemy(Team team) {
		return !IsAlly(team);
	}
}