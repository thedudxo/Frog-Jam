using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class FindClosest
{
    delegate bool Direction(float a, float b);
    static bool Ahead(float a, float b) => a > b;
    static bool Behind(float a, float b) => a < b;

    static private Obj ClosestInDirection<Obj>(IEnumerable<Obj> list, float pos, Direction compare)
        where Obj : MonoBehaviour
    {
        if (list.Count() == 0) return null;

        var positionQuery =
            from item in list
            let itemPos = item.transform.position.x
            where compare(itemPos, pos)
            orderby itemPos descending
            select item;

        List<Obj> resutls = positionQuery.ToList();
        if (resutls.Count() == 0) return null;

        if (compare == Ahead) return resutls?.Last();
        if (compare == Behind) return resutls?.First();

        throw new System.Exception($"Expected {compare} to be either 'Ahead' or 'Behind'");
    }

    public static Obj Ahead<Obj>(IEnumerable<Obj> list, float pos) where Obj : MonoBehaviour
        => ClosestInDirection(list, pos, Ahead);
    public static Obj Behind<Obj>(IEnumerable<Obj> list, float pos) where Obj : MonoBehaviour
        => ClosestInDirection(list, pos, Behind);
}
