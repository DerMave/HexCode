using HexCode.Common.Exceptions;
using System;
using System.Runtime.Serialization;

namespace HexCode.Common
{
    [Serializable]
    public class Location : ISerializable
    {
        public int XPos { get; set; }
        public int YPos { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                return (XPos == ((Location)obj).XPos && YPos == ((Location)obj).YPos);
            }
        }

        public override int GetHashCode()
        {
            return Tuple.Create(XPos, YPos).GetHashCode();
        }

        public static bool IsXYValid(int x, int y)
        {
            return (x + y) % 2 == 0;
        }
        public Location() // for serialization
        {
        }
        protected Location(SerializationInfo info, StreamingContext context) // for serialization
        {
            XPos = info.GetInt32("XPos");
            YPos = info.GetInt32("YPos");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("XPos", XPos);
            info.AddValue("YPos", YPos);
        }
        public Location(int x, int y)
        {
            if (!IsXYValid(x, y))
            {
                throw new DoLaterException();
            }

            XPos = x;
            YPos = y;
        }

        public int Distance(Location otherLoc)
        {
            // Y / 2 abgerundet (floor) + X inkorrekte version:
            //int dist = (int)(Math.Abs(XPos - otherLoc.XPos) + Math.Floor(Math.Abs(YPos - otherLoc.YPos) / 2F) );
            int diffX = (int)Math.Abs(XPos - otherLoc.XPos);
            int diffY = (int)Math.Abs(YPos - otherLoc.YPos);

            int dist = 0;// + Math.Floor(Math.Abs(YPos - otherLoc.YPos) / 2F));

            if (diffX >= diffY)
            {
                dist = diffX;
            }
            else
            {
                dist = (diffX + diffY) / 2;
            }
            return dist;
        }


        public Route RouteTo(Location targetLocation)
        {
            // most likely there is an easier method for this, but this is the best i came up with
            int dist1 = 0;
            int? dist2 = null;
            Direction dir1 = Direction.North;
            Direction? dir2 = default(Direction?);

            int x = XPos - targetLocation.XPos;
            int y = YPos - targetLocation.YPos;

            if (Math.Abs(x) < Math.Abs(y))
            {
                if (y > 0)
                {
                    dir1 = Direction.North;
                    if (x > 0)
                    {
                        dir2 = Direction.NorthWest;
                    }
                    else if (x < 0)
                    {
                        dir2 = Direction.NorthEast;
                        // else x = 0 dir2 = null
                    }
                }
                else if (y < 0)
                {
                    dir1 = Direction.South;
                    if (x > 0)
                    {
                        dir2 = Direction.SouthWest;
                    }
                    else if (x < 0)
                    {
                        dir2 = Direction.SouthEast;
                        // else x = 0 dir2 = null
                    }
                }
                else // y = 0 ost oder west
                {
                    if (x > 0)
                    {
                        dir1 = Direction.NorthWest;
                        dir2 = Direction.SouthWest;
                    }
                    else if (x < 0)
                    {
                        dir1 = Direction.NorthEast;
                        dir2 = Direction.SouthEast;
                    }
                }
            }
            else if (Math.Abs(x) > Math.Abs(y))
            {
                if (x > 0)
                {
                    dir1 = Direction.NorthWest;
                    dir2 = Direction.SouthWest;
                }
                else if (x < 0)
                {
                    dir1 = Direction.NorthEast;
                    dir2 = Direction.SouthEast;
                }
            }
            else // abs(x) = abs(y)
            {
                if (y > 0)
                {
                    if (x > 0)
                    {
                        dir1 = Direction.NorthWest;
                    }
                    else
                    {
                        dir1 = Direction.NorthEast;
                    }
                }
                else
                {
                    if (x > 0)
                    {
                        dir1 = Direction.SouthWest;
                    }
                    else
                    {
                        dir1 = Direction.SouthEast;
                    }
                }
            }


            int u = 0;
            int d = 0;
            int n = 0;

            if (Math.Abs(x) > Math.Abs(y))
            {
                u = (Math.Abs(x) - Math.Abs(y)) / 2;
                d = (Math.Abs(x) - Math.Abs(y)) / 2;
                x -= x - y;
            }

            if (Math.Abs(x) < Math.Abs(y))
            {
                if (y > 0)
                {
                    n = Math.Abs((y - Math.Abs(x)) / 2);
                    y -= y - Math.Abs(x);
                }
                else
                {
                    n = Math.Abs((y + Math.Abs(x)) / 2);
                    y -= y + Math.Abs(x);
                }
            }

            if (Math.Abs(x) == Math.Abs(y))
            {
                if (y == x)
                {
                    if (y > 0)
                    {
                        u += y;
                    }
                    else
                    {
                        d -= y;
                    }
                }
                else if (x > 0)
                {
                    d += x;
                }
                else if (y > 0)
                {
                    u += y;
                }
            }

            if (dir1 == Direction.North || dir1 == Direction.South) { dist1 = n; }
            else if (dir1 == Direction.NorthWest || dir1 == Direction.NorthEast) { dist1 = u; }
            else if (dir1 == Direction.SouthWest || dir1 == Direction.SouthEast) { dist1 = d; }

            if (dir2.HasValue)
            {
                if (dir2 == Direction.NorthWest || dir2 == Direction.NorthEast) { dist2 = u; }
                else if (dir2 == Direction.SouthWest || dir2 == Direction.SouthEast) { dist2 = d; }
            }

            if (dist2.HasValue && dist2.Value > dist1) // reihenfolge switchen
            {
                return new Route(dir2.Value, dist2.Value, dir1, dist1);
            }
            else
            {
                return new Route(dir1, dist1, dir2, dist2);
            }
        }

        public Location DirectTo(Direction direction, int distance)
        {
            int newY = YPos;
            int newX = XPos;
            switch (direction)
            {
                case Direction.North:
                    {
                        newY -= 2 * distance;
                        break;
                    }

                case Direction.NorthEast:
                    {
                        newX += distance;
                        newY -= distance;
                        break;
                    }

                case Direction.SouthEast:
                    {
                        newX += distance;
                        newY += distance;
                        break;
                    }

                case Direction.South:
                    {
                        newY += 2 * distance;
                        break;
                    }

                case Direction.SouthWest:
                    {
                        newX -= distance;
                        newY += distance;
                        break;
                    }

                case Direction.NorthWest:
                    {
                        newX -= distance;
                        newY -= distance;
                        break;
                    }
            }

            return new Location(newX, newY);
        }


    }
}