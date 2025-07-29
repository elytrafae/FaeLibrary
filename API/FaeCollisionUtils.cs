
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FaeLibrary.API {
    public class FaeCollisionUtils {

        #region Separating Axis

        // Tutorial I adapted: https://sayedhajaj.com/posts/separating%20axis%20theorem%20collision%20detection/

        /// <summary>
        /// Allows you to check collision between two rectangles that may or may not be axis aligned
        /// </summary>
        /// <param name="rectangle1">The first object's hitbox</param>
        /// <param name="rotation1">The first object's rotation, in radians</param>
        /// <param name="rectangle2">The second object's hitbox</param>
        /// <param name="rotation2">The second object's rotation, in radians</param>
        /// <returns></returns>
        public static bool OrientedRactanglesCollide(Rectangle rectangle1, float rotation1, Rectangle rectangle2, float rotation2 = 0) {
            var axes = OrientedRectangleGetAxes(rotation1).Concat(OrientedRectangleGetAxes(rotation2));
            var corners = OrientedRectangleGetCorners(rectangle1, rotation1);
            var otherCorners = OrientedRectangleGetCorners(rectangle2, rotation2);
            foreach (var axis in axes) {
                if (OrientedRectangleGetMinVertex(axis, corners) is [float thisMin, float thisMax] && OrientedRectangleGetMinVertex(axis, otherCorners) is [float otherMin, float otherMax]) {
                    // if one of the axes does not overlap, it's not colliding
                    if (thisMax < otherMin || otherMax < thisMin) {
                        return false;
                    }
                }
            }
            // none of the axes have overlapped, so it is colliding
            return true;
        }

        public static void DrawOrientedRactangleHitboxDebug(Rectangle rectangle, float rotation) {
            Main.EntitySpriteDraw(
                TextureAssets.MagicPixel.Value,
                rectangle.Center() - Main.screenPosition,
                new Rectangle(0, 0, 1, 1),
                Color.Orange * 0.4f,
                rotation,
                new Vector2(0.5f, 0.5f),
                new Vector2(rectangle.Width, rectangle.Height),
                SpriteEffects.None
            );
        }

        public static void DrawOrientedRactangleHitboxDebug(ModProjectile modProjectile) {
            Rectangle hitbox = modProjectile.Projectile.Hitbox;
            modProjectile.ModifyDamageHitbox(ref hitbox);
            DrawOrientedRactangleHitboxDebug(hitbox, modProjectile.Projectile.rotation);
        }

        private static List<Vector2> OrientedRectangleGetAxes(float angle) {
            // in a rectangle, opposite sides are parallel
            // meaning that they face the same direction
            // so there is no need to perform a dot product with the same vector twice
            // one side is also 90 degrees shifted from the other
            return [
                new Vector2(
                    (float)Math.Cos(angle),
                    (float)Math.Sin(angle)
                ),
                new Vector2(
                    (float)Math.Cos(angle + MathHelper.PiOver2),
                    (float)Math.Sin(angle + MathHelper.PiOver2)
                )
            ];
        }

        private static List<Vector2> OrientedRectangleGetCorners(Rectangle rectangle, float angle) {
            // don't worry too much about how this works
            // it assumes that the rectangle is located with a centre of (0, 0)
            // then it rotates using complex numbers (multiply to rotate from real to imaginary numbers)
            // (You can rotate using other methods)
            // then it adds the rotated corners to the centre position
            // giving the coordinates of all the coordinates
            // this is mostly useful if the angle or position of the rectangle can change
            // which is normally the case in games
            Vector2 halfDimensions = rectangle.Size() * 0.5f;

            List<Vector2> points = [
                halfDimensions * -1,
                new Vector2(halfDimensions.X, -halfDimensions.Y),
                new Vector2(-halfDimensions.X, halfDimensions.Y),
                halfDimensions
            ];

            List<Vector2> corners = new();

            for (int i= 0; i < points.Count; i++) {
                Vector2 point = points[i];
                Vector2 complexAngle = new Vector2(
                    (float)Math.Cos(angle),
                    (float)Math.Sin(angle)
                );
                corners.Add(rectangle.Center.ToVector2() + new Vector2(
                    point.X * complexAngle.X - point.Y * complexAngle.Y,
                    point.X * complexAngle.Y + point.Y * complexAngle.X
                ));
            }

            return corners;
        }

        private static float[] OrientedRectangleGetMinVertex(Vector2 axis, List<Vector2> corners) {
            var thisMin = FaeUtils.DotProduct(corners[0], axis);
            var thisMax = thisMin;
                
            foreach (var corner in corners) {
                var distance = FaeUtils.DotProduct(corner, axis);
                if (distance < thisMin) {
                    thisMin = distance;
                }
                if (distance > thisMax) {
                    thisMax = distance;
                }
            }
            return [thisMin, thisMax];
        }

        #endregion

        public static Rectangle GetRectangleThatContainsBoth(Vector2 corner1, Vector2 corner2) { 
            float minX = Math.Min(corner1.X, corner2.X);
            float maxX = Math.Max(corner1.X, corner2.X);
            float minY = Math.Min(corner1.Y, corner2.Y);
            float maxY = Math.Max(corner1.Y, corner2.Y);

            return new Rectangle((int)minX, (int)minY, (int)Math.Ceiling(maxX - minX), (int)Math.Ceiling(maxY - minY));
        }

    }
}
