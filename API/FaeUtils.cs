using Terraria.ID;

namespace FaeLibrary.API {
    public class FaeUtils {

        /// <summary>
        /// <para>NOTE: This method was simply copy-pasted from Calamity. This is the best way something like this can be achieved, so credit to them.</para>
        /// Adds a shimmer recipe, while having the result transform into the ingredient's original result.
        /// <para>This is used for inserting items into various shimmer result trees/loops, like the Class Emblem loop.</para>
        /// </summary>
        public static void InsertShimmerResult(int result, int ingredient) {
            ItemID.Sets.ShimmerTransformToItem[result] = ItemID.Sets.ShimmerTransformToItem[ingredient];
            ItemID.Sets.ShimmerTransformToItem[ingredient] = result;
        }

    }
}
