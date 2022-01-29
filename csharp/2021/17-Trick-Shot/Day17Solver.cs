namespace Year2021
{
	public class Day17Solver
	{
		private Target Target;
		
		public Day17Solver(Target target)
		{
			Target = target;
		}
		
		public long FindHighestProbePositionWhenLaunchedWithStyle()
		{
			// Note that for all of the following scenarios, each step along the way of an upward shot results in the exact same y positions
			// being occupied both times: on its way up and on its way down.
			
			// If the target's height touches the origin (0), then the highest y position trajectory is mathematically infinite or machine maximum.
			// A probe launched at any velocity will touch its exact origin height on its return. Thus, an infinite vertical velocity that reaches
			// an infinite height will result in it touching the target after it returns.
			if ((Target.Y1 >= 0 && Target.Y2 <= 0) || (Target.Y1 <= 0 && Target.Y2 >= 0))
			{
				return long.MaxValue;
			}
			
			// If the target's height exists entirely below the origin (0), then the highest y position trajectory will happen in the case of
			// the maximum possible downward y velocity of the probe at origin after its return from on high. That maximum velocity is
			// completely based upon it touching the target and not missing the target after one single step after its return to origin.
			// The downward velocity of the probe at the moment of its return is equal in magnitude to the upward velocity of the probe when it
			// is initially fired. That downward velocity + 1 hitting the very bottom of the target in the following step is the maximum
			// velocity scenario. Thus, the initial velocity that produces the maximum height is the distance from origin to the bottom of the
			// target - 1.
			if (Target.Y1 < 0 && Target.Y2 < 0)
			{
				var velocity = Math.Abs(Target.Y1) - 1;
				return (velocity * (velocity + 1)) / 2; // sum the velocities that uniformly decrement by 1 to 0 to get the max height
			}
			
			// If the target's height exists entirely above the origin (0), then the highest y position trajectory will happen in the case of
			// the probe touching a height at the top of the target immediately after it is fired at step one. Traversing to the highest point
			// at the top of the target in the first step is the highest initial velocity that later touches the target on its way back down
			// without skipping the target. This highest initial velocity will produce the highest y position.
			if (Target.Y1 > 0 && Target.Y2 > 0)
			{
				return (Target.Y2 * (Target.Y2 + 1)) / 2; // sum the velocities that uniformly decrement by 1 to 0 to get the max height
			}
			
			throw new Exception("Logic is broken.");
		}
		
		public IEnumerable<(long, long)> FindInitialVelocitiesThatHitTarget()
		{
			List<(long, long)> initialVelocities = new List<(long, long)>();
			
			// The range of xvel velocities to loop over is found the following way:
			// The x distance from origin of the most distant x edge of the target makes for a good loop x-boundary. Any larger than that will
			// never touch the target, so there's no need to bother looping over velocities greater than that in the x direction. If the target
			// spans the x origin, then the min and max xvel loop boundaries will be both target edges. If the target exists entirely to the
			// left of the x origin, then we want to iterate over velocities from the left most edge of the target all the way to the origin
			// zero. If the target exists entirely to the right of the x origin, then we want to iterate over velocities from the origin all
			// the way to the right most edge of the target to find the velocities that hit the target.
			var xvel1 = Math.Min(0, Target.X1);
			var xvel2 = Math.Max(0, Target.X2);
			
			// The range of yvel velocities to loop over is found the following way:
			// The largest absolute value of the y position edge of the target, be it positive or negative. As established in part 1, if
			// the initial y velocity is greater in magnitude than the distance of the y edge of the target from the origin, then it will
			// overshoot and never hit the target. Thus, +/- the distance of the furthest edge of the target makes for a good loop y-boundary.
			var yvel2 = Math.Max(Math.Abs(Target.Y1), Math.Abs(Target.Y2));
			var yvel1 = -yvel2;
			
			// TODO: Simplify with "yield return" and Enumerable.Range()
			for (var yvel = yvel1; yvel <= yvel2; yvel++)
			{
				for (var xvel = xvel1; xvel <= xvel2; xvel++)
				{
					if (Target.VelocityHitsTarget(xvel, yvel))
					{
						initialVelocities.Add((xvel, yvel));
					}
				}
			}
			
			return initialVelocities;
		}
	}
}