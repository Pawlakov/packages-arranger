using System;
using System.Collections.Generic;
using System.Linq;

namespace PackagesArranger.Model
{
	public class Region
	{
		private (int, int) _origin;
		private (int, int) _size;
		private Package _content;
		private Region _top;
		private Region _left;
		private Region _right;
		private Region _bottom;
		private (int, int) _potential;

		public Region((int, int) size)
		{
			_potential = _size = size;
		}

		public ValueTuple<int, int>? GreedyInsert(Package package, bool swapDimensions)
		{
			var packageSize = (
			swapDimensions ? package.SecondDimension : package.FirstDimension,
			swapDimensions ? package.FirstDimension : package.SecondDimension
			);
			//
			var target = SearchFit(packageSize);
			if (target == null)
				return null;
			target.Occupy(package, packageSize, true);
			CalculatePotential();
			return target._origin;
		}

		private void Occupy(Package package, (int, int) packageSize, bool isRoot)
		{
			if (_size.Item1 > packageSize.Item1)
			{
				var top = this;
				while (top._top != null)
					top = top._top;
				top.SplitVertically(packageSize.Item1);
			}

			if (_size.Item2 > packageSize.Item2)
			{
				var left = this;
				while (left._left != null)
					left = left._left;
				left.SplitHorizontally(packageSize.Item2);
			}

			if (_size.Item2 < packageSize.Item2)
				_bottom.Occupy(package, (_size.Item1, packageSize.Item2 - _size.Item2), false);
			if (_size.Item1 < packageSize.Item1)
				_right.Occupy(package, (packageSize.Item1 - _size.Item1, packageSize.Item2), false);
			_content = package;
		}

		private void CalculatePotential()
		{
			if (_top == null)
				_right?.CalculatePotential();
			_bottom?.CalculatePotential();
			_potential = _content != null ? (0, 0) : ((_right?._potential.Item1 ?? 0) + _size.Item1, (_bottom?._potential.Item2 ?? 0) + _size.Item2);
		}

		private Region SplitVertically(int where)
		{
			if (where >= _size.Item1)
				throw new InvalidOperationException("Can't cut there.");
			var newRegion = new Region((_size.Item1 - where, _size.Item2))
			{
			_origin = (_origin.Item1 + where, _origin.Item2),
			_content = _content,
			_top = _top?._right,
			_left = this,
			_right = _right
			};
			_size.Item1 = where;
			if (_right != null)
				_right._left = newRegion;
			_right = newRegion;
			newRegion._bottom = _bottom?.SplitVertically(where);
			return newRegion;
		}

		private Region SplitHorizontally(int where)
		{
			if (where >= _size.Item2)
				throw new InvalidOperationException("Can't cut there.");
			var newRegion = new Region((_size.Item1, _size.Item2 - where))
			{
			_origin = (_origin.Item1, _origin.Item2 + where),
			_content = _content,
			_top = this,
			_left = _left?._bottom,
			_bottom = _bottom
			};
			_size.Item2 = where;
			if (_bottom != null)
				_bottom._top = newRegion;
			_bottom = newRegion;
			newRegion._right = _right?.SplitHorizontally(where);
			return newRegion;
		}

		private Region SearchFit((int, int) minimum)
		{
			Region vertical;
			if (_potential.Item1 >= minimum.Item1 && _potential.Item2 >= minimum.Item2)
				vertical = this;
			else
				vertical = _bottom?.SearchFit(minimum);
			if (_top == null && vertical == null)
				return _right?.SearchFit(minimum);
			return vertical;
		}
	}
}