using NUnit.Framework;

namespace Year2021
{
	[TestFixture]
	public class Day17_Tests
	{
		[Test]
		public void Part1HighestPositionIfTargetHeightTouchesOrigin()
		{
			var target = new Target("target area: x=265..287, y=103..-58");
			var solver = new Day17Solver(target);
			var highestProbePosition = solver.FindHighestProbePositionWhenLaunchedWithStyle();
			Assert.AreEqual(long.MaxValue, highestProbePosition);
			
			target = new Target("target area: x=265..287, y=-103..58");
			solver = new Day17Solver(target);
			highestProbePosition = solver.FindHighestProbePositionWhenLaunchedWithStyle();
			Assert.AreEqual(long.MaxValue, highestProbePosition);
		}
		
		[Test]
		public void Part1HighestPositionIfTargetHeightIsBelowOrigin()
		{
			var target = new Target("target area: x=20..30, y=-10..-5");
			var solver = new Day17Solver(target);
			var highestProbePosition = solver.FindHighestProbePositionWhenLaunchedWithStyle();
			Assert.AreEqual(45, highestProbePosition);
		}
		
		[Test]
		public void Part1HighestPositionIfTargetHeightIsAboveOrigin()
		{
			var target = new Target("target area: x=20..30, y=10..5");
			var solver = new Day17Solver(target);
			var highestProbePosition = solver.FindHighestProbePositionWhenLaunchedWithStyle();
			Assert.AreEqual(55, highestProbePosition);
		}
		
		[Test]
		public void Part2NumberOfVelocitiesThatHitTarget()
		{
			var target = new Target("target area: x=20..30, y=-10..-5");
			var solver = new Day17Solver(target);
			var initialVelocities = solver.FindInitialVelocitiesThatHitTarget();
			Assert.AreEqual(112, initialVelocities.Count());
		}
	}
}