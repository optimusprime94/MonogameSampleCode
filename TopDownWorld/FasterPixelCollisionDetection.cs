using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownWorld
{
    static class FasterPixelCollisionDetection
    {
        static bool IntersectPixels(Rectangle rectangleA, Color[] dataA, Rectangle rectangleB, Color[] dataB)
        {
            if (!rectangleA.Intersects(rectangleB)) { return false; }

            Rectangle its = Rectangle.Intersect(rectangleA, rectangleB);

            //shapeRenderer.AddBoundingRectangle(rectangleA, Color.White, 0.02f);
            //shapeRenderer.AddBoundingRectangle(rectangleB, Color.Yellow, 0.02f);
            //shapeRenderer.AddBoundingRectangle(its, Color.Pink, 0.02f);

            // Check every point within the intersection bounds
            for (int y = its.Top; y < its.Bottom; y++)
            {
                for (int x = its.Left; x < its.Right; x++)
                {
                    // Get the color of both pixels at this point
                    Color colorA = dataA[(x - rectangleA.Left) + (y - rectangleA.Top) * rectangleA.Width];
                    Color colorB = dataB[(x - rectangleB.Left) + (y - rectangleB.Top) * rectangleB.Width];

                    // If both pixels are not completely transparent,
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        // then an intersection has been found
                        return true;
                    }
                }
            }

            // No intersection found
            return false;
        }
    }
}
