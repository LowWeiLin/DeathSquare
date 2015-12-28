using UnityEngine;
using System.Collections;

public class Team : MonoBehaviour {
	
	public Maybe<int> team;

	public void AllyWith(Team other) {
		this.team = other.team;
	}

	public void UnallyWith(Team other) {
		this.team = Maybe<int>.Empty;
	}

	public bool HasNoTeam() {
		return team.NotPresent;
	}
	
	public bool HasTeam() {
		return !HasNoTeam();
	}

	public bool IsAlly(int otherTeam) {
		return !HasNoTeam() && this.team.IsPresent && this.team.Value.Equals(otherTeam);
	}
	
	public bool IsAlly(Team otherTeam) {
		return !HasNoTeam() && this.team.Equals(otherTeam.team);
	}

	public bool IsEnemy(Team team) {
		return !IsAlly(team);
	}
}