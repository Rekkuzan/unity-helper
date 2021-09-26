using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyList3D : Rekkuzan.Helper.UI.List3DElement.ScrollviewList3DElement
{
    [Header("Dummy parameters")]
    public List<Color> ColorsToShow = new List<Color>();

    protected override void BuildList()
    {
        for (int i = 0; i < ColorsToShow.Count; i++)
        {
            var cc = Instantiate(this.GameObjectPrefab);

            // spawn it quite far for instance or on a hidden layer
            cc.transform.position = Vector3.right * (1000 + i * 15);
            cc.Initialize(this);

            _current3DElements.Add(cc);

            if (cc is Dummy3D dummy3D)
            {
                dummy3D.InitializeDummy(ColorsToShow[i]);
            }
        }
    }
}
