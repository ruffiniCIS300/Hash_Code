/* Board.cs
 * Author: Nick Ruffini
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.NimBoard
{
    /// <summary>
    /// A Nim board.
    /// </summary>
    public class Board
    {
        /// <summary>
        /// The number of stones on each pile.
        /// </summary>
        private int[] _piles;

        /// <summary>
        /// The limit for each pile.
        /// </summary>
        private int[] _limits;

        /// <summary>
        /// Gets the number of piles.
        /// </summary>
        public int NumberOfPiles => _piles.Length;

        /// <summary>
        /// Int that holds the hashcode for the object
        /// </summary>
        private int _hashCode;

        /// <summary>
        /// Bool that represents whether or not the hashcode has been computed or not
        /// </summary>
        private bool _hasBeenComputed = false;

        /// <summary>
        /// Constructs a new board with the given number of stones and limit
        /// for each pile.
        /// </summary>
        /// <param name="piles">The number of stones on each pile.</param>
        /// <param name="limits">The limit for each pile.</param>
        public Board(int[] piles, int[] limits)
        {
            if (piles.Length != limits.Length)
            {
                throw new ArgumentException();
            }
            _piles = new int[piles.Length];
            piles.CopyTo(_piles, 0);
            _limits = new int[limits.Length];
            limits.CopyTo(_limits, 0);
        }

        /// <summary>
        /// Gets the number of stones on the given pile.
        /// </summary>
        /// <param name="pile">The pile.</param>
        /// <returns>The number of stones on the given pile.</returns>
        public int GetValue(int pile)
        {
            if (pile < 0 || pile >= NumberOfPiles)
            {
                throw new ArgumentException();
            }
            return _piles[pile];
        }

        /// <summary>
        /// Gets the limit for the given pile.
        /// </summary>
        /// <param name="pile">The pile.</param>
        /// <returns>The limit for the given pile.</returns>
        public int GetLimit(int pile)
        {
            if (pile < 0 || pile >= NumberOfPiles)
            {
                throw new ArgumentException();
            }
            return _limits[pile];
        }

        /// <summary>
        /// Compares the two given boards for equality.
        /// </summary>
        /// <param name="x">The first board.</param>
        /// <param name="y">The second board.</param>
        /// <returns>Whether the two boards represent the same game configuration.</returns>
        public static bool operator ==(Board x, Board y)
        {
            if (Equals(x, null))
            {
                return (Equals(y, null));
            }
            else if (Equals(y, null))
            {
                return false;
            }
            else
            {
                if (x.NumberOfPiles != y.NumberOfPiles)
                {
                    return false;
                }
                else
                {
                    for (int i = 0; i < x.NumberOfPiles; i++)
                    {
                        if (x._piles[i] != y._piles[i] || x._limits[i] != y._limits[i])
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
        }

        /// <summary>
        /// Determines whether the given boards are different.
        /// </summary>
        /// <param name="x">The first board.</param>
        /// <param name="y">The second board.</param>
        /// <returns>Whether the given boards represent different configurations.</returns>
        public static bool operator !=(Board x, Board y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Compares this board with the given object for equality.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>Whether obj is a nim board representing the same configuration as this board.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Board)
            {
                return this == (Board)obj;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the result of making the given play from this board position.
        /// </summary>
        /// <param name="p">The play to make.</param>
        /// <returns>The resulting board position.</returns>
        public Board MakePlay(Play p)
        {
            if (p.Pile > _piles.Length || p.Number > _limits[p.Pile])
            {
                throw new ArgumentException();
            }
            Board b = new Board(_piles, _limits);
            b._piles[p.Pile] -= p.Number;
            b._limits[p.Pile] = Math.Min(b._piles[p.Pile], 2 * p.Number);
            return b;
        }

        /// <summary>
        /// Overrides the GetHashCode() method!
        /// </summary>
        /// <returns> Integer value representing the hashcode for the object </returns>
        public override int GetHashCode()
        {
            if (_hasBeenComputed == false)
            {
                _hashCode = 0;
                int mult = 37;

                _hashCode = _hashCode * mult + NumberOfPiles;

                foreach(int stones in _piles)
                {
                    _hashCode = _hashCode * mult + stones;
                }

                foreach (int limit in _limits)
                {
                    _hashCode = _hashCode * mult + limit;
                }

                _hasBeenComputed = true;
                return _hashCode;
            }
            else
            {
                return _hashCode;
            }
        }
    }
}
