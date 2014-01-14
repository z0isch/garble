using NUnit.Framework;
using System;

namespace Garble.Core
{
	[TestFixture ()]
	public class GarbleServiceTests
	{
		Random rand;

		[TestFixtureSetUp]
		public void SetUp(){
			rand = new Random ();
		}

		[Test]
		public void DoNothing()
		{
			byte[] test = new byte[10000];
			rand.NextBytes (test);

			Assert.AreEqual (test, GarbleService.DoNothing (test));
		}
	}
}

