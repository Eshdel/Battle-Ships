using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleShips.Scenes.Scripts;
public class SceneManagerExapmle : SceneManagerBase
{
    public override void InitScenesMap() {
       this.sceneConfigMap[SceneConfigExample.SCENE_NAME] = new SceneConfigExample();
    }
}
