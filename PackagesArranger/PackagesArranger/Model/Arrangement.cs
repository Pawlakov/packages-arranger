using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace PackagesArranger.Model
{
	public class Arrangement
	{
		private Region _tree;
		public Container Container { get; }
		public IEnumerable<Placement> Placements { get; private set; }

		public Arrangement(Container container)
		{
			Container = container;
			Placements = new List<Placement>();
			_tree = new Region(new ValueTuple<int, int>(container.Length, container.Width));
		}

		public bool PlaceNextPackage(Package package, bool swapDimensions)
		{
			var placement = new Placement(package);
			if (swapDimensions)
				placement.SwapDimensions();
			var position = _tree.GreedyInsert(package, swapDimensions);
			if (!position.HasValue)
				return false;
			placement.X = position.Value.Item1;
			placement.Y = position.Value.Item2;
			Placements = Placements.Append(placement);
			return true;
		}

		public int Length => Placements.Any() ? (from placement in Placements select placement.X + placement.Length).Max() : 0;

		public static Arrangement ApproximateAlogrithm(Container container, IEnumerable<Package> selectedPackages)
		{
			Arrangement best = null;
			var list = selectedPackages.ToList();
			list.Sort((left, right) => right.Surface - left.Surface);
			var swaps = new BitArray(list.Count);
			while (true)
			{
				var arrangement = new Arrangement(container);
				var fit = true;
				for (var i = 0; i < list.Count; ++i)
				{
					if (!arrangement.PlaceNextPackage(list[i], swaps[i]))
					{
						fit = false;
						break;
					}
				}

				//
				if (fit && (best == null || best.Length > arrangement.Length))
					best = arrangement;
				//
				if (!swaps.Cast<bool>().Contains(false))
					break;
				//
				for (var i = 0; i < 32; i++)
				{
					var previous = swaps[i];
					swaps[i] = !previous;
					if (!previous)
						break;
				}
			}

			return best;
		}
	}
}