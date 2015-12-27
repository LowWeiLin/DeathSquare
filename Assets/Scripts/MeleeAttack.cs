using UnityEngine;
using System.Collections;

public class MeleeAttack : Attack {

	public override void AttackTarget(Maybe<GameObject> target, Vector3 targetPosition, Maybe<Health> targetHealth) {
		float halfTime = 0.15f;

		model.IfPresent(m => {
			Vector3 basePosition = m.transform.localPosition;

			GoTweenChain chain = new GoTweenChain()
				.append(new GoTween(m.transform, halfTime, new GoTweenConfig()
					.position(targetPosition)
					.setEaseType(GoEaseType.BackIn)))
				.append(new GoTween(m.transform, halfTime, new GoTweenConfig()
					.localPosition(basePosition)
					.setEaseType(GoEaseType.BackOut)));

			chain.setOnCompleteHandler(c => targetHealth.IfPresent(t => t.TakeDamage(damage)));
			chain.play();
		});
	}
}
